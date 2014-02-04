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
        private readonly RealtyParserDatabase _database = RealtyParserUtils.GetDatabase();

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
            Debug.WriteLine(request.ToString());
            Debug.WriteLine("request.RegionId -> " + request.RegionId);
            Debug.WriteLine("request.RubricId -> " + request.RubricId);
            Debug.WriteLine("request.ActionId -> " + request.ActionId);
            Debug.WriteLine("request.LastPublicationId -> '" + request.LastPublicationId + "'");
            Debug.WriteLine("-------------------------------------------------------------------");

            const long siteId = 2;
            SiteProperties properties = _database.GetSiteProperties(siteId);
            IComparer<string> comparer =
                RealtyParserUtils.CreatePublicationIdComparer(properties.PublicationIdComparerClassName);


            long regionId = request.RegionId;
            long rubricId = request.RubricId;
            long actionId = request.ActionId;
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

            if (!properties.Mapping.Action.ContainsKey(actionId) || !properties.Mapping.Rubric.ContainsKey(rubricId) ||
                !properties.Mapping.Region.ContainsKey(regionId))
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
                            (string.IsNullOrEmpty(lastResourceId) ? " ПУСТОЙ !!!!!!!" : " НЕ ПУСТОЙ !!!!!!!"));

            if (string.IsNullOrEmpty(lastResourceId)) lastResourceId = "";

            Debug.WriteLine("-------------------------------------------------------------------");
            Debug.WriteLine("Используются параметры:");
            Debug.WriteLine("siteId -> " + siteId);
            Debug.WriteLine("regionId -> " + regionId);
            Debug.WriteLine("rubricId -> " + rubricId);
            Debug.WriteLine("actionId -> " + actionId);
            Debug.WriteLine("lastPublicationId -> '" + lastResourceId + "'");
            Debug.WriteLine("-------------------------------------------------------------------");


            ParametersValues siteParametersValues = RealtyParserUtils.BuildParametersValues(_database, regionId,
                rubricId, actionId,
                properties.Mapping, siteId);

            var baseBuilder = new UriBuilder(properties.Url)
            {
                UserName = properties.UserName,
                Password = properties.Password,
            };

            var resourceIds = new List<string>();
            var dictionary = new Dictionary<string, string>();
            while ((++pageId > 0 && resourceIds.Count < Convert.ToInt32(properties.CountAd) &&
                    ((!lastResourceId.IsNullOrEmpty()) &&
                     (resourceIds.Count == 0 || comparer.Compare(resourceIds.First(), lastResourceId) > 0)) ||
                    (lastResourceId.IsNullOrEmpty() && pageId == 1)))
            {
                ParametersValues pageParametersValues =
                    new ParametersValues(siteParametersValues).InsertOrReplace(
                        RealtyParserUtils.BuildParametersValues(pageId));
                Debug.WriteLine(pageParametersValues.ToString());
                int count = 0;
                foreach (
                    string url in RealtyParserUtils.ParseTemplate(properties.ExtSearchTemplate, pageParametersValues)
                    )
                {
                    Debug.WriteLine(url);
                    try
                    {
                        var builder = new UriBuilder(baseBuilder + url);
                        HtmlDocument[] documents =
                            await
                                RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method,
                                    properties.Encoding);
                        HtmlNode[] nodes = documents.Select(document => document.DocumentNode).ToArray();
                        ReturnFields returnFields = RealtyParserUtils.BuildReturnFields(_database, nodes,
                            pageParametersValues, properties.ReturnFieldInfos);
                        var arguments = new ParametersValues(returnFields);
                        List<string> keys = RealtyParserUtils.ParseTemplate(properties.ResourceIdTemplate, arguments);
                        List<string> values = RealtyParserUtils.ParseTemplate(properties.UnoSearchTemplate, arguments);
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
                    responceLevel = Math.Min(1, responceLevel);
                }
                else if (string.IsNullOrEmpty(lastResourceId) && resourceIds.Count > 0)
                {
                    //При первом вызове метода парсера из DLL не известно о IdRes объявления внутри ресурса, поэтому он
                    //передает значение NULL. В этом случае парсер должен выбрать самое последнее объявление и
                    //вернуть его IdRes ресурса.
                    resourceIds.RemoveRange(0, resourceIds.Count - 1);
                }
                else if (!string.IsNullOrEmpty(lastResourceId) && resourceIds.BinarySearch(lastResourceId, comparer) < 0)
                {
                    responceLevel = Math.Min(2, responceLevel);

                    //Если IdRes не найден, то парсер возвращает последнии CountAD
                    //объявлений (такая ситуация может получится, если размещенное объявление к следующей
                    //итерации будет удалено на ресурсе)
                    resourceIds.RemoveRange(0,
                        (int) (resourceIds.Count - Convert.ToUInt32(properties.CountAd)));
                }

                if (!string.IsNullOrEmpty(lastResourceId) && resourceIds.Count > 0 &&
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
                else if (!string.IsNullOrEmpty(lastResourceId))
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
                    (int) (resourceIds.Count - Convert.ToUInt32(properties.CountAd)));
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
                            RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method,
                                properties.Encoding);
                    HtmlNode[] nodes = documents.Select(document => document.DocumentNode).ToArray();
                    ReturnFields returnFields = RealtyParserUtils.BuildReturnFields(_database, nodes,
                        siteParametersValues, properties.ReturnFieldInfos);
                    publications.Add(RealtyParserUtils.CreateWebPublication(returnFields, regionId, rubricId,
                        actionId, builder));
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
            long regionId = bind.RegionId;
            long rubricId = bind.RubricId;
            long actionId = bind.ActionId;

            Dictionary<long, string> sites = _database.GetDictionary<long>("Site");
            var list = new List<string>();
            foreach (var site in sites)
            {
                SiteProperties properties = _database.GetSiteProperties(site.Key);
                try
                {
                    if (!String.IsNullOrEmpty(properties.Mapping.Region[regionId]) &&
                        !String.IsNullOrEmpty(properties.Mapping.Rubric[rubricId]) &&
                        !String.IsNullOrEmpty(properties.Mapping.Action[actionId]))
                        list.Add(site.Value);
                }
                catch (Exception exception)
                {
                    _lastError = exception;
                    Debug.WriteLine(_lastError.ToString());
                }
            }
            return list;
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
            var keys = new List<Bind>();

            const long siteId = 2;
            SiteProperties properties = _database.GetSiteProperties(siteId);
            Mapping mapping = properties.Mapping;
            List<long> regions = mapping.Region.Keys.ToList();
            List<long> rubrics = mapping.Rubric.Keys.ToList();
            List<long> actions = mapping.Action.Keys.ToList();

            foreach (long region in regions)
                foreach (long rubric in rubrics)
                    foreach (long action in actions)
                        keys.Add(new Bind
                        {
                            RegionId = (int) region,
                            RubricId = (int) rubric,
                            ActionId = (int) action
                        });
            return keys;
        }
    }
}