using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RT.Crawler;
using RT.ParsingLibs.Models;
using TidyManaged;

namespace RealtyParser
{
    /// <summary>
    ///     Статический класс вспомогательных алгоритмов
    /// </summary>
    public static class RealtyParserUtils
    {
        /// <summary>
        ///     Единый коннектор к классу базы данных
        /// </summary>
        private static readonly RealtyParserDatabase Database = new RealtyParserDatabase();

        /// <summary>
        ///     Единый коннектор к классу базы данных
        /// </summary>
        public static RealtyParserDatabase GetDatabase()
        {
            return Database;
        }

        /// <summary>
        ///     Запрос к сайту с использованием RT.Crawler
        ///     Вынесено сюда только чтобы удалить дублирование кода
        /// </summary>
        public static async Task<HtmlDocument> WebRequestHtmlDocument(Uri uri, string method, string encoding)
        {
            Debug.WriteLine(uri.ToString());
            ICrawler crawler = new WebCrawler();
            var requestWeb = (HttpWebRequest) WebRequest.Create(uri);
            requestWeb.Method = method;
            WebResponse responce = await crawler.GetResponse(requestWeb);
            if (responce != null)
            {
                Encoding encoder = Encoding.GetEncoding(encoding);
                var reader = new StreamReader(responce.GetResponseStream(), encoder);
                var input = new MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd()));
                var output = new MemoryStream();
                Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                input.Seek(0, SeekOrigin.Begin);
                Debug.WriteLine(new StreamReader(input).ReadToEnd());
                Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                input.Seek(0, SeekOrigin.Begin);
                Document tidy = Document.FromStream(input);
                tidy.ForceOutput = true;
                tidy.InputCharacterEncoding = EncodingType.Raw;
                tidy.OutputCharacterEncoding = EncodingType.Raw;
                tidy.CharacterEncoding = EncodingType.Raw;
                tidy.ShowWarnings = false;
                tidy.Quiet = true;
                tidy.OutputXhtml = true;
                tidy.CleanAndRepair();
                tidy.Save(output);
                Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                output.Seek(0, SeekOrigin.Begin);
                Debug.WriteLine(new StreamReader(output, Encoding.UTF8).ReadToEnd());
                Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                var document = new HtmlDocument();
                output.Seek(0, SeekOrigin.Begin);
                document.LoadHtml(new StreamReader(output, Encoding.UTF8).ReadToEnd());
                return document;
            }
            return new HtmlDocument();
        }

        /// <summary>
        ///     Создание экземпляра указанного класса, реализующего интерфейс IComparer<string>
        /// </summary>
        public static IComparer<string> CreatePublicationIdComparer(string className)
        {
            try
            {
                return
                    Activator.CreateInstance(Type.GetType("RealtyParser.PublicationIdComparer." + className)) as
                        IComparer<string>;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        /// <summary>
        ///     Создание инстанса WebPublication на основе разобранных текстровых полей объявления
        /// </summary>
        public static WebPublication CreateWebPublication(
            ReturnFields returnFields,
            long regionId,
            long rubricId,
            long actionId,
            UriBuilder builder
            )
        {
            var webPublication = new WebPublication
            {
                AdditionalInfo = new AdditionalInfo
                {
                    RealtyAdditionalInfo = new RealtyAdditionalInfo()
                },
                Contact = new WebPublicationContact()
            };

            Debug.Assert(webPublication != null);
            Debug.Assert(webPublication.AdditionalInfo != null);
            Debug.Assert(webPublication.AdditionalInfo.RealtyAdditionalInfo != null);
            Debug.Assert(webPublication.Contact != null);

            try
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.Address =
                    returnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoAddress
                        .Aggregate((i, j) => i + "\t" + j);
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.Address = "";
            }
            try
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.District =
                    returnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoDistrict
                        .Aggregate((i, j) => i + "\t" + j);
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.District = "";
            }
            try
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.AppointmentOfRoom =
                    returnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoAppointmentOfRoom
                        .Aggregate((i, j) => i + "\t" + j);
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.AppointmentOfRoom = "";
            }
            try
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.CostAll =
                    Convert.ToDecimal(
                        returnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoCostAll
                            .Aggregate((i, j) => i + j));
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.CostAll = 0m;
            }
            try
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.TotalSpace =
                    Convert.ToInt32(
                        returnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoTotalSpace
                            .Aggregate((i, j) => i + j));
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.TotalSpace = 0;
            }
            try
            {
                webPublication.Contact.Author = returnFields.WebPublicationContactAuthor
                    .Aggregate((i, j) => i + "\t" + j);
            }
            catch (Exception)
            {
                webPublication.Contact.Author = "";
            }
            try
            {
                webPublication.Contact.AuthorUrl = ConvertToUris(returnFields.WebPublicationContactAuthorUrl)
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                webPublication.Contact.AuthorUrl = null;
            }
            try
            {
                webPublication.Contact.ContactName = returnFields.WebPublicationContactContactName
                    .Aggregate((i, j) => i + "\t" + j);
            }
            catch (Exception)
            {
                webPublication.Contact.ContactName = "";
            }
            try
            {
                webPublication.Contact.Email = returnFields.WebPublicationContactEmail;
            }
            catch (Exception)
            {
                webPublication.Contact.Email = new List<string>();
            }
            try
            {
                webPublication.Contact.Icq = Convert.ToUInt32(
                    returnFields.WebPublicationContactIcq
                        .FirstOrDefault());
            }
            catch (Exception)
            {
                webPublication.Contact.Icq = 0;
            }
            try
            {
                webPublication.Contact.Phone = returnFields.WebPublicationContactPhone;
            }
            catch (Exception)
            {
                webPublication.Contact.Phone = new List<string>();
            }
            try
            {
                webPublication.Contact.Skype = returnFields.WebPublicationContactSkype
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                webPublication.Contact.Skype = "";
            }
            try
            {
                webPublication.Description = returnFields.WebPublicationDescription
                    .Aggregate((i, j) => i + "\t" + j);
            }
            catch (Exception)
            {
                webPublication.Description = "";
            }
            try
            {
                webPublication.ModifyDate = DateTime.Parse(
                    returnFields.WebPublicationModifyDate
                        .FirstOrDefault());
            }
            catch (Exception)
            {
                webPublication.ModifyDate = DateTime.Now;
            }
            try
            {
                webPublication.Photos = ConvertToUris(returnFields.WebPublicationPhotos);
            }
            catch (Exception)
            {
                webPublication.Photos = new List<Uri>();
            }
            try
            {
                webPublication.PublicationId = returnFields.WebPublicationPublicationId
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                webPublication.PublicationId = "";
            }
            try
            {
                webPublication.RegionId = regionId;
            }
            catch (Exception)
            {
                webPublication.RegionId = 0;
            }
            try
            {
                webPublication.RubricId = rubricId;
            }
            catch (Exception)
            {
                webPublication.RubricId = 0;
            }
            try
            {
                webPublication.ActionId = actionId;
            }
            catch (Exception)
            {
                webPublication.ActionId = 0;
            }
            try
            {
                webPublication.Site = new Uri(builder.Scheme + @"://" + builder.Host);
            }
            catch (Exception)
            {
                webPublication.Site = null;
            }
            try
            {
                webPublication.Url = builder.Uri;
            }
            catch (Exception)
            {
                webPublication.Url = null;
            }

            return webPublication;
        }

        /// <summary>
        ///     Конвертация списка строк в список Uri
        /// </summary>
        public static IList<Uri> ConvertToUris(List<string> strings)
        {
            return strings.Select(s => new Uri(s)).ToList();
        }

        /// <summary>
        ///     Замена в строке-шаблоне идентификаторов-параметров на их значения
        /// </summary>
        public static string ParseTemplate(string template, Arguments args)
        {
            foreach (var pair in args)
            {
                Debug.Assert(pair.Key != null);
                Debug.Assert(pair.Value != null);

                Debug.WriteLine("ParseTemplate: /" + pair.Key + "/ -> /" +
                                pair.Value.Substring(0, Math.Min(30, pair.Value.Length)) +
                                ((pair.Value.Length > 30) ? ".../" : "/"));
                var regex = new Regex(pair.Key, RegexOptions.IgnoreCase);
                template = regex.Replace(template, pair.Value);
            }
            var rgx = new Regex(@"\{\{[^\}]*\}\}", RegexOptions.IgnoreCase);
            template = rgx.Replace(template, @"");
            Debug.WriteLine("ParseTemplate: " + template);
            return template;
        }

        /// <summary>
        ///     Поиск и формирование значений возвращаемых полей загруженного с сайта объявления
        /// </summary>
        public static ReturnFields BuildReturnFields(
            RealtyParserDatabase database,
            HtmlNode parentNode,
            Arguments parentArguments,
            List<ReturnFieldInfo> returnFieldInfos)
        {
            var returnFields = new ReturnFields();
            foreach (ReturnFieldInfo returnFieldInfo in returnFieldInfos)
            {
                var regex = new Regex(returnFieldInfo.ReturnFieldRegexPattern, RegexOptions.IgnoreCase);

                HtmlNodeCollection nodes =
                    parentNode.SelectNodes(ParseTemplate(returnFieldInfo.ReturnFieldXpathTemplate,
                        (new Arguments(parentArguments)).InsertOrReplaceArguments(
                            BuildArguments(returnFieldInfo.ReturnFieldXpathTemplate, parentNode))));

                var list = new List<string>();
                if (nodes != null)
                {
                    foreach (HtmlNode node in nodes)
                    {
                        string value = regex.Replace(
                            ParseTemplate(returnFieldInfo.ReturnFieldResultTemplate,
                                (new Arguments(parentArguments)).InsertOrReplaceArguments(
                                    BuildArguments(returnFieldInfo.ReturnFieldResultTemplate, node))),
                            returnFieldInfo.ReturnFieldRegexReplacement);

                        list.Add(value);
                        Debug.WriteLine("BuildReturnFields: " + returnFieldInfo.ReturnFieldId + " -> " + value);
                    }
                }
                else
                {
                    Debug.WriteLine("BuildReturnFields: parentNode.SelectNodes: No nodes found");
                }
                returnFields.Add(returnFieldInfo.ReturnFieldId, list);
            }
            return returnFields;
        }

        /// <summary>
        ///     Формирование пар идентификатор параметра <-> значение параметра
        ///     для замены в строке-шаблоне
        /// </summary>
        public static Arguments BuildArguments(
            RealtyParserDatabase database,
            long regionId,
            long rubricId,
            long actionId,
            string publicationId,
            Mapping mapping,
            long siteId)
        {
            var args = new Arguments
            {
                {@"\{\{PublicationId\}\}", publicationId}
            };
            var id = new Dictionary<string, long>
            {
                {"Action", actionId},
                {"Region", regionId},
                {"Rubric", rubricId}
            };

            var mappingId = new Dictionary<string, string>();

            foreach (string mappingTable in id.Keys)
            {
                try
                {
                    mappingId.Add(mappingTable, mapping[mappingTable][id[mappingTable]]);
                    args.Add(@"\{\{" + mappingTable + @"Id\}\}", mappingId[mappingTable]);
                    try
                    {
                        Dictionary<string, string> userFields = database.GetUserFields(id[mappingTable], mappingTable,
                            siteId);
                        foreach (var field in userFields)
                        {
                            try
                            {
                                string key = @"\{\{" + field.Key + @"\}\}";
                                if (!args.ContainsKey(key)) args.Add(key, field.Value);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        Dictionary<string, string> userFields = database.GetUserFields(mappingId[mappingTable],
                            mappingTable, siteId);
                        foreach (var field in userFields)
                        {
                            try
                            {
                                string key = @"\{\{" + field.Key + @"\}\}";
                                if (!args.ContainsKey(key)) args.Add(key, field.Value);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                catch (Exception)
                {
                }
            }

            foreach (string mappingTable in new List<string> {"Region", "Rubric"})
            {
                try
                {
                    for (
                        long level = database.GetScalar<long, string>(mappingId[mappingTable], "Level", mappingTable,
                            siteId);
                        level > 0 && !String.IsNullOrEmpty(mappingId[mappingTable]);
                        level = database.GetScalar<long, string>(mappingId[mappingTable], "Level", mappingTable, siteId))
                    {
                        string key = @"\{\{" + mappingTable + @"Id\[" + level + @"\]\}\}";
                        if (!args.ContainsKey(key)) args.Add(key, mappingId[mappingTable]);

                        Dictionary<string, string> userFields = database.GetUserFields(mappingId[mappingTable],
                            mappingTable, siteId);
                        foreach (var field in userFields)
                        {
                            try
                            {
                                key = @"\{\{" + field.Key + @"\[" + level + @"\]\}\}";
                                if (!args.ContainsKey(key)) args.Add(key, field.Value);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        mappingId[mappingTable] = database.GetScalar<string, string>(mappingId[mappingTable], "ParentId",
                            mappingTable, siteId);
                    }
                }
                catch (Exception)
                {
                }
            }
            return args;
        }

        /// <summary>
        ///     Формирование пар идентификатор параметра <-> значение параметра
        ///     для замены в строке-шаблоне
        /// </summary>
        public static Arguments BuildArguments(long pageId)
        {
            var arguments = new Arguments();
            if (pageId > 1) arguments.Add(@"\{\{PageId\}\}", pageId.ToString(CultureInfo.InvariantCulture));
            return arguments;
        }

        /// <summary>
        ///     Формирование пар идентификатор параметра <-> значение параметра
        ///     для замены в строке-шаблоне
        /// </summary>
        /// <param name="template"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Arguments BuildArguments(string template, HtmlNode node)
        {
            Debug.Assert(node != null);
            try
            {
                var args = new Arguments();

                var regex = new Regex(@"\{\{[^\}]+\}\}");
                foreach (Match match in regex.Matches(template))
                {
                    string name = match.Value.Replace(@"{{", @"").Replace(@"}}", @"");
                    Debug.WriteLine(template + " <- " + name);
                    try
                    {
                        args.Add(@"\{\{" + name + @"\}\}", InvokeNodeProperty(node, name));
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        args.Add(@"\{\{" + name + @"\}\}", AttributeValue(node, name));
                    }
                    catch (Exception)
                    {
                    }
                }

                return args;
            }
            catch (Exception)
            {
                return new Arguments();
            }
        }

        /// <summary>
        ///     Не входит в техническое задание
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="xpath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<LinksCollection> GetLinks(Uri uri, string xpath, string encoding)
        {
            try
            {
                HtmlDocument document = await WebRequestHtmlDocument(uri, "GET", encoding);
                var links = new LinksCollection();
                Debug.Assert(document != null, "document != null");
                Debug.WriteLine("xpath: " + xpath);
                Debug.WriteLine("document: " + document.DocumentNode.OuterHtml);
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    string link = AttributeValue(node, "href");
                    string value = node.InnerText;
                    Debug.WriteLine(link + "->" + value);
                    if (!String.IsNullOrEmpty(link) && !links.ContainsKey(link)) links.Add(link, value);
                }
                return links;
            }
            catch (Exception)
            {
                return new LinksCollection();
            }
        }

        /// <summary>
        ///     Не входит в техническое задание
        /// </summary>
        public static async Task<OptionsCollection> GetOptions(Uri uri, string xpath, string encoding)
        {
            try
            {
                HtmlDocument document = await WebRequestHtmlDocument(uri, "GET", encoding);
                var options = new OptionsCollection();
                Debug.Assert(document != null, "document != null");
                Debug.WriteLine("xpath: " + xpath);
                Debug.WriteLine("document: " + document.DocumentNode.OuterHtml);
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    string option = AttributeValue(node, "value");
                    string value = node.NextSibling.InnerText;
                    Debug.WriteLine(option + "->" + value);
                    if (!String.IsNullOrEmpty(option) && !options.ContainsKey(option)) options.Add(option, value);
                }
                return options;
            }
            catch (Exception)
            {
                return new OptionsCollection();
            }
        }

        /// <summary>
        ///     Не входит в техническое задание
        /// </summary>
        public static Dictionary<long, string> BuildMapping(Dictionary<long, string> lefts,
            Dictionary<string, string> rights, string tableName, long siteId)
        {
            Debug.Assert(rights.Count > 0);
            var results = new Dictionary<long, string>();
            foreach (var left in lefts)
            {
                string a = left.Value;
                var builder = new StringBuilder();
                do
                {
                    builder.AppendLine(a);
                } while
                    (
                    tableName != "Action" &&
                    Database.GetScalar<long, string>(a, "Level", tableName) > 1 &&
                    !string.IsNullOrEmpty(a = Database.GetScalar<string, string>(a, "ParentId", tableName))
                    );
                a = builder.ToString();


                string b = rights.First().Value;
                builder = new StringBuilder();
                do
                {
                    builder.AppendLine(b);
                } while
                    (
                    tableName != "Action" &&
                    Database.GetScalar<long, string>(b, "Level", tableName, siteId) > 1 &&
                    !string.IsNullOrEmpty(b = Database.GetScalar<string, string>(b, "ParentId", tableName, siteId))
                    );
                b = builder.ToString();

                string index = rights.First().Key;
                int distance = LevenshteinDistance.Compute(a, b);
                foreach (var right in rights.Where(right => index != right.Key))
                {
                    b = right.Value;
                    builder = new StringBuilder();
                    do
                    {
                        builder.AppendLine(b);
                    } while
                        (
                        tableName != "Action" &&
                        Database.GetScalar<long, string>(b, "Level", tableName, siteId) > 1 &&
                        !string.IsNullOrEmpty(b = Database.GetScalar<string, string>(b, "ParentId", tableName, siteId))
                        );
                    b = builder.ToString();

                    int current = LevenshteinDistance.Compute(a, b);
                    if (current < distance)
                    {
                        distance = current;
                        index = right.Key;
                    }
                }
                results.Add(left.Key, index);
            }
            return results;
        }

        #region Получение значения параметра

        /// <summary>
        ///     Получение значения указанного аттрибута указанного нода
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string AttributeValue(HtmlNode node, string attributeName)
        {
            try
            {
                HtmlAttribute attribute = node.Attributes[attributeName];
                return attribute.Value;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        ///     Получение значения указанного свойства указанного нода
        /// </summary>
        /// <param name="node"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string InvokeNodeProperty(HtmlNode node, string propertyName)
        {
            Type type = typeof (HtmlNode);
            Debug.Assert(type != null, "type != null");
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            return (string) propertyInfo.GetValue(node, null);
        }

        #endregion
    }
}