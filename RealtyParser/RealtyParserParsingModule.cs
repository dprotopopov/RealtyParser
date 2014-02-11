using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RT.ParsingLibs;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;
using ServiceStack;

namespace RealtyParser
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "RealtyParserParsingModule")]
    public class RealtyParserParsingModule : IParsingModule
    {
        /// <summary>
        ///     Единый коннектор к классу базы данных
        /// </summary>
        public static readonly Database Database = new Database();

        public static readonly Parser Parser = new Parser();

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

            var id = new RequestProperties
            {
                {"Action", Database.GetScalar(request.ActionId, "Action")},
                {"Rubric", Database.GetScalar(request.RubricId, "Rubric")},
                {"Region", Database.GetScalar(request.RegionId, "Region")},
                {"Site", Database.GetScalar((long) 2, "Site")},
            };
            SiteProperties properties = Database.GetSiteProperties(id.Site);
            IComparer<string> comparer =
                Comparer.CreatePublicationComparer(properties.PublicationComparerClassName.ToString());
            ReturnFieldInfos returnFieldInfos = Database.GetReturnFieldInfos(id.Site);

            Debug.WriteLine("id {0}", id);
            Debug.WriteLine("properties {0}", properties);
            Debug.WriteLine("returnFieldInfos {0}", returnFieldInfos);

            string lastResourceId = request.LastPublicationId;
            long pageId = 0;
            int responceLevel = 4;
            var responseCode = new List<ParseResponseCode>
            {
                ParseResponseCode.NotAvailableResource, // Неправильные параметры
                ParseResponseCode.NotFoundId, // Нет никаких данных
                ParseResponseCode.NotFoundId, // Данные есть, но последний Id не найден
                ParseResponseCode.ContentEmpty, // Последний Id найден, но нечего возвращать
                ParseResponseCode.Success // Последний Id найден и есть что вернуть
            };

            if (id.Region == null || id.Rubric == null || id.Action == null || id.Site == null ||
                System.String.IsNullOrEmpty(id.Region.ToString()) ||
                System.String.IsNullOrEmpty(id.Rubric.ToString()) ||
                System.String.IsNullOrEmpty(id.Action.ToString()) ||
                System.String.IsNullOrEmpty(id.Site.ToString()))

            {
                responceLevel = Math.Min(0, responceLevel);
                return new ParseResponse
                {
                    ResponseCode = responseCode[responceLevel],
                    LastPublicationId = lastResourceId,
                    ModuleName = GetType().ToString(),
                    Publications = new List<WebPublication>()
                };
            }

            IEnumerable<string> mapping = Database.GetList("Mapping", "TableName").Select(item => item.ToString());
            var mappingId = new RequestProperties
            {
                id.Keys.ToDictionary(item => item,
                    item => mapping.Contains(item) ? Database.GetScalar(id[item], item, id.Site) : id[item])
            };

            Debug.WriteLine("mappingId {0}", mappingId);

            if (mappingId.Region == null || mappingId.Rubric == null || mappingId.Action == null ||
                mappingId.Site == null ||
                System.String.IsNullOrEmpty(mappingId.Region.ToString()) ||
                System.String.IsNullOrEmpty(mappingId.Rubric.ToString()) ||
                System.String.IsNullOrEmpty(mappingId.Action.ToString()) ||
                System.String.IsNullOrEmpty(mappingId.Site.ToString()))
            {
                responceLevel = Math.Min(0, responceLevel);
                return new ParseResponse
                {
                    ResponseCode = responseCode[responceLevel],
                    LastPublicationId = lastResourceId,
                    ModuleName = GetType().ToString(),
                    Publications = new List<WebPublication>()
                };
            }

            Debug.WriteLine("Параметр LastPublicationId " +
                            (System.String.IsNullOrEmpty(lastResourceId) ? " ПУСТОЙ !!!!!!!" : " НЕ ПУСТОЙ !!!!!!!"));

            if (System.String.IsNullOrEmpty(lastResourceId)) lastResourceId = "";

            Debug.WriteLine("-------------------------------------------------------------------");
            Debug.WriteLine("id {0}", id);
            Debug.WriteLine("mappingId {0}", mappingId);
            Debug.WriteLine("-------------------------------------------------------------------");

            Values siteValues = Parser.BuildValues(Database, id, mappingId);

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
                Values pageValues =
                    new Values(siteValues).InsertOrReplace(
                        Parser.BuildValues(pageId));
                Debug.WriteLine(pageValues.ToString());
                int count = 0;
                foreach (
                    string url in Parser.ParseTemplate(properties.LookupTemplate.ToString(), pageValues)
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
                        List<string> keys = Parser.ParseTemplate(properties.ResourceIdTemplate.ToString(), arguments);
                        List<string> values = Parser.ParseTemplate(properties.PublicationTemplate.ToString(), arguments);
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
                if (System.String.IsNullOrEmpty(lastResourceId) && resourceIds.Count == 0)
                {
                    responceLevel = Math.Min(1, responceLevel);
                }
                else if (System.String.IsNullOrEmpty(lastResourceId) && resourceIds.Count > 0)
                {
                    //При первом вызове метода парсера из DLL не известно о IdRes объявления внутри ресурса, поэтому он
                    //передает значение NULL. В этом случае парсер должен выбрать самое последнее объявление и
                    //вернуть его IdRes ресурса.
                    resourceIds.RemoveRange(0, resourceIds.Count - 1);
                }
                else if (!System.String.IsNullOrEmpty(lastResourceId) &&
                         resourceIds.BinarySearch(lastResourceId, comparer) < 0)
                {
                    responceLevel = Math.Min(2, responceLevel);

                    //Если IdRes не найден, то парсер возвращает последнии CountAD
                    //объявлений (такая ситуация может получится, если размещенное объявление к следующей
                    //итерации будет удалено на ресурсе)
                    resourceIds.RemoveRange(0,
                        (int) (resourceIds.Count - Database.ConvertTo<long>(properties.CountAd)));
                }

                if (!System.String.IsNullOrEmpty(lastResourceId) && resourceIds.Count > 0 &&
                    comparer.Compare(resourceIds.Last(), lastResourceId) < 0)
                {
                    //Одним из условий работы парсера является требование к оптимизации алгоритма
                    //получения новых объявлений, т.е. необходимо делать минимальное количество запросов
                    //к ресурсу. Для этого парсер должен использовать форму расширенного поиска, чтобы
                    //получать выборку нужных объявлений. Затем отсеивать объявления, которые были до
                    //IdRes и использовать дальнейшие ссылки только для новых объявлений.
                    responceLevel = Math.Min(3, responceLevel);
                    resourceIds.Clear();
                }
                else if (!System.String.IsNullOrEmpty(lastResourceId))
                {
                    //Одним из условий работы парсера является требование к оптимизации алгоритма
                    //получения новых объявлений, т.е. необходимо делать минимальное количество запросов
                    //к ресурсу. Для этого парсер должен использовать форму расширенного поиска, чтобы
                    //получать выборку нужных объявлений. Затем отсеивать объявления, которые были до
                    //IdRes и использовать дальнейшие ссылки только для новых объявлений.
                    responceLevel = Math.Min(4, responceLevel);
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
                    publications.Add(Parser.CreateWebPublication(returnFields, id, builder));
                }
                catch (Exception exception)
                {
                    _lastError = exception;
                    Debug.WriteLine(_lastError.ToString());
                }
            }

            if (!publications.Any()) responceLevel = Math.Min(3, responceLevel);

            return new ParseResponse
            {
                ResponseCode = responseCode[responceLevel],
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
            var id = new RequestProperties
            {
                {"Action", Database.GetScalar(bind.ActionId, "Action")},
                {"Rubric", Database.GetScalar(bind.RubricId, "Rubric")},
                {"Region", Database.GetScalar(bind.RegionId, "Region")},
            };

            if (id.Region == null || id.Rubric == null || id.Action == null)
                return new List<string>();

            Dictionary<object, object> sites = Database.GetDictionary("Site");

            return (from site in sites
                where
                    Database.GetScalar(id.Region, "Region", site.Key) != null &&
                    Database.GetScalar(id.Rubric, "Rubric", site.Key) != null &&
                    Database.GetScalar(id.Action, "Action", site.Key) != null
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
            object siteId = Database.GetScalar((long) 2, "Site");

            Mapping mapping = Database.GetMapping(siteId);

            return (from region in mapping.Region.Keys
                from rubric in mapping.Rubric.Keys
                from action in mapping.Action.Keys
                select new Bind
                {
                    RegionId = Database.ConvertTo<int>(region),
                    RubricId = Database.ConvertTo<int>(rubric),
                    ActionId = Database.ConvertTo<int>(action)
                }).ToList();
        }
    }
}