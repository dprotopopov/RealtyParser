using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using RealtyParser.Collections;
using RT.ParsingLibs.Models;

namespace RealtyParser
{
    /// <summary>
    ///     Класс вспомогательных алгоритмов
    /// </summary>
    public class Parser
    {
        public const char SplitChar = '\\';

        public Parser()
        {
            ModuleNamespace = GetType().Namespace;
        }

        public Transformation Transformation { get; set; }
        public ProgressCallback ProgressCallback { get; set; }
        public AppendLineCallback AppendLineCallback { get; set; }
        public CompliteCallback CompliteCallback { get; set; }
        private object LastError { get; set; }
        public Database Database { get; set; }
        public Converter Converter { get; set; }

        public string ModuleNamespace { get; set; }

        /// <summary>
        ///     Создание инстанса WebPublication на основе разобранных текстровых полей объявления
        /// </summary>
        public WebPublication CreateWebPublication(ReturnFields returnFields, RequestProperties requestProperties)
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

            foreach (
                PropertyInfo propertyInfo in
                    returnFields.GetType()
                        .GetProperties()
                        .Where(propertyInfo => !returnFields.ContainsKey(propertyInfo.Name)))
            {
                Debug.WriteLine(string.Format("No data for property {0}", propertyInfo.Name));
            }

            var stackListQueue = new StackListQueue<KeyValuePair<object, string>>
            {
                new KeyValuePair<object, string>(webPublication, webPublication.GetType().Name),
                new KeyValuePair<object, string>(webPublication.AdditionalInfo,
                    webPublication.GetType().Name + webPublication.AdditionalInfo.GetType().Name),
                new KeyValuePair<object, string>(webPublication.AdditionalInfo.RealtyAdditionalInfo,
                    webPublication.GetType().Name + webPublication.AdditionalInfo.GetType().Name +
                    webPublication.AdditionalInfo.RealtyAdditionalInfo.GetType().Name),
                new KeyValuePair<object, string>(webPublication.Contact,
                    webPublication.GetType().Name + "Contact"),
            };

            foreach (var pair in stackListQueue)
            {
                foreach (PropertyInfo propertyInfo in pair.Key.GetType().GetProperties())
                {
                    string propertyName = propertyInfo.Name;
                    string propertyFullName = pair.Value + propertyName;
                    PropertyInfo returnPropertyInfo = returnFields.GetType().GetProperty(propertyFullName);
                    if (returnPropertyInfo == null) continue;
                    Type propertyType = propertyInfo.PropertyType;
                    Type returnPropertyType = returnPropertyInfo.PropertyType;
                    try
                    {
                        var enumerable = (IEnumerable<string>) returnPropertyInfo.GetValue(returnFields);
                        List<string> list = enumerable.ToList();
                        object value = Converter.Convert(list, propertyType);
                        propertyInfo.SetValue(pair.Key, value);
                        Debug.WriteLine("Assign data for property {0} from {1}", propertyInfo.Name, propertyFullName);
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.ToString());
                        Debug.WriteLine("Try assign {0}->{1}", propertyFullName, propertyName);
                        Debug.WriteLine("Try convert {0}{1}->{2}{3}", returnPropertyType.Name,
                            (returnPropertyType.IsArray ? " As Array" : ""), propertyType.Name,
                            (propertyType.IsArray ? " As Array" : ""));
                    }
                }
            }

            Debug.Assert(webPublication != null);
            Debug.Assert(webPublication.AdditionalInfo != null);
            Debug.Assert(webPublication.AdditionalInfo.RealtyAdditionalInfo != null);
            Debug.Assert(webPublication.Contact != null);

            foreach (var pair in requestProperties)
            {
                string propertyName = string.Format("{0}Id", pair.Key);
                PropertyInfo propertyInfo = webPublication.GetType().GetProperty(propertyName);
                if (propertyInfo == null) continue;
                try
                {
                    propertyInfo.SetValue(webPublication, Database.ConvertTo<long>(pair.Value));
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.ToString());
                    propertyInfo.SetValue(webPublication, (long) 0);
                }
            }

            foreach (var pair in stackListQueue)
            {
                foreach (PropertyInfo propertyInfo in pair.Key.GetType().GetProperties())
                {
                    try
                    {
                        object propertyValue = propertyInfo.GetValue(pair.Key);
                        Debug.WriteLine(propertyValue != null
                            ? string.Format("Property {0} is ok ({1})", propertyInfo.Name,
                                pair.Value + propertyInfo.Name)
                            : string.Format("Property {0} is null or empty ({1})", propertyInfo.Name,
                                pair.Value + propertyInfo.Name));
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine("Error in property {0} ({1})", propertyInfo.Name, pair.Value + propertyInfo.Name);
                        Debug.WriteLine(exception.ToString());
                    }
                }
            }

            Debug.Assert(webPublication != null);
            Debug.Assert(webPublication.AdditionalInfo != null);
            Debug.Assert(webPublication.AdditionalInfo.RealtyAdditionalInfo != null);
            Debug.Assert(webPublication.Contact != null);

            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return webPublication;
        }

        /// <summary>
        ///     Поиск и формирование значений возвращаемых полей загруженного с сайта объявления
        /// </summary>
        public ReturnFields BuildReturnFields(IEnumerable<HtmlDocument> parentDocuments, Values parentValues, ReturnFieldInfos returnFieldInfos)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var returnFields = new ReturnFields();
            long current = 0;
            long total = returnFieldInfos.ToList().Count*(parentDocuments.Count() + 1);
            foreach (ReturnFieldInfo returnFieldInfo in returnFieldInfos.ToList())
            {
                var agregated = new Values();
                foreach (HtmlDocument document in parentDocuments)
                {
                    var values = new Values(parentValues);
                    IEnumerable<string> xpaths =
                        Transformation.ParseTemplate(returnFieldInfo.ReturnFieldXpathTemplate.ToString(),
                            parentValues);

                    foreach (string xpath in xpaths) Debug.WriteLine(xpath);

                    foreach (HtmlNode htmlNode in xpaths.Select(xpath => document.DocumentNode.SelectNodes(xpath)).Where(nodes => nodes != null).SelectMany(nodes => nodes))
                    {
                        values.InsertOrAppend(BuildValues(returnFieldInfo.ReturnFieldResultTemplate.ToString(), htmlNode));
                    }

                    foreach (var pair in values)
                        if (!agregated.ContainsKey((pair.Key))) agregated.Add(pair.Key, pair.Value);
                        else if (agregated[pair.Key].Count() < pair.Value.Count())
                            agregated[pair.Key] = pair.Value;

                    if (ProgressCallback != null) ProgressCallback(++current, total);
                }


                var regex = new Regex(returnFieldInfo.ReturnFieldRegexPattern.ToString(),
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);
                IEnumerable<string> list =
                    Transformation.ParseTemplate(returnFieldInfo.ReturnFieldResultTemplate.ToString(), agregated)
                        .Select(
                            input => regex.Replace(input, returnFieldInfo.ReturnFieldRegexReplacement.ToString()).Trim())
                        .Where(replace => !string.IsNullOrEmpty(replace))
                        .SelectMany(
                            replace =>
                                (from Match match in
                                    Regex.Matches(replace,
                                        returnFieldInfo.ReturnFieldRegexMatchPattern.ToString())
                                    select match.Value.Trim()))
                        .Where(value => !string.IsNullOrEmpty(value));
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
        public Values BuildValues(RequestProperties requestId, RequestProperties mappedId, RequestProperties mappedLevel)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine("requestId {0}", requestId);
            Debug.WriteLine("mappedId {0}", mappedId);
            IEnumerable<string> mapping =
                Database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());
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
                values.Add(string.Format("{0}", mappingTable),
                    parents[Database.ConvertTo<long>(mappedLevel[mappingTable])]);
                for (int i = 0; i < parents.Count(); i++)
                    values.Add(string.Format("{0}[{1}]", mappingTable, i), parents[i]);

                Collections.Properties userFields = Database.GetUserFields(requestId[mappingTable],
                    mappedId[mappingTable],
                    mappingTable, requestId.Site);
                List<string> keys =
                    userFields.Keys.Select(item => string.Format("{0}", item)).ToList();
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
            MatchCollection matches = Regex.Matches(template, Transformation.FieldPattern);
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
                        string key = string.Format("{0}", name);
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
            string value = (attribute != null) ? attribute.Value : null;
            return (value != null) ? Uri.UnescapeDataString(value) : null;
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