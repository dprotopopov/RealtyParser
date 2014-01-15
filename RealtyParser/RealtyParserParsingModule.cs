using System;
using System.Collections.Generic;
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
            const long siteId = 2;
            long regionId = request.RegionId;
            long rubricId = request.RubricId;
            long actionId = request.ActionId;
            string lastPublicationId = request.LastPublicationId;

            SiteProperties properties = _database.GetSiteProperties(siteId);
            Arguments args = RealtyParserUtils.BuildArgs(_database, regionId, rubricId, actionId, lastPublicationId, properties.Mapping);


            List<string> publishedIds;
            if (string.IsNullOrEmpty(lastPublicationId))
            {
                //При первом вызове метода парсера из DLL не известно о IdRes объявления внутри ресурса, поэтому он
                //передает значение NULL. В этом случае парсер должен выбрать самое последнее объявление и
                //вернуть его IdRes ресурса.
                UriBuilder builder = new UriBuilder(properties.Url + RealtyParserUtils.ParseTemplate(properties.LastPublicationIdSearchTemplate, args))
                {
                    UserName = properties.UserName,
                    Password = properties.Password
                };
                HtmlDocument document = await RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method, properties.Encoding);
                HtmlNode node =
                    document.DocumentNode.SelectSingleNode(
                        RealtyParserUtils.ParseTemplate(properties.LastPublicationIdXpathTemplate, args));

                Regex regex = new Regex(properties.LastPublicationIdRegexPattern, RegexOptions.IgnoreCase);
                publishedIds = new List<string>
                {
                    regex.Replace(RealtyParserUtils.ParseTemplate(properties.LastPublicationIdResultTemplate,RealtyParserUtils.BuildArgs(node)),properties.LastPublicationIdRegexReplacement)
                };
            }
            else
            {
                UriBuilder builder = new UriBuilder(properties.Url + RealtyParserUtils.ParseTemplate(properties.ExtSearchTemplate, args))
                {
                    UserName = properties.UserName,
                    Password = properties.Password
                };
                HtmlDocument document = await RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method, properties.Encoding);
                IComparer<string> comparer =
                    RealtyParserUtils.CreatePublicationIdComparer(properties.PublicationIdComparerClassName);
                Regex regex = new Regex(properties.ExtRegexPattern, RegexOptions.IgnoreCase);
                publishedIds = document.DocumentNode.SelectNodes(RealtyParserUtils.ParseTemplate(properties.ExtXpathTemplate, args)).Select(node => regex.Replace(RealtyParserUtils.ParseTemplate(properties.ExtResultTemplate,
                    RealtyParserUtils.BuildArgs(node)), properties.ExtRegexReplacement)).ToList();
                publishedIds.Sort(comparer);
                int index = publishedIds.BinarySearch(0, publishedIds.Count, lastPublicationId, comparer);
                try
                {

                    if (index < 0)
                    {
                        //Если IdRes не найден, то парсер возвращает последнии CountAD
                        //объявлений (такая ситуация может получится, если размещенное объявление к следующей
                        //итерации будет удалено на ресурсе)
                        publishedIds.RemoveRange(0, (int)(publishedIds.Count - Convert.ToUInt32(properties.CountAd)));
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
                catch (ArgumentException)
                {
                }

            }


            List<WebPublication> publications = new List<WebPublication>();
            foreach (var publishedId in publishedIds)
            {
                Arguments unoArgs = RealtyParserUtils.BuildArgs(_database, regionId, rubricId, actionId, publishedId, properties.Mapping);
                UriBuilder unoBuilder = new UriBuilder(properties.Url + RealtyParserUtils.ParseTemplate(properties.UnoSearchTemplate, unoArgs))
                {
                    UserName = properties.UserName,
                    Password = properties.Password
                };
                HtmlDocument unoDocument = await RealtyParserUtils.WebRequestHtmlDocument(unoBuilder.Uri, properties.Method, properties.Encoding);
                ReturnFields unoReturnFields = RealtyParserUtils.BuildReturnFields(_database,
                    unoDocument.DocumentNode.SelectSingleNode(RealtyParserUtils.ParseTemplate(properties.UnoXpathTemplate, unoArgs)),
                    unoArgs,
                    properties.ReturnFieldInfos);
                publications.Add(RealtyParserUtils.CreateWebPublication(unoReturnFields, regionId, rubricId, actionId, unoBuilder));
                lastPublicationId = publishedId;
            }

            return new ParseResponse
            {
                ResponseCode = ParseResponseCode.Success,
                LastPublicationId = lastPublicationId,
                ModuleName = GetType().ToString(),
                Publications = publications.ToArray()
            };
        }

        /// <summary>
        /// Получить названия ресурсов, обрабатываемая библиотекой
        /// </summary>
        /// <param name="bind">Бинд запроса</param>
        /// <returns> Коллекция названий ресурсов (сайтов)</returns>
        public IList<string> Sources(Bind bind)
        {
            try
            {
                Dictionary<long, string> sites = _database.GetDictionary<long>("Site");
                return (from site in sites let properties = _database.GetSiteProperties(site.Key) where !String.IsNullOrEmpty(properties.Mapping["Region"][bind.RegionId]) && !String.IsNullOrEmpty(properties.Mapping["Rubric"][bind.RubricId]) && !String.IsNullOrEmpty(properties.Mapping["Action"][bind.ActionId]) select site.Value).ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
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
            List<long> regions = _database.GetDictionary<long>("Region").Keys.ToList();
            List<long> rubrics = _database.GetDictionary<long>("Rubric").Keys.ToList();
            List<long> actions = _database.GetDictionary<long>("Action").Keys.ToList();
            List<Bind> keys = new List<Bind>();
            foreach (var region in regions)
                foreach (var rubric in rubrics)
                    keys.AddRange(actions.Select(action => new Bind { RegionId = (int)region, RubricId = (int)rubric, ActionId = (int)action }));
            return keys;
        }
    }
}
