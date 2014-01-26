using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RT.ParsingLibs;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;
using System.ComponentModel.Composition;

namespace RealtyParser
{
    [Export(typeof(IParsingModule))]
    [ExportMetadata("Name", "RealtyParserParsingModule")]
    public class RealtyParserParsingModule : IParsingModule
    {
        readonly RealtyParserDatabase _database = RealtyParserUtils.GetDatabase();

        /// <summary>
        /// Задача на парсинг
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
            List<ParseResponseCode> responseCode = new List<ParseResponseCode>
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

            Debug.WriteLine("Параметр LastPublicationId " + (string.IsNullOrEmpty(lastPublicationId) ? " ПУСТОЙ !!!!!!!" : " НЕ ПУСТОЙ !!!!!!!"));

            // Переопределение нулевых значений для тестовых целей
            //if (regionId == 0 && rubricId == 0 && actionId == 0)
            //{
            //    Debug.WriteLine("Переопределение нулевых значений для тестовых целей");
            //    siteId = 1;
            //    regionId = 1;
            //    rubricId = 1;
            //    actionId = 1;
            //}

            //if (regionId == 0) regionId = properties.Mapping.Region.Keys.FirstOrDefault();
            //if (rubricId == 0) rubricId = properties.Mapping.Rubric.Keys.FirstOrDefault();
            //if (actionId == 0) actionId = properties.Mapping.Action.Keys.FirstOrDefault();

            if (string.IsNullOrEmpty(lastPublicationId)) lastPublicationId = "";

            Debug.WriteLine("-------------------------------------------------------------------");
            Debug.WriteLine("Используются параметры:");
            Debug.WriteLine("siteId -> " + siteId);
            Debug.WriteLine("regionId -> " + regionId);
            Debug.WriteLine("rubricId -> " + rubricId);
            Debug.WriteLine("actionId -> " + actionId);
            Debug.WriteLine("lastPublicationId -> '" + lastPublicationId + "'");
            Debug.WriteLine("-------------------------------------------------------------------");


            Arguments args = RealtyParserUtils.BuildArguments(_database, regionId, rubricId, actionId, lastPublicationId, properties.Mapping, siteId);

            List<string> publishedIds = new List<string>();

            if (string.IsNullOrEmpty(lastPublicationId))
            {
                //При первом вызове метода парсера из DLL не известно о IdRes объявления внутри ресурса, поэтому он
                //передает значение NULL. В этом случае парсер должен выбрать самое последнее объявление и
                //вернуть его IdRes ресурса.
                UriBuilder builder =
                    new UriBuilder(properties.Url +
                                   RealtyParserUtils.ParseTemplate(properties.LastPublicationIdSearchTemplate, args))
                    {
                        UserName = properties.UserName,
                        Password = properties.Password
                    };
                HtmlDocument document =
                    await RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method, properties.Encoding);

                Regex regex = new Regex(properties.LastPublicationIdRegexPattern, RegexOptions.IgnoreCase);
                var nodes = document.DocumentNode.SelectNodes(
                    RealtyParserUtils.ParseTemplate(properties.LastPublicationIdXpathTemplate, args));
                if (nodes != null)
                {
                    foreach (HtmlNode node in nodes)
                    {
                        try
                        {
                            publishedIds.Add(
                                regex.Replace(
                                    RealtyParserUtils.ParseTemplate(properties.ExtResultTemplate,
                                        (new Arguments(args)).InsertOrReplaceArguments(
                                            RealtyParserUtils.BuildArguments(properties.LastPublicationIdResultTemplate, node))),
                                    properties.LastPublicationIdRegexReplacement));
                        }
                        catch (Exception)
                        {
                        }
                    }
                    publishedIds.Sort(comparer);
                    if (publishedIds.Count > 1) publishedIds.RemoveRange(0, publishedIds.Count - 1);
                }
                else
                {
                    Debug.WriteLine("document.DocumentNode.SelectSingleNode: No nodes found");
                    responceLevel = Math.Min(1, responceLevel);
                }
            }
            else
            {
                Debug.Assert(properties.CountAd != null, "properties.CountAd != null");
                int index;
                while (((index = publishedIds.BinarySearch(0, publishedIds.Count, lastPublicationId, comparer)) < 0) &&
                       publishedIds.Count < Convert.ToInt32(properties.CountAd))
                {

                    UriBuilder builder =
                        new UriBuilder(properties.Url +
                                       RealtyParserUtils.ParseTemplate(properties.ExtSearchTemplate,
                                           (new Arguments(args)).InsertOrReplaceArguments(
                                               RealtyParserUtils.BuildArguments(++pageId))))
                        {
                            UserName = properties.UserName,
                            Password = properties.Password
                        };
                    Debug.WriteLine("Получена страница " + builder.ToString());
                    HtmlDocument document =
                        await
                            RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method, properties.Encoding);
                    Regex regex = new Regex(properties.ExtRegexPattern, RegexOptions.IgnoreCase);
                    List<string> newIds = new List<string>();
                    var nodes = document.DocumentNode.SelectNodes(
                        RealtyParserUtils.ParseTemplate(properties.ExtXpathTemplate, args));
                    if (nodes != null)
                    {
                        foreach (HtmlNode node in nodes)
                        {
                            try
                            {
                                newIds.Add(
                                    regex.Replace(
                                        RealtyParserUtils.ParseTemplate(properties.ExtResultTemplate,
                                            (new Arguments(args)).InsertOrReplaceArguments(
                                                RealtyParserUtils.BuildArguments(properties.ExtResultTemplate, node))),
                                        properties.ExtRegexReplacement));
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    foreach (var item in newIds)
                        Debug.WriteLine("Найден идентификатор обхявления " + item);

                    if (!newIds.Any()) break;

                    publishedIds.AddRange(newIds);
                    publishedIds.Sort(comparer);

                }
                try
                {

                    if (index < 0)
                    {

                        responceLevel = Math.Min(2, responceLevel);

                        //Если IdRes не найден, то парсер возвращает последнии CountAD
                        //объявлений (такая ситуация может получится, если размещенное объявление к следующей
                        //итерации будет удалено на ресурсе)
                        publishedIds.RemoveRange(0,
                            (int)(publishedIds.Count - Convert.ToUInt32(properties.CountAd)));

                    }
                    else
                    {
                        //Одним из условий работы парсера является требование к оптимизации алгоритма
                        //получения новых объявлений, т.е. необходимо делать минимальное количество запросов
                        //к ресурсу. Для этого парсер должен использовать форму расширенного поиска, чтобы
                        //получать выборку нужных объявлений. Затем отсеивать объявления, которые были до
                        //IdRes и использовать дальнейшие ссылки только для новых объявлений.
                        publishedIds.RemoveRange(0, index + 1);
                    }
                }
                catch (Exception)
                {
                }
            }
            List<WebPublication> publications = new List<WebPublication>();

            foreach (var publishedId in publishedIds)
            {
                Arguments arguments = RealtyParserUtils.BuildArguments(_database, regionId, rubricId, actionId, publishedId,
                    properties.Mapping, siteId);
                UriBuilder builder =
                    new UriBuilder(properties.Url + RealtyParserUtils.ParseTemplate(properties.UnoSearchTemplate, arguments))
                    {
                        UserName = properties.UserName,
                        Password = properties.Password
                    };

                HtmlDocument document =
                    await
                        RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method, properties.Encoding);
                HtmlNode node = document.DocumentNode;

                if (node != null)
                {
                    ReturnFields returnFields = null;
                    try
                    {
                        returnFields = RealtyParserUtils.BuildReturnFields(_database, node, arguments,
                            properties.ReturnFieldInfos);
                    }
                    catch (Exception)
                    {
                        returnFields = new ReturnFields();
                    }

                    Debug.WriteLine("{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{");
                    foreach (var field in returnFields)
                        if (field.Value != null)
                            foreach (var value in field.Value)
                                Debug.WriteLine(field.Key + " -> " + value);
                    Debug.WriteLine("}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}");

                    publications.Add(RealtyParserUtils.CreateWebPublication(returnFields, regionId, rubricId, actionId, builder));
                }
                else
                {
                    Debug.WriteLine("unoDocument.DocumentNode.SelectSingleNode: No nodes found");
                }
                lastPublicationId = publishedId;
            }
            if (!publications.Any())
                responceLevel = Math.Min(3, responceLevel);

            Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            foreach (var publication in publications)
            {
                Debug.WriteLine("{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{");
                Debug.WriteLine(publication.ToString());
                Debug.WriteLine("}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}");
            }
            Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            return new ParseResponse
            {
                ResponseCode = responseCode[responceLevel],
                LastPublicationId = lastPublicationId,
                ModuleName = GetType().ToString(),
                Publications = publications
            };
        }

        /// <summary>
        /// Получить названия ресурсов, обрабатываемая библиотекой
        /// </summary>
        /// <param name="bind">Бинд запроса</param>
        /// <returns> Коллекция названий ресурсов (сайтов)</returns>
        public IList<string> Sources(Bind bind)
        {
            long regionId = bind.RegionId;
            long rubricId = bind.RubricId;
            long actionId = bind.ActionId;

            // Переопределение нулевых значений для тестовых целей
            if (regionId == 0) regionId = _database.GetDictionary<int>("Region").Keys.ToList().FirstOrDefault();
            if (rubricId == 0) rubricId = _database.GetDictionary<int>("Rubric").Keys.ToList().FirstOrDefault();
            if (actionId == 0) actionId = _database.GetDictionary<int>("Action").Keys.ToList().FirstOrDefault();

            Dictionary<long, string> sites = _database.GetDictionary<long>("Site");
            List<string> list = new List<string>();
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
                catch (Exception)
                {
                }
            }
            return list;
        }

        /// <summary>
        /// Получить информацию о разработчике
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
        /// Получить список биндов, обрабатываемая библиотекой
        /// </summary>
        /// <returns>Коллекция биндов</returns>
        public IList<Bind> Keys()
        {

            List<Bind> keys = new List<Bind>();

            const long siteId = 2;
            SiteProperties properties = _database.GetSiteProperties(siteId);
            Mapping mapping = properties.Mapping;
            List<long> regions = mapping.Region.Keys.ToList();
            List<long> rubrics = mapping.Rubric.Keys.ToList();
            List<long> actions = mapping.Action.Keys.ToList();

            //regions.Sort();
            //rubrics.Sort();
            //actions.Sort();

            //keys.Add(new Bind() { RegionId = (int)regions.First(), RubricId = (int)rubrics.First(), ActionId = (int)actions.First() });
            //keys.Add(new Bind() { RegionId = (int)regions.Last(), RubricId = (int)rubrics.Last(), ActionId = (int)actions.Last() });

            foreach (var region in regions)
                foreach (var rubric in rubrics)
                    foreach (var action in actions)
                        keys.Add(new Bind
                        {
                            RegionId = (int)region,
                            RubricId = (int)rubric,
                            ActionId = (int)action
                        });
            return keys;
        }
    }
}
