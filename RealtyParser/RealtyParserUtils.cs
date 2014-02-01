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
    ///     Статичный класс вспомогательных алгоритмов
    /// </summary>
    public static class RealtyParserUtils
    {
        public const string FieldPattern = @"\{\{(?<name>[^\}]*)\}\}";

        /// <summary>
        ///     Единый коннектор к классу базы данных
        /// </summary>
        private static readonly RealtyParserDatabase Database = new RealtyParserDatabase();

        public static object LastError { get; set; }

        /// <summary>
        ///     Единый коннектор к классу базы данных
        /// </summary>
        public static RealtyParserDatabase GetDatabase()
        {
            return Database;
        }

        /// <summary>
        ///     Запрос к сайту с использованием RT.Crawler
        /// </summary>
        public static async Task<HtmlDocument[]> WebRequestHtmlDocument(Uri uri, string method, string encoding)
        {
            Debug.WriteLine(uri.ToString());
            ICrawler crawler = new WebCrawler();
            var requestWeb = (HttpWebRequest) WebRequest.Create(uri);
            requestWeb.Method = method;
            var collection = new List<HtmlDocument>();
            WebResponse responce = await crawler.GetResponse(requestWeb);
            if (responce != null)
            {
                Encoding encoder = Encoding.GetEncoding(encoding);
                var reader = new StreamReader(responce.GetResponseStream(), encoder);
                var input = new MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd()));
                var output = new MemoryStream();

                input.Seek(0, SeekOrigin.Begin);
                var firstEdition = new HtmlDocument();
                firstEdition.LoadHtml(new StreamReader(input, Encoding.UTF8).ReadToEnd());
                collection.Add(firstEdition);

                Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                input.Seek(0, SeekOrigin.Begin);
                Debug.WriteLine(new StreamReader(input).ReadToEnd());
                Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                input.Seek(0, SeekOrigin.Begin);

                Document tidy = Document.FromStream(input);
                tidy.ForceOutput = true;
                tidy.PreserveEntities = true;
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
                var secondEdition = new HtmlDocument();
                output.Seek(0, SeekOrigin.Begin);
                secondEdition.LoadHtml(new StreamReader(output, Encoding.UTF8).ReadToEnd());
                collection.Add(secondEdition);
            }
            return collection.ToArray();
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
                throw new NotImplementedException();
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
                webPublication.Contact.AuthorUrl =
                    ConvertToUris(returnFields.WebPublicationContactAuthorUrl).FirstOrDefault();
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
                        .Aggregate((i, j) => i + "\t" + j));
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
                    .Aggregate((i, j) => i + "\t" + j);
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
                        .Aggregate((i, j) => i + "\t" + j));
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
                    .Aggregate((i, j) => i + "\t" + j);
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

        public static string RegexEscape(string text)
        {
            var regex = new Regex(@"(\{|\[|\]|\})");
            return regex.Replace(text, @"\$&").Trim();
        }

        /// <summary>
        ///     Замена в строке-шаблоне идентификаторов-параметров на их значения
        /// </summary>
        public static List<string> ParseTemplate(string template, Arguments arguments)
        {
            Debug.WriteLine("Start ParseTemplate: " + template);
            Debug.WriteLine(arguments.ToString());
            var list = new List<string>();
            int maxCount = arguments.MaxCount;
            for (int index = 0; index < maxCount; index++)
            {
                string value = template.Trim();
                foreach (var pair in arguments.Where(pair => pair.Value != null && pair.Value.Count > index))
                    try
                    {
                        var regex = new Regex(pair.Key, RegexOptions.IgnoreCase);
                        value = regex.Replace(value, pair.Value[index]).Trim();
                    }
                    catch (Exception exception)
                    {
                        LastError = exception;
                        Debug.WriteLine(LastError.ToString());
                    }
                var rgx = new Regex(FieldPattern, RegexOptions.IgnoreCase);
                value = rgx.Replace(value, @"").Trim();
                if (!String.IsNullOrEmpty(value)) list.Add(value);
                Debug.WriteLine("ParseTemplate: " + template + " add value " + value);
            }
            return list;
        }

        /// <summary>
        ///     Поиск и формирование значений возвращаемых полей загруженного с сайта объявления
        /// </summary>
        public static ReturnFields BuildReturnFields(RealtyParserDatabase database, HtmlNode[] parentNodes,
            Arguments parentArguments, List<ReturnFieldInfo> returnFieldInfos)
        {
            var returnFields = new ReturnFields();
            foreach (ReturnFieldInfo returnFieldInfo in returnFieldInfos)
            {
                var agregated = new Arguments();
                foreach (HtmlNode parentNode in parentNodes)
                {
                    var arguments = new Arguments();
                    List<string> xpaths = ParseTemplate(returnFieldInfo.ReturnFieldXpathTemplate, parentArguments);
                    foreach (HtmlNode node in xpaths.Select(xpath => parentNode.SelectNodes(xpath)).Where(nodes => nodes != null).SelectMany(nodes => nodes))
                    {
                        arguments.InsertOrAppend(parentArguments);
                        arguments.InsertOrAppend(BuildArguments(returnFieldInfo.ReturnFieldResultTemplate, node));
                    }
                    foreach (KeyValuePair<string, List<string>> pair in arguments)
                        if (!agregated.ContainsKey((pair.Key))) agregated.Add(pair.Key, pair.Value);
                        else if (agregated[pair.Key].Count < pair.Value.Count)
                            agregated[pair.Key] = pair.Value;
                }
                var list = new List<string>();
                var regex = new Regex(returnFieldInfo.ReturnFieldRegexPattern,
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);
                foreach (string input in ParseTemplate(returnFieldInfo.ReturnFieldResultTemplate, agregated))
                {
                    string replace = regex.Replace(input, returnFieldInfo.ReturnFieldRegexReplacement).Trim();
                    if (!String.IsNullOrEmpty(replace)) list.Add(replace);
                    Debug.WriteLine("BuildReturnFields: add for " + returnFieldInfo.ReturnFieldId + " value " + input);
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
            Mapping mapping,
            long siteId)
        {
            var arguments = new Arguments();
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
                    arguments.Add(RegexEscape(@"{{" + mappingTable + @"Id}}"), mappingId[mappingTable]);
                    foreach (object userId in new object[] {id[mappingTable], mappingId[mappingTable]})
                    {
                        try
                        {
                            MethodInfo methodInfo = database.GetType()
                                .GetMethod("GetUserFields", new[] {userId.GetType(), typeof (string), typeof (long)});
                            var userFields =
                                (Dictionary<string, string>)
                                    methodInfo.Invoke(database, new[] {userId, mappingTable, siteId});
                            foreach (var field in userFields)
                            {
                                arguments.Add(RegexEscape(@"{{" + field.Key + @"}}"), field.Value);
                            }
                        }
                        catch (Exception exception)
                        {
                            LastError = exception;
                            Debug.WriteLine(LastError.ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    LastError = exception;
                    Debug.WriteLine(LastError.ToString());
                }
            }

            foreach (string mappingTable in new[] {"Region", "Rubric"})
            {
                try
                {
                    for (
                        long level = database.GetScalar<long, string>(mappingId[mappingTable], "Level", mappingTable,
                            siteId);
                        level > 0 && !String.IsNullOrEmpty(mappingId[mappingTable]);
                        level = database.GetScalar<long, string>(mappingId[mappingTable], "Level", mappingTable, siteId))
                    {
                        arguments.Add(RegexEscape(@"{{" + mappingTable + @"Id[" + level + @"]}}"),
                            mappingId[mappingTable]);

                        Dictionary<string, string> userFields = database.GetUserFields(mappingId[mappingTable],
                            mappingTable, siteId);
                        foreach (var field in userFields)
                        {
                            arguments.Add(RegexEscape(@"{{" + field.Key + @"[" + level + @"]}}"), field.Value);
                        }
                        mappingId[mappingTable] = database.GetScalar<string, string>(mappingId[mappingTable], "ParentId",
                            mappingTable, siteId);
                    }
                }
                catch (Exception exception)
                {
                    LastError = exception;
                    Debug.WriteLine(LastError.ToString());
                }
            }
            return arguments;
        }

        /// <summary>
        ///     Формирование пар идентификатор параметра <-> значение параметра
        ///     для замены в строке-шаблоне
        /// </summary>
        public static Arguments BuildArguments(long pageId)
        {
            var arguments = new Arguments
            {
                {RegexEscape(@"{{PageId}}"), (pageId > 1) ? pageId.ToString(CultureInfo.InvariantCulture) : @""}
            };
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
            var arguments = new Arguments();

            foreach (
                string name in
                    from Match match in Regex.Matches(template, FieldPattern)
                    select match.Groups["name"].Value)
            {
                Debug.WriteLine(template + " <- " + name);
                foreach (
                    MethodInfo methodInfo in
                        new[]
                        {
                            typeof (RealtyParserUtils).GetMethod("InvokeNodeProperty"),
                            typeof (RealtyParserUtils).GetMethod("AttributeValue")
                        })
                {
                    try
                    {
                        object value = methodInfo.Invoke(null, new object[] {node, name});
                        if (value != null) arguments.Add(RegexEscape(@"{{" + name + @"}}"), value.ToString());
                    }
                    catch (Exception exception)
                    {
                        LastError = exception;
                        Debug.WriteLine(LastError.ToString());
                    }
                }
            }

            return arguments;
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
                HtmlDocument[] documents = await WebRequestHtmlDocument(uri, "GET", encoding);
                var links = new LinksCollection();
                Debug.Assert(documents != null, "document != null");
                Debug.WriteLine("xpath: " + xpath);
                Debug.WriteLine("document: " + documents[0].DocumentNode.OuterHtml);
                HtmlNodeCollection nodes = documents[0].DocumentNode.SelectNodes(xpath);
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
                HtmlDocument[] documents = await WebRequestHtmlDocument(uri, "GET", encoding);
                var options = new OptionsCollection();
                Debug.Assert(documents != null, "document != null");
                Debug.WriteLine("xpath: " + xpath);
                Debug.WriteLine("document: " + documents[0].DocumentNode.OuterHtml);
                HtmlNodeCollection nodes = documents[0].DocumentNode.SelectNodes(xpath);
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
            HtmlAttribute attribute = node.Attributes[attributeName];
            if (attribute != null) return attribute.Value;
            return null;
        }

        /// <summary>
        ///     Получение значения указанного свойства указанного нода
        /// </summary>
        /// <param name="node"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string InvokeNodeProperty(HtmlNode node, string propertyName)
        {
            PropertyInfo propertyInfo = typeof (HtmlNode).GetProperty(propertyName);
            if (propertyInfo != null) return (string) propertyInfo.GetValue(node, null);
            return null;
        }

        #endregion
    }
}