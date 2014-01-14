using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            const long siteId = 1;
            long regionId = request.RegionId;
            long rubricId = request.RubricId;
            long actionId = request.ActionId;
            string lastPublicationId = request.LastPublicationId;

            SiteProperties properties = _database.GetProperties(siteId);
            Arguments args = RealtyParserUtils.BuildArgs(_database, regionId, rubricId, actionId, lastPublicationId, siteId);


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
                HtmlAgilityPack.HtmlDocument document = await RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method);
                publishedIds = new List<string>
                {
                    RealtyParserUtils.InvokeNodeProperty(
                        document.DocumentNode.SelectSingleNode(RealtyParserUtils.ParseTemplate(properties.LastPublicationIdSearchXpathTemplate, args)),
                        properties.LastPublicationIdSearchNodePropertyName)
                };
            }
            else
            {
                UriBuilder builder = new UriBuilder(properties.Url + RealtyParserUtils.ParseTemplate(properties.ExtSearchTemplate, args))
                {
                    UserName = properties.UserName,
                    Password = properties.Password
                };
                HtmlAgilityPack.HtmlDocument document = await RealtyParserUtils.WebRequestHtmlDocument(builder.Uri, properties.Method);
                string propertyName = properties.ExtSearchNodePropertyName;
                IComparer<string> comparer =
                    RealtyParserUtils.CreatePublicationIdComparer(properties.PublicationIdComparerClassName);
                publishedIds = document.DocumentNode.SelectNodes(RealtyParserUtils.ParseTemplate(properties.ExtSearchXpathTemplate, args)).Select(node => RealtyParserUtils.InvokeNodeProperty(node, propertyName)).ToList();
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
                Arguments unoArgs = RealtyParserUtils.BuildArgs(_database, regionId, rubricId, actionId, publishedId, siteId);
                UriBuilder unoBuilder = new UriBuilder(properties.Url + RealtyParserUtils.ParseTemplate(properties.UnoSearchTemplate, unoArgs))
                {
                    UserName = properties.UserName,
                    Password = properties.Password
                };
                HtmlAgilityPack.HtmlDocument unoDocument = await RealtyParserUtils.WebRequestHtmlDocument(unoBuilder.Uri, properties.Method);
                ReturnFields unoReturnFields = RealtyParserUtils.BuildReturnFields(_database,
                    unoDocument.DocumentNode.SelectSingleNode(RealtyParserUtils.ParseTemplate(properties.UnoSearchXpathTemplate, unoArgs)), unoArgs, siteId);
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
            Dictionary<long, string> sites = _database.GetTable("Site");
            foreach (var site in sites.Where(site => String.IsNullOrEmpty(_database.Mapping(site.Key, bind.RegionId, "Region")) || String.IsNullOrEmpty(_database.Mapping(site.Key, bind.RubricId, "Rubric")) || String.IsNullOrEmpty(_database.Mapping(site.Key, bind.ActionId, "Action"))))
            {
                sites.Remove(site.Key);
            }
            return sites.Values.ToList();
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
            List<long> regions = _database.GetTable("Region").Keys.ToList();
            List<long> rubrics = _database.GetTable("Rubric").Keys.ToList();
            List<long> actions = _database.GetTable("Action").Keys.ToList();
            List<Bind> keys = new List<Bind>();
            foreach (var region in regions)
                foreach (var rubric in rubrics)
                    keys.AddRange(actions.Select(action => new Bind { RegionId = (int)region, RubricId = (int)rubric, ActionId = (int)action }));
            return keys;
        }
    }
}
