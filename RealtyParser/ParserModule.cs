using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RealtyParser.Collections;
using RT.ParsingLibs;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;
using ServiceStack;
using Uri = RealtyParser.Types.Uri;

namespace RealtyParser
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "ParserModule")]
    public class ParserModule : IParsingModule
    {
        #region

        public string ModuleNamespace { get; set; }
        public Database Database { get; set; }
        public Transformation Transformation { get; set; }
        public Parser Parser { get; set; }
        public ComparerManager ComparerManager { get; set; }
        public CompressionManager CompressionManager { get; set; }
        public Crawler Crawler { get; set; }

        private ReturnFieldInfos ReturnFieldInfos { get; set; }
        private IComparer<string> Comparer { get; set; }
        private SiteProperties SiteProperties { get; set; }
        private Converter Converter { get; set; }

        #endregion

        private object _lastError;

        public ParserModule()
        {
            ModuleNamespace = GetType().Namespace;
            Database = new Database {ModuleNamespace = ModuleNamespace};
            ComparerManager = new ComparerManager {ModuleNamespace = ModuleNamespace};
            CompressionManager = new CompressionManager {ModuleNamespace = ModuleNamespace};
            Converter = new Converter {ModuleNamespace = ModuleNamespace};
            Transformation = new Transformation();
            Parser = new Parser {Transformation = Transformation, Database = Database, Converter = Converter};
            Crawler = new Crawler {CompressionManager = CompressionManager};
        }

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

            string lastResourceId = request.LastPublicationId;
            int responseLevel = 4;
            var responseCode = new List<ParseResponseCode>
            {
                ParseResponseCode.NotAvailableResource, // Неправильные параметры
                ParseResponseCode.NotFoundId, // Нет никаких данных
                ParseResponseCode.NotFoundId, // Данные есть, но последний Id не найден
                ParseResponseCode.ContentEmpty, // Последний Id найден, но нечего возвращать
                ParseResponseCode.Success // Последний Id найден и есть что вернуть
            };

            var requestId = new RequestProperties
            {
                {"Action", Database.GetScalar(request.ActionId, "Action")},
                {"Rubric", Database.GetScalar(request.RubricId, "Rubric")},
                {"Region", Database.GetScalar(request.RegionId, "Region")},
                {
                    Database.SiteTable,
                    Database.GetScalar(ModuleNamespace, Database.SiteIdColumn, Database.ModuleNamespaceColumn,
                        Database.SiteTable)
                },
            };

            if (!requestId.Select(pair => pair.Value != null).Aggregate((i, j) => i && j))
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

            SiteProperties = Database.GetSiteProperties(requestId.Site);
            Comparer = ComparerManager.CreatePublicationComparer(SiteProperties.PublicationComparerClassName.ToString());
            ReturnFieldInfos = Database.GetReturnFieldInfos(requestId.Site);
            Crawler.Method = SiteProperties.Method.ToString();
            Crawler.Encoding = SiteProperties.Encoding.ToString();
            Crawler.Compression = SiteProperties.CompressionClassName.ToString();

            Debug.WriteLine("requestId {0}", requestId);
            Debug.WriteLine("properties {0}", SiteProperties);
            Debug.WriteLine("returnFieldInfos {0}", ReturnFieldInfos);


            IEnumerable<string> mapping =
                Database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());

            Dictionary<string, List<object>> mappedEnum = mapping.ToDictionary(s => s,
                s => Database.GetList(requestId[s], s, requestId.Site).ToList());

            Dictionary<string, List<object>> childrenEnum = mappedEnum.ToDictionary(table => table.Key,
                table => new List<object>());
            foreach (var pair in mappedEnum)
                foreach (object item in pair.Value)
                    childrenEnum[pair.Key].AddRange(Database.GetList(pair.Key, item, requestId.Site).ToList());
            childrenEnum = childrenEnum.ToDictionary(pair => pair.Key, pair => pair.Value.Distinct().ToList());

            var includeList = new StackListQueue<RequestProperties>
            {
                from action in mappedEnum["Action"]
                from rubric in mappedEnum["Rubric"]
                from region in mappedEnum["Region"]
                select new RequestProperties {Action = action, Rubric = rubric, Region = region, Site = requestId.Site}
            };


            if (!includeList.Any())
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

            var excludeList = new StackListQueue<RequestProperties>
            {
                from action in mappedEnum["Action"]
                from rubric in childrenEnum["Rubric"]
                from region in mappedEnum["Region"]
                select new RequestProperties {Action = action, Rubric = rubric, Region = region, Site = requestId.Site}
            };


            if (string.IsNullOrEmpty(lastResourceId)) lastResourceId = "";

            var baseBuilder = new UriBuilder(SiteProperties.Url.ToString())
            {
                UserName = SiteProperties.UserName.ToString(),
                Password = SiteProperties.Password.ToString(),
            };
            var returnFieldInfos = new ReturnFieldInfos();
            foreach (
                string key in
                    ReturnFieldInfos.GetType().GetProperties()
                        .Select(propertyInfo => propertyInfo.Name)
                        .Where(key => ReturnFieldInfos.ContainsKey(key)))
                returnFieldInfos.Add(key, ReturnFieldInfos[key]);

            var dictionary = new Dictionary<string, KeyValuePair<string, Values>>();
            var resourceIds = new StackListQueue<string>();
            foreach (RequestProperties mappedId in includeList)
            {
                var mappedLevel = new RequestProperties
                {
                    mappedId.Where(pair => mapping.Contains(pair.Key)).ToDictionary(pair => pair.Key,
                        pair =>
                            Database.GetScalar(pair.Value, Database.LevelColumn, pair.Key, requestId.Site))
                };

                Values siteValues = Parser.BuildValues(requestId, mappedId, mappedLevel);

                var currentResourceIds = new StackListQueue<string>();
                for (long pageId = 1;
                    currentResourceIds.Count < Database.ConvertTo<long>(SiteProperties.CountAd);
                    pageId++)
                {
                    var pageValues = new Values(siteValues)
                    {
                        Page = new List<string> {((pageId > 1) ? pageId.ToString(CultureInfo.InvariantCulture) : @"")}
                    };
                    Debug.WriteLine(pageValues.ToString());
                    var loopResourceIds = new StackListQueue<string>();
                    foreach (
                        string url in Transformation.ParseTemplate(SiteProperties.LookupTemplate.ToString(), pageValues)
                        )
                    {
                        Debug.WriteLine(url);
                        try
                        {
                            var builder = new UriBuilder(Uri.Combine(baseBuilder.Uri.ToString(), url))
                            {
                                UserName = baseBuilder.UserName,
                                Password = baseBuilder.Password,
                            };
                            var urlValues = new Values(pageValues)
                            {
                                Url = new List<string> {builder.Uri.ToString()}
                            };
                            IEnumerable<HtmlDocument> documents =
                                await
                                    Crawler.WebRequestHtmlDocument(builder.Uri);
                            Thread.Sleep(0);
                            ReturnFields returnFields = Parser.BuildReturnFields(documents,
                                urlValues, returnFieldInfos);
                            var values1 = new Values(returnFields);
                            List<string> keys =
                                Transformation.ParseTemplate(SiteProperties.ResourceIdTemplate.ToString(),
                                    values1).ToList();
                            List<string> values = Transformation.ParseTemplate(
                                SiteProperties.PublicationTemplate.ToString(),
                                values1).ToList();
                            for (int i = 0; i < returnFields.PublicationLink.Count(); i++)
                            {
                                if (dictionary.ContainsKey(keys[i]))
                                    continue;
                                dictionary.Add(keys[i], new KeyValuePair<string, Values>(values[i], siteValues));
                                loopResourceIds.Enqueue(keys[i]);
                            }
                        }
                        catch (Exception exception)
                        {
                            _lastError = exception;
                            Debug.WriteLine(_lastError.ToString());
                        }
                    }
                    if (!loopResourceIds.Any()) break;
                    loopResourceIds.Sort(Comparer);
                    currentResourceIds = StackListQueue<string>.DistinctSorted(currentResourceIds, loopResourceIds,
                        Comparer);
                    if (lastResourceId.IsNullOrEmpty()) break;
                    if (Comparer.Compare(currentResourceIds.First(), lastResourceId) <= 0) break;
                }
                resourceIds = StackListQueue<string>.DistinctSorted(resourceIds, currentResourceIds, Comparer);
            }
            try
            {
                if (!string.IsNullOrEmpty(lastResourceId))
                {
                    //Одним из условий работы парсера является требование к оптимизации алгоритма
                    //получения новых объявлений, т.е. необходимо делать минимальное количество запросов
                    //к ресурсу. Для этого парсер должен использовать форму расширенного поиска, чтобы
                    //получать выборку нужных объявлений. Затем отсеивать объявления, которые были до
                    //IdRes и использовать дальнейшие ссылки только для новых объявлений.

                    IEnumerable<string> list = resourceIds.Where(item => Comparer.Compare(item, lastResourceId) >= 0);
                    resourceIds = new StackListQueue<string>();
                    resourceIds.AddRange(list);
                }

                if (!resourceIds.Any())
                {
                    responseLevel = Math.Min(1, responseLevel);
                }

                if (!string.IsNullOrEmpty(lastResourceId) && resourceIds.Any() &&
                    Comparer.Compare(resourceIds.First(), lastResourceId) > 0)
                {
                    responseLevel = Math.Min(2, responseLevel);
                }

                if (!string.IsNullOrEmpty(lastResourceId) && resourceIds.Any() &&
                    Comparer.Compare(resourceIds.Last(), lastResourceId) == 0)
                {
                    responseLevel = Math.Min(3, responseLevel);
                }

                responseLevel = Math.Min(4, responseLevel);

                if (string.IsNullOrEmpty(lastResourceId) && resourceIds.Count > 1)
                {
                    //При первом вызове метода парсера из DLL не известно о IdRes объявления внутри ресурса, поэтому он
                    //передает значение NULL. В этом случае парсер должен выбрать самое последнее объявление и
                    //вернуть его IdRes ресурса.
                    resourceIds.RemoveRange(0, resourceIds.Count - 1);
                }

                if (!string.IsNullOrEmpty(lastResourceId) && resourceIds.Any() &&
                    Comparer.Compare(resourceIds.Last(), lastResourceId) == 0)
                {
                    resourceIds.Clear();
                }


                if (!string.IsNullOrEmpty(lastResourceId) &&
                    (resourceIds.Count > Database.ConvertTo<long>(SiteProperties.CountAd)) &&
                    Comparer.Compare(resourceIds.First(), lastResourceId) > 0)
                {
                    //Если IdRes не найден, то парсер возвращает последнии CountAD
                    //объявлений (такая ситуация может получится, если размещенное объявление к следующей
                    //итерации будет удалено на ресурсе)
                    resourceIds.RemoveRange(0,
                        (int) (resourceIds.Count - Database.ConvertTo<long>(SiteProperties.CountAd)));
                }

                if (!string.IsNullOrEmpty(lastResourceId) && resourceIds.Any() &&
                    Comparer.Compare(resourceIds.First(), lastResourceId) == 0)
                {
                    resourceIds.Dequeue();
                }
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
                    string url = dictionary[resourceId].Key;
                    var builder = new UriBuilder(Uri.Combine(baseBuilder.Uri.ToString(), url))
                    {
                        UserName = baseBuilder.UserName,
                        Password = baseBuilder.Password,
                    };
                    var urlValues = new Values(dictionary[resourceId].Value)
                    {
                        Url = new List<string> {builder.Uri.ToString()}
                    };
                    IEnumerable<HtmlDocument> documents =
                        await
                            Crawler.WebRequestHtmlDocument(builder.Uri);
                    Thread.Sleep(0);
                    ReturnFields returnFields = Parser.BuildReturnFields(documents,
                        urlValues, ReturnFieldInfos);
                    publications.Add(Parser.CreateWebPublication(returnFields, requestId));
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
        ///     Sources – метод получает «рубрика, категория, регион», проверяет обрабатывается ли
        ///     такой набор исходных данных данной библиотекой и возвращает коллекцию названий
        ///     ресурсов (сайтов) в случае возможной обработки.
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

            if (!requestId.Select(pair => pair.Value != null).Aggregate((i, j) => i && j))
                return new List<string>();

            Mapping sites = Database.GetMapping(Database.SiteTable);

            return (from site in sites
                where
                    requestId.Select(pair => Database.GetScalar(pair.Value, pair.Key, site.Key) != null)
                        .Aggregate((i, j) => i && j)
                select site.Value.ToString()).ToList();
        }

        /// <summary>
        ///     Получить информацию о разработчике
        ///     About — возвращает информацию и координаты разработчика, а так же информацию о
        ///     передаче исключительных прав. Текст о передаче исключительных прав будет выслан
        ///     дополнительно.
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
            object siteId = Database.GetScalar(ModuleNamespace, Database.SiteIdColumn,
                Database.ModuleNamespaceColumn,
                Database.SiteTable);

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

        /// <summary>
        ///     KeysRubrics – возвращает коллекцию «ИД рубрик» в структуре сервиса, которые умеет
        ///     обрабатывать библиотека
        /// </summary>
        /// <returns></returns>
        public IList<Bind> KeysRubrics()
        {
            object siteId = Database.GetScalar(ModuleNamespace, Database.SiteIdColumn,
                Database.ModuleNamespaceColumn,
                Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return (from rubric in mappings.Rubric.Keys
                select new Bind
                {
                    RubricId = Database.ConvertTo<int>(rubric),
                }).ToList();
        }

        /// <summary>
        ///     KeysRegions – возвращает коллекцию «ИД регионов» в структуре сервиса, которые умеет
        ///     обрабатывать библиотека
        /// </summary>
        /// <returns></returns>
        public IList<Bind> KeysRegions()
        {
            object siteId = Database.GetScalar(ModuleNamespace, Database.SiteIdColumn,
                Database.ModuleNamespaceColumn,
                Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return (from region in mappings.Region.Keys
                select new Bind
                {
                    RegionId = Database.ConvertTo<int>(region),
                }).ToList();
        }

        /// <summary>
        ///     KeysActions – возвращает коллекцию «ИД действий» в структуре сервиса, которые умеет
        ///     обрабатывать библиотека
        /// </summary>
        /// <returns></returns>
        public IList<Bind> KeysActions()
        {
            object siteId = Database.GetScalar(ModuleNamespace, Database.SiteIdColumn,
                Database.ModuleNamespaceColumn,
                Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return (from action in mappings.Action.Keys
                select new Bind
                {
                    ActionId = Database.ConvertTo<int>(action)
                }).ToList();
        }
    }
}