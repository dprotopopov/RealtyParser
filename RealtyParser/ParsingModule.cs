using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RealtyParser.Collections;
using RT.ParsingLibs;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;
using ServiceStack;

namespace RealtyParser
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "ParsingModule")]
    public class ParsingModule : IParsingModule
    {
        /// <summary>
        ///     Единый коннектор к классу базы данных
        /// </summary>
        public static readonly Database Database = new Database();

        private static readonly Transformation Transformation = new Transformation();

        public static readonly Parser Parser = new Parser {Transformation = Transformation};

        private object _lastError;

        /// <summary>
        ///     Задача на парсинг
        /// </summary>
        /// <param name="request">Запрос на парсинг</param>
        /// <returns>Ответ от парсера</returns>
        public async Task<ParseResponse> Result(ParseRequest request)
        {
            Debug.WriteLine("-------------------------------------------------------------------");
            Debug.WriteLine("Переданы параметры:");
            Debug.WriteLine("request.RegionId -> {0}", request.RegionId);
            Debug.WriteLine("request.RubricId -> {0}", request.RubricId);
            Debug.WriteLine("request.ActionId -> {0}", request.ActionId);
            Debug.WriteLine(string.Format("request.LastPublicationId -> '{0}'", request.LastPublicationId));
            Debug.WriteLine("-------------------------------------------------------------------");

            var requestId = new RequestProperties
            {
                {"Action", Database.GetScalar(request.ActionId, "Action")},
                {"Rubric", Database.GetScalar(request.RubricId, "Rubric")},
                {"Region", Database.GetScalar(request.RegionId, "Region")},
                {Database.SiteTable, Database.GetScalar((long) 2, Database.SiteTable)},
            };
            SiteProperties properties = Database.GetSiteProperties(requestId.Site);
            IComparer<string> comparer =
                Comparer.CreatePublicationComparer(properties.PublicationComparerClassName.ToString());
            ReturnFieldInfos returnFieldInfos = Database.GetReturnFieldInfos(requestId.Site);
            Debug.WriteLine("id {0}", requestId);
            Debug.WriteLine("properties {0}", properties);
            Debug.WriteLine("returnFieldInfos {0}", returnFieldInfos);

            string lastResourceId = request.LastPublicationId;
            long pageId = 0;
            int responseLevel = 4;
            var responseCode = new List<ParseResponseCode>
            {
                ParseResponseCode.NotAvailableResource, // Неправильные параметры
                ParseResponseCode.NotFoundId, // Нет никаких данных
                ParseResponseCode.NotFoundId, // Данные есть, но последний Id не найден
                ParseResponseCode.ContentEmpty, // Последний Id найден, но нечего возвращать
                ParseResponseCode.Success // Последний Id найден и есть что вернуть
            };

            if (requestId.Region == null || requestId.Rubric == null || requestId.Action == null ||
                requestId.Site == null ||
                string.IsNullOrEmpty(requestId.Region.ToString()) ||
                string.IsNullOrEmpty(requestId.Rubric.ToString()) ||
                string.IsNullOrEmpty(requestId.Action.ToString()) ||
                string.IsNullOrEmpty(requestId.Site.ToString()))

            {
                responseLevel = Math.Min(0, responseLevel);
                return new ParseResponse
                {
                    ResponseCode = responseCode[responseLevel],
                    LastPublicationId = lastResourceId,
                    ModuleName = GetType().ToString(),
                    Publications = new List<WebPublication>()
                };
            }

            IEnumerable<string> mapping =
                Database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());
            var mappedId = new RequestProperties
            {
                requestId.Keys.ToDictionary(item => item,
                    item =>
                        mapping.Contains(item)
                            ? Database.GetScalar(requestId[item], item, requestId.Site)
                            : requestId[item])
            };

            Debug.WriteLine("mappingId {0}", mappedId);

            if (mappedId.Region == null || mappedId.Rubric == null || mappedId.Action == null ||
                mappedId.Site == null ||
                string.IsNullOrEmpty(mappedId.Region.ToString()) ||
                string.IsNullOrEmpty(mappedId.Rubric.ToString()) ||
                string.IsNullOrEmpty(mappedId.Action.ToString()) ||
                string.IsNullOrEmpty(mappedId.Site.ToString()))
            {
                responseLevel = Math.Min(0, responseLevel);
                return new ParseResponse
                {
                    ResponseCode = responseCode[responseLevel],
                    LastPublicationId = lastResourceId,
                    ModuleName = GetType().ToString(),
                    Publications = new List<WebPublication>()
                };
            }

            Debug.WriteLine("Параметр LastPublicationId " +
                            (string.IsNullOrEmpty(lastResourceId) ? " ПУСТОЙ !!!!!!!" : " НЕ ПУСТОЙ !!!!!!!"));

            if (string.IsNullOrEmpty(lastResourceId)) lastResourceId = "";

            Debug.WriteLine("-------------------------------------------------------------------");
            Debug.WriteLine("id {0}", requestId);
            Debug.WriteLine("mappingId {0}", mappedId);
            Debug.WriteLine("-------------------------------------------------------------------");

            Values siteValues = Parser.BuildValues(Database, requestId, mappedId);

            var baseBuilder = new UriBuilder(properties.Url.ToString())
            {
                UserName = properties.UserName.ToString(),
                Password = properties.Password.ToString(),
            };

            var resourceIds = new List<string>();
            var dictionary = new Dictionary<string, string>();
            while ((++pageId > 0 && resourceIds.Count < Database.ConvertTo<long>(properties.CountAd) &&
                    ((!lastResourceId.IsNullOrEmpty()) &&
                     (resourceIds.Count == 0 || comparer.Compare(resourceIds.First(), lastResourceId) > 0)) ||
                    (lastResourceId.IsNullOrEmpty() && pageId == 1)))
            {
                var pageValues =
                    new Values(siteValues)
                    {
                        {Regex.Escape(@"{{Page}}"), (pageId > 1) ? pageId.ToString(CultureInfo.InvariantCulture) : @""}
                    };
                Debug.WriteLine(pageValues.ToString());
                int count = 0;
                foreach (
                    string url in Transformation.ParseTemplate(properties.LookupTemplate.ToString(), pageValues)
                    )
                {
                    Debug.WriteLine(url);
                    try
                    {
                        var builder = new UriBuilder(baseBuilder + url);
                        HtmlDocument[] documents =
                            await
                                Parser.WebRequestHtmlDocument(builder.Uri, properties.Method.ToString(),
                                    properties.Encoding.ToString());
                        HtmlNode[] nodes = documents.Select(document => document.DocumentNode).ToArray();
                        ReturnFields returnFields = Parser.BuildReturnFields(nodes,
                            pageValues, returnFieldInfos);
                        var arguments = new Values(returnFields);
                        List<string> keys = Transformation.ParseTemplate(properties.ResourceIdTemplate.ToString(),
                            arguments);
                        List<string> values = Transformation.ParseTemplate(properties.PublicationTemplate.ToString(),
                            arguments);
                        for (int i = returnFields.PublicationLink.Count; i-- > 0;)
                        {
                            try
                            {
                                dictionary.Add(keys[i], values[i]);
                                resourceIds.Add(keys[i]);
                            }
                            catch (Exception exception)
                            {
                                _lastError = exception;
                                Debug.WriteLine(_lastError.ToString());
                            }
                        }
                        count += returnFields.PublicationLink.Count;
                    }
                    catch (Exception exception)
                    {
                        _lastError = exception;
                        Debug.WriteLine(_lastError.ToString());
                    }
                }
                if (count == 0) break;
                resourceIds.Sort(comparer);
            }
            try
            {
                if (string.IsNullOrEmpty(lastResourceId) && resourceIds.Count == 0)
                {
                    responseLevel = Math.Min(1, responseLevel);
                }
                else if (string.IsNullOrEmpty(lastResourceId) && resourceIds.Count > 0)
                {
                    //При первом вызове метода парсера из DLL не известно о IdRes объявления внутри ресурса, поэтому он
                    //передает значение NULL. В этом случае парсер должен выбрать самое последнее объявление и
                    //вернуть его IdRes ресурса.
                    resourceIds.RemoveRange(0, resourceIds.Count - 1);
                }
                else if (!string.IsNullOrEmpty(lastResourceId) &&
                         resourceIds.BinarySearch(lastResourceId, comparer) < 0)
                {
                    responseLevel = Math.Min(2, responseLevel);

                    //Если IdRes не найден, то парсер возвращает последнии CountAD
                    //объявлений (такая ситуация может получится, если размещенное объявление к следующей
                    //итерации будет удалено на ресурсе)
                    resourceIds.RemoveRange(0,
                        (int) (resourceIds.Count - Database.ConvertTo<long>(properties.CountAd)));
                }

                if (!string.IsNullOrEmpty(lastResourceId) && resourceIds.Count > 0 &&
                    comparer.Compare(resourceIds.Last(), lastResourceId) < 0)
                {
                    //Одним из условий работы парсера является требование к оптимизации алгоритма
                    //получения новых объявлений, т.е. необходимо делать минимальное количество запросов
                    //к ресурсу. Для этого парсер должен использовать форму расширенного поиска, чтобы
                    //получать выборку нужных объявлений. Затем отсеивать объявления, которые были до
                    //IdRes и использовать дальнейшие ссылки только для новых объявлений.
                    responseLevel = Math.Min(3, responseLevel);
                    resourceIds.Clear();
                }
                else if (!string.IsNullOrEmpty(lastResourceId))
                {
                    //Одним из условий работы парсера является требование к оптимизации алгоритма
                    //получения новых объявлений, т.е. необходимо делать минимальное количество запросов
                    //к ресурсу. Для этого парсер должен использовать форму расширенного поиска, чтобы
                    //получать выборку нужных объявлений. Затем отсеивать объявления, которые были до
                    //IdRes и использовать дальнейшие ссылки только для новых объявлений.
                    responseLevel = Math.Min(4, responseLevel);
                    resourceIds =
                        resourceIds.Where(item => comparer.Compare(item, lastResourceId) > 0).ToList();
                }

                resourceIds.RemoveRange(0,
                    (int) (resourceIds.Count - Database.ConvertTo<long>(properties.CountAd)));
            }
            catch (Exception exception)
            {
                _lastError = exception;
                Debug.WriteLine(_lastError.ToString());
            }
            var publications = new List<WebPublication>();
            foreach (string resourceId in resourceIds)
            {
                try
                {
                    string url = dictionary[resourceId];
                    var builder = new UriBuilder(baseBuilder + url);
                    HtmlDocument[] documents =
                        await
                            Parser.WebRequestHtmlDocument(builder.Uri, properties.Method.ToString(),
                                properties.Encoding.ToString());
                    HtmlNode[] nodes = documents.Select(document => document.DocumentNode).ToArray();
                    ReturnFields returnFields = Parser.BuildReturnFields(nodes,
                        siteValues, returnFieldInfos);
                    publications.Add(Parser.CreateWebPublication(returnFields, requestId, builder));
                }
                catch (Exception exception)
                {
                    _lastError = exception;
                    Debug.WriteLine(_lastError.ToString());
                }
            }

            if (!publications.Any()) responseLevel = Math.Min(3, responseLevel);

            return new ParseResponse
            {
                ResponseCode = responseCode[responseLevel],
                LastPublicationId = resourceIds.DefaultIfEmpty(lastResourceId).LastOrDefault(),
                ModuleName = GetType().ToString(),
                Publications = publications
            };
        }

        /// <summary>
        ///     Получить названия ресурсов, обрабатываемая библиотекой
        /// </summary>
        /// <param name="bind">Бинд запроса</param>
        /// <returns> Коллекция названий ресурсов (сайтов)</returns>
        public IList<string> Sources(Bind bind)
        {
            var requestId = new RequestProperties
            {
                {"Action", Database.GetScalar(bind.ActionId, "Action")},
                {"Rubric", Database.GetScalar(bind.RubricId, "Rubric")},
                {"Region", Database.GetScalar(bind.RegionId, "Region")},
            };

            if (requestId.Region == null || requestId.Rubric == null || requestId.Action == null)
                return new List<string>();

            Mapping sites = Database.GetMapping(Database.SiteTable);

            return (from site in sites
                where
                    Database.GetScalar(requestId.Region, "Region", site.Key) != null &&
                    Database.GetScalar(requestId.Rubric, "Rubric", site.Key) != null &&
                    Database.GetScalar(requestId.Action, "Action", site.Key) != null
                select site.Value.ToString()).ToList();
        }

        /// <summary>
        ///     Получить информацию о разработчике
        /// </summary>
        /// <returns>Информация о разработчике</returns>
        public AboutResponse About()
        {
            return new AboutResponse
            {
                Info = "Dmitry Protopopov",
                Contacts = "dmitry@protopopov.ru",
                CopyRight = "All reserved"
            };
        }

        /// <summary>
        ///     Получить список биндов, обрабатываемая библиотекой
        /// </summary>
        /// <returns>Коллекция биндов</returns>
        public IList<Bind> Keys()
        {
            object siteId = Database.GetScalar((long) 2, Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return (from region in mappings.Region.Keys
                from rubric in mappings.Rubric.Keys
                from action in mappings.Action.Keys
                select new Bind
                {
                    RegionId = Database.ConvertTo<int>(region),
                    RubricId = Database.ConvertTo<int>(rubric),
                    ActionId = Database.ConvertTo<int>(action)
                }).ToList();
        }

        public IList<Bind> KeysRubrics()
        {
            object siteId = Database.GetScalar((long) 2, Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return (from rubric in mappings.Rubric.Keys
                select new Bind
                {
                    RubricId = Database.ConvertTo<int>(rubric),
                }).ToList();
        }

        public IList<Bind> KeysRegions()
        {
            object siteId = Database.GetScalar((long) 2, Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return (from region in mappings.Region.Keys
                select new Bind
                {
                    RegionId = Database.ConvertTo<int>(region),
                }).ToList();
        }

        public IList<Bind> KeysActions()
        {
            object siteId = Database.GetScalar((long) 2, Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return (from action in mappings.Action.Keys
                select new Bind
                {
                    ActionId = Database.ConvertTo<int>(action)
                }).ToList();
        }
    }
}