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
            long siteId = 2;

            long regionId = request.RegionId;
            long rubricId = request.RubricId;
            long actionId = request.ActionId;
            string lastPublicationId = request.LastPublicationId;

            // Переопределение нулевых значений для тестовых целей
            if (regionId == 0 && rubricId == 0 && actionId == 0)
            {
                siteId = 1;
                regionId = 1;
                rubricId = 1;
                actionId = 1;
            }

            SiteProperties properties = _database.GetSiteProperties(siteId);

            if (regionId == 0) regionId = properties.Mapping.Region.Keys.FirstOrDefault();
            if (rubricId == 0) rubricId = properties.Mapping.Rubric.Keys.FirstOrDefault();
            if (actionId == 0) actionId = properties.Mapping.Action.Keys.FirstOrDefault();

            if (string.IsNullOrEmpty(lastPublicationId)) lastPublicationId = "";

            Debug.Assert(properties.Mapping.Action.ContainsKey(actionId));
            Debug.Assert(properties.Mapping.Rubric.ContainsKey(rubricId));
            Debug.Assert(properties.Mapping.Region.ContainsKey(regionId));

            Arguments args = RealtyParserUtils.BuildArgs(_database, regionId, rubricId, actionId, lastPublicationId, properties.Mapping, siteId);

            List<string> publishedIds = publishedIds = new List<string>();
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
                HtmlNode node =
                    document.DocumentNode.SelectSingleNode(
                        RealtyParserUtils.ParseTemplate(properties.LastPublicationIdXpathTemplate, args));
                if (node != null)
                {
                    Regex regex = new Regex(properties.LastPublicationIdRegexPattern, RegexOptions.IgnoreCase);
                    publishedIds = new List<string>
                    {
                        regex.Replace(
                            RealtyParserUtils.ParseTemplate(properties.LastPublicationIdResultTemplate,
                                RealtyParserUtils.BuildArgs(node)), properties.LastPublicationIdRegexReplacement)
                    };
                }
                else
                {
                    return new ParseResponse
                    {
                        ResponseCode = ParseResponseCode.NotFoundId,
                        LastPublicationId = null,
                        ModuleName = GetType().ToString(),
                        Publications = null
                    };
                }
            }

            List<WebPublication> publications = new List<WebPublication>();

            foreach (var publishedId in publishedIds)
            {
                Arguments unoArgs = RealtyParserUtils.BuildArgs(_database, regionId, rubricId, actionId, publishedId, properties.Mapping, siteId);
                UriBuilder unoBuilder = new UriBuilder(properties.Url + RealtyParserUtils.ParseTemplate(properties.UnoSearchTemplate, unoArgs))
                {
                    UserName = properties.UserName,
                    Password = properties.Password
                };
                HtmlDocument unoDocument = await RealtyParserUtils.WebRequestHtmlDocument(unoBuilder.Uri, properties.Method, properties.Encoding);
                ReturnFields unoReturnFields = null;
                try
                {
                    unoReturnFields = RealtyParserUtils.BuildReturnFields(_database,
                        unoDocument.DocumentNode.SelectSingleNode(RealtyParserUtils.ParseTemplate(properties.UnoXpathTemplate, unoArgs)),
                        unoArgs,
                        properties.ReturnFieldInfos);
                }
                catch (Exception)
                {
                    unoReturnFields = new ReturnFields();
                }
                publications.Add(RealtyParserUtils.CreateWebPublication(unoReturnFields, regionId, rubricId, actionId, unoBuilder));
                lastPublicationId = publishedId;
            }

            return new ParseResponse
            {
                ResponseCode = (publishedIds.Count()>0) ? ParseResponseCode.Success : ParseResponseCode.ContentEmpty,
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
            // Переопределение нулевых значений для тестовых целей
            if (bind.RegionId == 0) bind.RegionId = _database.GetDictionary<int>("Region").Keys.ToList().FirstOrDefault();
            if (bind.RubricId == 0) bind.RubricId = _database.GetDictionary<int>("Rubric").Keys.ToList().FirstOrDefault();
            if (bind.ActionId == 0) bind.ActionId = _database.GetDictionary<int>("Action").Keys.ToList().FirstOrDefault();

            Dictionary<long, string> sites = _database.GetDictionary<long>("Site");
            List<string> list = new List<string>();
            foreach (var site in sites)
            {
                SiteProperties properties = _database.GetSiteProperties(site.Key);
                try
                {
                    if (!String.IsNullOrEmpty(properties.Mapping.Region[bind.RegionId]) &&
                        !String.IsNullOrEmpty(properties.Mapping.Rubric[bind.RubricId]) &&
                        !String.IsNullOrEmpty(properties.Mapping.Action[bind.ActionId]))
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
            List<long> sites = _database.GetDictionary<long>("Site").Keys.ToList();
            List<SiteProperties> sitePropertiesCollection = sites.Select(site => _database.GetSiteProperties(site)).ToList();

            List<long> regions = _database.GetDictionary<long>("Region").Keys.ToList();
            List<long> rubrics = _database.GetDictionary<long>("Rubric").Keys.ToList();
            List<long> actions = _database.GetDictionary<long>("Action").Keys.ToList();
            List<Bind> keys = new List<Bind>();

            long total = 0;
            foreach (var region in regions)
                foreach (var rubric in rubrics)
                    keys.AddRange(from action in actions
                                  where
                                      sitePropertiesCollection.Any(
                                          item =>
                                              item.Mapping.Action.ContainsKey(action) &&
                                              item.Mapping.Rubric.ContainsKey(rubric) &&
                                              item.Mapping.Region.ContainsKey(region) &&
                                              total++ < 1000)
                                  select new Bind
                                  {
                                      RegionId = (int)region,
                                      RubricId = (int)rubric,
                                      ActionId = (int)action
                                  });
            return keys;
        }
    }
}
