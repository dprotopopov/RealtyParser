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
            string lastPublicationId = request.LastPublicationId;
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
                    LastPublicationId = lastPublicationId,
                    ModuleName = GetType().ToString(),
                    Publications = new List<WebPublication>()
                };
            }

            Debug.WriteLine("Параметр LastPublicationId " +
                            (string.IsNullOrEmpty(lastPublicationId) ? " ПУСТОЙ !!!!!!!" : " НЕ ПУСТОЙ !!!!!!!"));

            if (string.IsNullOrEmpty(lastPublicationId)) lastPublicationId = "";

            Debug.WriteLine("-------------------------------------------------------------------");
            Debug.WriteLine("Используются параметры:");
            Debug.WriteLine("siteId -> " + siteId);
            Debug.WriteLine("regionId -> " + regionId);
            Debug.WriteLine("rubricId -> " + rubricId);
            Debug.WriteLine("actionId -> " + actionId);
            Debug.WriteLine("lastPublicationId -> '" + lastPublicationId + "'");
            Debug.WriteLine("-------------------------------------------------------------------");


            Arguments siteArguments = RealtyParserUtils.BuildArguments(_database, regionId, rubricId, actionId,
                properties.Mapping, siteId);

            var baseBuilder = new UriBuilder(properties.Url)
            {
                UserName = properties.UserName,
                Password = properties.Password,
            };


            var publicationIds = new List<string>();
            int index = -1;
            while ((++pageId > 0 && publicationIds.Count < Convert.ToInt32(properties.CountAd) &&
                    (!lastPublicationId.IsNullOrEmpty()) &&
                    (index = publicationIds.BinarySearch(0, publicationIds.Count, lastPublicationId, comparer)) < 0) ||
                   (lastPublicationId.IsNullOrEmpty() && pageId == 1))
            {
                Arguments pageArguments =
                    new Arguments(siteArguments).InsertOrReplace(RealtyParserUtils.BuildArguments(pageId));
                Debug.WriteLine(pageArguments.ToString());
                int count = 0;
                foreach (string url in RealtyParserUtils.ParseTemplate(properties.ExtSearchTemplate, pageArguments))
                {
                    Debug.WriteLine(url);
                    try
                    {
                        var builder = new UriBuilder(baseBuilder + url);
                        HtmlDocument[] documents =
                            await
                                RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method,
                                    properties.Encoding);
                        var nodes = documents.Select(document => document.DocumentNode).ToArray();
                        ReturnFields returnFields = RealtyParserUtils.BuildReturnFields(_database, nodes,
                            pageArguments, properties.ReturnFieldInfos);
                        publicationIds.AddRange(returnFields.PublicationId);
                        count += returnFields.PublicationId.Count;
                    }
                    catch (Exception exception)
                    {
                        _lastError = exception;
                        Debug.WriteLine(_lastError.ToString());
                    }
                }
                if (count == 0) break;
                publicationIds.Sort(comparer);
            }
            try
            {
                if (string.IsNullOrEmpty(lastPublicationId) && publicationIds.Count == 0)
                {
                    responceLevel = Math.Min(1, responceLevel);
                }
                else if (string.IsNullOrEmpty(lastPublicationId) && publicationIds.Count > 0)
                {
                    //При первом вызове метода парсера из DLL не известно о IdRes объявления внутри ресурса, поэтому он
                    //передает значение NULL. В этом случае парсер должен выбрать самое последнее объявление и
                    //вернуть его IdRes ресурса.
                    publicationIds.RemoveRange(0, publicationIds.Count - 1);
                }
                else if (index < 0)
                {
                    responceLevel = Math.Min(2, responceLevel);

                    //Если IdRes не найден, то парсер возвращает последнии CountAD
                    //объявлений (такая ситуация может получится, если размещенное объявление к следующей
                    //итерации будет удалено на ресурсе)
                    publicationIds.RemoveRange(0,
                        (int) (publicationIds.Count - Convert.ToUInt32(properties.CountAd)));
                }
                else if (index == publicationIds.Count - 1)
                {
                    //Одним из условий работы парсера является требование к оптимизации алгоритма
                    //получения новых объявлений, т.е. необходимо делать минимальное количество запросов
                    //к ресурсу. Для этого парсер должен использовать форму расширенного поиска, чтобы
                    //получать выборку нужных объявлений. Затем отсеивать объявления, которые были до
                    //IdRes и использовать дальнейшие ссылки только для новых объявлений.
                    responceLevel = Math.Min(3, responceLevel);
                    publicationIds.Clear();
                }
                else if (index < publicationIds.Count - 1)
                {
                    //Одним из условий работы парсера является требование к оптимизации алгоритма
                    //получения новых объявлений, т.е. необходимо делать минимальное количество запросов
                    //к ресурсу. Для этого парсер должен использовать форму расширенного поиска, чтобы
                    //получать выборку нужных объявлений. Затем отсеивать объявления, которые были до
                    //IdRes и использовать дальнейшие ссылки только для новых объявлений.
                    responceLevel = Math.Min(4, responceLevel);
                    publicationIds.RemoveRange(0, index + 1);
                }
            }
            catch (Exception exception)
            {
                _lastError = exception;
                Debug.WriteLine(_lastError.ToString());
            }
            var publications = new List<WebPublication>();
            {
                var arguments = new Arguments {{RealtyParserUtils.RegexEscape("{{PublicationId}}"), publicationIds}};
                for (int i = arguments.PublicationId.Count; i-- > 0;) arguments.InsertOrAppend(siteArguments);
                foreach (string url in RealtyParserUtils.ParseTemplate(properties.UnoSearchTemplate, arguments))
                {
                    try
                    {
                        var builder = new UriBuilder(baseBuilder + url);
                        HtmlDocument[] documents =
                            await
                                RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method,
                                    properties.Encoding);
                        var nodes = documents.Select(document => document.DocumentNode).ToArray();
                        ReturnFields returnFields = RealtyParserUtils.BuildReturnFields(_database, nodes,
                            siteArguments, properties.ReturnFieldInfos);
                        publications.Add(RealtyParserUtils.CreateWebPublication(returnFields, regionId, rubricId,
                            actionId, builder));
                    }
                    catch (Exception exception)
                    {
                        _lastError = exception;
                        Debug.WriteLine(_lastError.ToString());
                    }
                }
            }

            if (!publications.Any()) responceLevel = Math.Min(3, responceLevel);

            return new ParseResponse
            {
                ResponseCode = responseCode[responceLevel],
                LastPublicationId = publicationIds.DefaultIfEmpty(lastPublicationId).LastOrDefault(),
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