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
    public class Parser
    {
        private const string FieldPattern = @"\{\{(?<name>[^\}]*)\}\}";

        private object LastError { get; set; }

        /// <summary>
        ///     Запрос к сайту с использованием RT.Crawler
        /// </summary>
        public async Task<HtmlDocument[]> WebRequestHtmlDocument(Uri uri, string method, string encoding)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine(uri.ToString());
            ICrawler crawler = new WebCrawler();
            var requestWeb = (HttpWebRequest) WebRequest.Create(uri);
            requestWeb.Method = method;
            var collection = new List<HtmlDocument>();
            WebResponse responce = await crawler.GetResponse(requestWeb);
            if (responce != null)
            {
                Encoding encoder = Encoding.GetEncoding(encoding);
                Stream responceStream = responce.GetResponseStream();
                if (responceStream != null)
                {
                    var reader = new StreamReader(responceStream, encoder);
                    var memoryStreams = new List<MemoryStream>
                    {
                        new MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd()))
                    };

                    Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                    memoryStreams.Last().Seek(0, SeekOrigin.Begin);
                    Debug.WriteLine(new StreamReader(memoryStreams.Last()).ReadToEnd());
                    Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

                    memoryStreams.First().Seek(0, SeekOrigin.Begin);
                    Document tidy = Document.FromStream(memoryStreams.First());

                    tidy.ForceOutput = true;
                    tidy.PreserveEntities = true;
                    tidy.InputCharacterEncoding = EncodingType.Raw;
                    tidy.OutputCharacterEncoding = EncodingType.Raw;
                    tidy.CharacterEncoding = EncodingType.Raw;
                    tidy.ShowWarnings = false;
                    tidy.Quiet = true;
                    tidy.OutputXhtml = true;
                    tidy.CleanAndRepair();

                    memoryStreams.Add(new MemoryStream());
                    tidy.Save(memoryStreams.Last());

                    Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                    memoryStreams.Last().Seek(0, SeekOrigin.Begin);
                    Debug.WriteLine(new StreamReader(memoryStreams.Last(), Encoding.UTF8).ReadToEnd());
                    Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

                    foreach (MemoryStream stream in memoryStreams)
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        var edition = new HtmlDocument();
                        edition.LoadHtml(new StreamReader(stream, Encoding.UTF8).ReadToEnd());
                        collection.Add(edition);
                    }
                }
            }
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return collection.ToArray();
        }

        /// <summary>
        ///     Создание инстанса WebPublication на основе разобранных текстровых полей объявления
        /// </summary>
        public WebPublication CreateWebPublication(
            ReturnFields returnFields,
            RequestProperties id,
            UriBuilder builder
            )
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
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
                        .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.Address = "";
            }
            try
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.District =
                    returnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoDistrict
                        .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.District = "";
            }
            try
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.AppointmentOfRoom =
                    returnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoAppointmentOfRoom
                        .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j));
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
                    .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.Contact.Author = "";
            }
            try
            {
                webPublication.Contact.AuthorUrl =
                    String.ConvertToUri(returnFields.WebPublicationContactAuthorUrl).FirstOrDefault();
            }
            catch (Exception)
            {
                webPublication.Contact.AuthorUrl = null;
            }
            try
            {
                webPublication.Contact.ContactName = returnFields.WebPublicationContactContactName
                    .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j));
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
                        .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j)));
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
                    .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.Contact.Skype = "";
            }
            try
            {
                webPublication.Description = returnFields.WebPublicationDescription
                    .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.Description = "";
            }
            try
            {
                webPublication.ModifyDate = DateTime.Parse(
                    returnFields.WebPublicationModifyDate
                        .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j)));
            }
            catch (Exception)
            {
                webPublication.ModifyDate = System.DateTime.Now;
            }
            try
            {
                webPublication.Photos = String.ConvertToUri(returnFields.WebPublicationPhotos);
            }
            catch (Exception)
            {
                webPublication.Photos = new List<Uri>();
            }
            try
            {
                webPublication.PublicationId = returnFields.WebPublicationPublicationId
                    .Aggregate((i, j) => System.String.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.PublicationId = "";
            }
            try
            {
                webPublication.RegionId = Database.ConvertTo<long>(id.Region);
            }
            catch (Exception)
            {
                webPublication.RegionId = 0;
            }
            try
            {
                webPublication.RubricId = Database.ConvertTo<long>(id.Rubric);
            }
            catch (Exception)
            {
                webPublication.RubricId = 0;
            }
            try
            {
                webPublication.ActionId = Database.ConvertTo<long>(id.Action);
            }
            catch (Exception)
            {
                webPublication.ActionId = 0;
            }
            try
            {
                webPublication.Site = new Uri(System.String.Format("{0}://{1}", builder.Scheme, builder.Host));
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
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return webPublication;
        }

        /// <summary>
        ///     Замена в строке-шаблоне идентификаторов-параметров на их значения
        /// </summary>
        public List<string> ParseTemplate(string template, Values values)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var list = new List<string>();
            int maxCount = values.MaxCount;
            for (int[] index = {0}; index[0] < maxCount; index[0]++)
            {
                string value = template.Trim();
                foreach (var pair in values.Where(pair => pair.Value != null && pair.Value.Count > index[0]))
                {
                    var regex = new System.Text.RegularExpressions.Regex(pair.Key, RegexOptions.IgnoreCase);
                    value = regex.Replace(value, pair.Value[index[0]]).Trim();
                }
                var rgx = new System.Text.RegularExpressions.Regex(FieldPattern, RegexOptions.IgnoreCase);
                value = rgx.Replace(value, @"").Trim();
                if (!System.String.IsNullOrEmpty(value)) list.Add(value);
                Debug.WriteLine("ParseTemplate: {0} add value {1}", template, value);
            }
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return list;
        }

        /// <summary>
        ///     Поиск и формирование значений возвращаемых полей загруженного с сайта объявления
        /// </summary>
        public ReturnFields BuildReturnFields(HtmlNode[] parentNodes,
            Values parentValues, IEnumerable<ReturnFieldInfo> returnFieldInfos)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var returnFields = new ReturnFields();
            foreach (ReturnFieldInfo returnFieldInfo in returnFieldInfos)
            {
                var agregated = new Values();
                foreach (HtmlNode parentNode in parentNodes)
                {
                    var values = new Values();
                    List<string> xpaths = ParseTemplate(returnFieldInfo.ReturnFieldXpathTemplate.ToString(),
                        parentValues);
                    HtmlNode node = parentNode;
                    foreach (
                        HtmlNode htmlNode in
                            xpaths.Select(xpath => node != null ? node.SelectNodes(xpath) : null)
                                .Where(nodes => nodes != null)
                                .SelectMany(nodes => nodes))
                    {
                        values.InsertOrAppend(parentValues);
                        values.InsertOrAppend(BuildValues(returnFieldInfo.ReturnFieldResultTemplate.ToString(), htmlNode));
                    }
                    foreach (var pair in values)
                        if (!agregated.ContainsKey((pair.Key))) agregated.Add(pair.Key, pair.Value);
                        else if (agregated[pair.Key].Count < pair.Value.Count)
                            agregated[pair.Key] = pair.Value;
                }
                var regex = new System.Text.RegularExpressions.Regex(returnFieldInfo.ReturnFieldRegexPattern.ToString(),
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);
                List<string> list =
                    ParseTemplate(returnFieldInfo.ReturnFieldResultTemplate.ToString(), agregated)
                        .Select(
                            input => regex.Replace(input, returnFieldInfo.ReturnFieldRegexReplacement.ToString()).Trim())
                        .Where(replace => !System.String.IsNullOrEmpty(replace))
                        .SelectMany(
                            replace =>
                                (from Match match in
                                    System.Text.RegularExpressions.Regex.Matches(replace,
                                        returnFieldInfo.ReturnFieldRegexMatchPattern.ToString())
                                    select match.Value.Trim()))
                        .Where(value => !System.String.IsNullOrEmpty(value))
                        .ToList();
                returnFields.Add(returnFieldInfo.ReturnFieldId.ToString(), list);
            }
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return returnFields;
        }

        /// <summary>
        ///     Формирование пар идентификатор параметра - значение параметра
        ///     для замены в строке-шаблоне
        /// </summary>
        public Values BuildValues(
            Database database,
            RequestProperties id,
            RequestProperties mappingId)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine("id {0}", id);
            Debug.WriteLine("mappingId {0}", mappingId);
            IEnumerable<string> mapping = database.GetList("Mapping", "TableName").Select(item => item.ToString());
            IEnumerable<string> hierarchical =
                database.GetList("Hierarchical", "TableName").Select(item => item.ToString());
            Debug.WriteLine("mapping {0}", mapping);
            Debug.WriteLine("hierarchical {0}", hierarchical);
            var values = new Values
            {
                {
                    mappingId.Keys.Select(item => Regex.Escape(System.String.Format(@"{{{{{0}}}}}", item))).ToList(),
                    mappingId.Values.Select(item => item.ToString()).ToList()
                }
            };

            foreach (string mappingTable in mapping)
            {
                Debug.Assert(id[mappingTable] != null && !System.String.IsNullOrEmpty(id[mappingTable].ToString()));
                Debug.Assert(mappingId[mappingTable] != null &&
                             !System.String.IsNullOrEmpty(mappingId[mappingTable].ToString()));
                Dictionary<string, object> userFields = database.GetUserFields(id[mappingTable], mappingId[mappingTable],
                    mappingTable, id.Site);
                Debug.WriteLine(mappingTable);
                List<string> keys =
                    userFields.Keys.Select(item => Regex.Escape(System.String.Format(@"{{{{{0}}}}}", item))).ToList();
                List<string> list = userFields.Values.Select(item => item.ToString()).ToList();
                var mappingTableValues = new Values {{keys, list}};
                values.InsertOrReplace(mappingTableValues);
            }

            foreach (string mappingTable in hierarchical)
            {
                for (
                    object level = database.GetScalar(mappingId[mappingTable], "Level", mappingTable, mappingId.Site);
                    Database.ConvertTo<long>(level) > 0;
                    level = database.GetScalar(mappingId[mappingTable], "Level", mappingTable, mappingId.Site))
                {
                    Debug.Assert(id[mappingTable] != null && !System.String.IsNullOrEmpty(id[mappingTable].ToString()));
                    Debug.Assert(mappingId[mappingTable] != null &&
                                 !System.String.IsNullOrEmpty(mappingId[mappingTable].ToString()));
                    Dictionary<string, object> mappingUserFields = database.GetUserFields(id[mappingTable],
                        mappingId[mappingTable],
                        mappingTable, id.Site);

                    var mappingTableValues = new Values
                    {
                        {
                            Regex.Escape(System.String.Format(@"{{{{{0}[{1}]}}}}", mappingTable, level)),
                            mappingId[mappingTable].ToString()
                        },
                        {
                            mappingUserFields.Keys.Select(
                                item => Regex.Escape(System.String.Format(@"{{{{{0}[{1}]}}}}", item, level))).ToList(),
                            mappingUserFields.Values.Select(item => item.ToString()).ToList()
                        }
                    };
                    values.InsertOrReplace(mappingTableValues);

                    mappingId[mappingTable] = database.GetScalar(mappingId[mappingTable], "ParentId",
                        mappingTable, mappingId.Site);
                    if (mappingId[mappingTable] == null ||
                        System.String.IsNullOrEmpty(mappingId[mappingTable].ToString())) break;
                }
            }

            Debug.WriteLine("values {0}", values);

            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return values;
        }

        /// <summary>
        ///     Формирование пар идентификатор параметра - значение параметра
        ///     для замены в строке-шаблоне
        /// </summary>
        public Values BuildValues(long pageId)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var values = new Values
            {
                {Regex.Escape(@"{{Page}}"), (pageId > 1) ? pageId.ToString(CultureInfo.InvariantCulture) : @""}
            };
            Debug.WriteLine("values {0}", values);
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return values;
        }

        /// <summary>
        ///     Формирование пар идентификатор параметра - значение параметра
        ///     для замены в строке-шаблоне
        /// </summary>
        /// <param name="template"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private Values BuildValues(string template, HtmlNode node)
        {
            Debug.Assert(node != null);
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var values = new Values();

            foreach (
                string name in
                    from Match match in System.Text.RegularExpressions.Regex.Matches(template, FieldPattern)
                    select match.Groups["name"].Value)
            {
                Debug.WriteLine("{0} <- {1}", template, name);
                foreach (
                    MethodInfo methodInfo in
                        new[]
                        {
                            typeof (Parser).GetMethod("InvokeNodeProperty"),
                            typeof (Parser).GetMethod("AttributeValue")
                        })
                {
                    try
                    {
                        object value = methodInfo.Invoke(null, new object[] {node, name});
                        if (value != null)
                            values.Add(Regex.Escape(System.String.Format(@"{{{{{0}}}}}", name)), value.ToString());
                    }
                    catch (Exception exception)
                    {
                        LastError = exception;
                        Debug.WriteLine(LastError.ToString());
                    }
                }
            }
            Debug.WriteLine("values {0}", values);
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return values;
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