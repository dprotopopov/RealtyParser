using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    ///     Класс вспомогательных алгоритмов
    /// </summary>
    public class Parser
    {
        private const char SplitChar = '\\';
        public Transformation Transformation { get; set; }
        public ProgressCallback ProgressCallback { get; set; }
        public AppendLineCallback AppendLineCallback { get; set; }
        public CompliteCallback CompliteCallback { get; set; }
        private object LastError { get; set; }

        /// <summary>
        ///     Запрос к сайту с использованием RT.Crawler
        /// </summary>
        public async Task<HtmlDocument[]> WebRequestHtmlDocument(Uri uri, string method, string encoding)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine(uri.ToString());
            long current = 0;
            long total = 1;
            ICrawler crawler = new WebCrawler();
            var requestWeb = (HttpWebRequest) WebRequest.Create(uri);
            requestWeb.Method = method;
            var collection = new List<HtmlDocument>();
            WebResponse responce = await crawler.GetResponse(requestWeb);
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
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
                    if (ProgressCallback != null) ProgressCallback(++current, ++total);

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

                    if (ProgressCallback != null) ProgressCallback(++current, ++total);

                    foreach (MemoryStream stream in memoryStreams)
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        var edition = new HtmlDocument();
                        edition.LoadHtml(new StreamReader(stream, Encoding.UTF8).ReadToEnd());
                        collection.Add(edition);
                    }
                    if (ProgressCallback != null) ProgressCallback(++current, total);
                }
            }
            if (CompliteCallback != null) CompliteCallback();
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
            long current = 0;
            long total = 1;
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
                        .Aggregate((i, j) => string.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.Address = "";
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.District =
                    returnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoDistrict
                        .Aggregate((i, j) => string.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.District = "";
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.AppointmentOfRoom =
                    returnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoAppointmentOfRoom
                        .Aggregate((i, j) => string.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.AdditionalInfo.RealtyAdditionalInfo.AppointmentOfRoom = "";
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
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
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
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
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Contact.Author = returnFields.WebPublicationContactAuthor
                    .Aggregate((i, j) => string.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.Contact.Author = "";
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Contact.AuthorUrl =
                    String.ConvertToUri(returnFields.WebPublicationContactAuthorUrl).FirstOrDefault();
            }
            catch (Exception)
            {
                webPublication.Contact.AuthorUrl = null;
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Contact.ContactName = returnFields.WebPublicationContactContactName
                    .Aggregate((i, j) => string.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.Contact.ContactName = "";
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Contact.Email = returnFields.WebPublicationContactEmail;
            }
            catch (Exception)
            {
                webPublication.Contact.Email = new List<string>();
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Contact.Icq = Convert.ToUInt32(
                    returnFields.WebPublicationContactIcq
                        .Aggregate((i, j) => string.Format("{0}\t{1}", i, j)));
            }
            catch (Exception)
            {
                webPublication.Contact.Icq = 0;
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Contact.Phone = returnFields.WebPublicationContactPhone;
            }
            catch (Exception)
            {
                webPublication.Contact.Phone = new List<string>();
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Contact.Skype = returnFields.WebPublicationContactSkype
                    .Aggregate((i, j) => string.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.Contact.Skype = "";
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Description = returnFields.WebPublicationDescription
                    .Aggregate((i, j) => string.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.Description = "";
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.ModifyDate = DateTime.Parse(
                    returnFields.WebPublicationModifyDate
                        .Aggregate((i, j) => string.Format("{0}\t{1}", i, j)));
            }
            catch (Exception)
            {
                webPublication.ModifyDate = System.DateTime.Now;
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Photos = String.ConvertToUri(returnFields.WebPublicationPhotos);
            }
            catch (Exception)
            {
                webPublication.Photos = new List<Uri>();
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.PublicationId = returnFields.WebPublicationPublicationId
                    .Aggregate((i, j) => string.Format("{0}\t{1}", i, j));
            }
            catch (Exception)
            {
                webPublication.PublicationId = "";
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.RegionId = Database.ConvertTo<long>(id.Region);
            }
            catch (Exception)
            {
                webPublication.RegionId = 0;
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.RubricId = Database.ConvertTo<long>(id.Rubric);
            }
            catch (Exception)
            {
                webPublication.RubricId = 0;
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.ActionId = Database.ConvertTo<long>(id.Action);
            }
            catch (Exception)
            {
                webPublication.ActionId = 0;
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Site = new Uri(string.Format("{0}://{1}", builder.Scheme, builder.Host));
            }
            catch (Exception)
            {
                webPublication.Site = null;
            }
            if (ProgressCallback != null) ProgressCallback(++current, ++total);
            try
            {
                webPublication.Url = builder.Uri;
            }
            catch (Exception)
            {
                webPublication.Url = null;
            }
            if (ProgressCallback != null) ProgressCallback(++current, total);
            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return webPublication;
        }

        /// <summary>
        ///     Поиск и формирование значений возвращаемых полей загруженного с сайта объявления
        /// </summary>
        public ReturnFields BuildReturnFields(HtmlNode[] parentNodes,
            Values parentValues, ReturnFieldInfos returnFieldInfos)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var returnFields = new ReturnFields();
            long current = 0;
            long total = returnFieldInfos.Count;
            foreach (ReturnFieldInfo returnFieldInfo in returnFieldInfos.Values)
            {
                var agregated = new Values();
                foreach (HtmlNode parentNode in parentNodes)
                {
                    var values = new Values();
                    List<string> xpaths =
                        Transformation.ParseTemplate(returnFieldInfo.ReturnFieldXpathTemplate.ToString(),
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
                    Transformation.ParseTemplate(returnFieldInfo.ReturnFieldResultTemplate.ToString(), agregated)
                        .Select(
                            input => regex.Replace(input, returnFieldInfo.ReturnFieldRegexReplacement.ToString()).Trim())
                        .Where(replace => !string.IsNullOrEmpty(replace))
                        .SelectMany(
                            replace =>
                                (from Match match in
                                    System.Text.RegularExpressions.Regex.Matches(replace,
                                        returnFieldInfo.ReturnFieldRegexMatchPattern.ToString())
                                    select match.Value.Trim()))
                        .Where(value => !string.IsNullOrEmpty(value))
                        .ToList();
                returnFields.Add(returnFieldInfo.ReturnFieldId.ToString(), list);
                if (ProgressCallback != null) ProgressCallback(++current, total);
            }
            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return returnFields;
        }

        /// <summary>
        ///     Формирование пар идентификатор параметра - значение параметра
        ///     для замены в строке-шаблоне
        /// </summary>
        public Values BuildValues(Database database, RequestProperties requestId, RequestProperties mappedId)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine("requestId {0}", requestId);
            Debug.WriteLine("mappedId {0}", mappedId);
            IEnumerable<string> mapping =
                database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());
            var values = new Values();

            long current = 0;
            long total = mapping.Count();
            foreach (string mappingTable in mapping)
            {
                Debug.Assert(requestId[mappingTable] != null &&
                             !string.IsNullOrEmpty(requestId[mappingTable].ToString()));
                Debug.Assert(mappedId[mappingTable] != null &&
                             !string.IsNullOrEmpty(mappedId[mappingTable].ToString()));

                string[] parents = mappedId[mappingTable].ToString().Split(SplitChar);
                values.Add(Regex.Escape(string.Format("{{{{{0}}}}}", mappingTable)), parents.Last());
                for (int i = 0; i < parents.Count(); i++)
                    values.Add(Regex.Escape(string.Format("{{{{{0}[{1}]}}}}", mappingTable, i)), parents[i]);

                Collections.Properties userFields = database.GetUserFields(requestId[mappingTable],
                    mappedId[mappingTable],
                    mappingTable, requestId.Site);
                List<string> keys =
                    userFields.Keys.Select(item => Regex.Escape(string.Format("{{{{{0}}}}}", item))).ToList();
                List<string> list = userFields.Values.Select(item => item.ToString()).ToList();
                values.InsertOrReplace(new Values {{keys, list}});
                if (ProgressCallback != null) ProgressCallback(++current, total);
            }

            Debug.WriteLine("values {0}", values);
            if (CompliteCallback != null) CompliteCallback();
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
            MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(template, Transformation.FieldPattern);
            long current = 0;
            long total = matches.Count;
            foreach (
                string name in
                    from Match match in matches
                    select match.Groups[Transformation.NameGroup].Value)
            {
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
                        string key = Regex.Escape(string.Format("{{{{{0}}}}}", name));
                        if (values.ContainsKey(key)) continue;
                        object value = methodInfo.Invoke(null, new object[] {node, name});
                        if (value != null) values.Add(key, value.ToString());
                    }
                    catch (Exception exception)
                    {
                        LastError = exception;
                        Debug.WriteLine(LastError.ToString());
                    }
                }
                if (ProgressCallback != null) ProgressCallback(++current, total);
            }
            Debug.WriteLine("values {0}", values);
            if (CompliteCallback != null) CompliteCallback();
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
            return attribute != null ? attribute.Value : null;
        }

        /// <summary>
        ///     Получение значения указанного свойства указанного нода
        /// </summary>
        /// <param name="node"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string InvokeNodeProperty(HtmlNode node, string propertyName)
        {
            string[] names = propertyName.Split('.');
            object value = node;
            foreach (PropertyInfo propertyInfo in names.Select(name => typeof (HtmlNode).GetProperty(name)))
            {
                value = propertyInfo != null ? propertyInfo.GetValue(value, null) : null;
                if (value == null) return null;
            }
            return (string) value;
        }

        #endregion
    }
}