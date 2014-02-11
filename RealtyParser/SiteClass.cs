using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RealtyParser.Collections;

namespace RealtyParser
{
    public static class SiteClass
    {
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
                HtmlDocument[] documents = await RealtyParserParsingModule.Parser.WebRequestHtmlDocument(uri, "GET", encoding);
                var links = new LinksCollection();
                Debug.Assert(documents != null, "document != null");
                Debug.WriteLine("xpath: " + xpath);
                Debug.WriteLine("document: " + documents[0].DocumentNode.OuterHtml);
                HtmlNodeCollection nodes = documents[0].DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    string link = Parser.AttributeValue(node, "href");
                    string value = node.InnerText;
                    Debug.WriteLine(System.String.Format("{0}->{1}", link, value));
                    if (!System.String.IsNullOrEmpty(link) && !links.ContainsKey(link)) links.Add(link, value);
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
                HtmlDocument[] documents = await RealtyParserParsingModule.Parser.WebRequestHtmlDocument(uri, "GET", encoding);
                var options = new OptionsCollection();
                Debug.Assert(documents != null, "document != null");
                Debug.WriteLine("xpath: " + xpath);
                Debug.WriteLine("document: " + documents[0].DocumentNode.OuterHtml);
                HtmlNodeCollection nodes = documents[0].DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    string option = Parser.AttributeValue(node, "value");
                    string value = node.NextSibling.InnerText;
                    Debug.WriteLine(System.String.Format("{0}->{1}", option, value));
                    if (!System.String.IsNullOrEmpty(option) && !options.ContainsKey(option))
                        options.Add(option, value);
                }
                return options;
            }
            catch (Exception)
            {
                return new OptionsCollection();
            }
        }

        public static Dictionary<object, string> BuildFullTitle(IEnumerable<object> items, string tableName,
            object siteId)
        {
            IEnumerable<string> mapping = RealtyParserParsingModule.Database.GetList("Mapping", "TableName").Select(item => item.ToString());
            IEnumerable<string> hierarchical = RealtyParserParsingModule.Database.GetList("Hierarchical", "TableName").Select(item => item.ToString());
            Debug.Assert(mapping.Contains(tableName));
            var dictionary = new Dictionary<object, string>();
            foreach (string key in items)
            {
                object i = key;
                var builder = new StringBuilder();
                bool contains = hierarchical.Contains(tableName);
                do
                {
                    builder.AppendLine(RealtyParserParsingModule.Database.GetScalar(i,
                        string.Format("Site{0}Title", tableName), tableName, siteId).ToString());
                } while
                    (
                    contains &&
                    Database.ConvertTo<long>(RealtyParserParsingModule.Database.GetScalar(i, "Level", tableName, siteId)) >
                    1 &&
                    (
                        (i =
                            RealtyParserParsingModule.Database.GetScalar(i, "ParentId", tableName,
                                siteId)) != null)
                    );
                dictionary.Add(key, builder.ToString());
            }
            return dictionary;
        }

        public static Dictionary<object, string> BuildFullTitle(IEnumerable<object> items, string tableName)
        {
            IEnumerable<string> mapping = RealtyParserParsingModule.Database.GetList("Mapping", "TableName").Select(item => item.ToString());
            IEnumerable<string> hierarchical = RealtyParserParsingModule.Database.GetList("Hierarchical", "TableName").Select(item => item.ToString());
            Debug.Assert(mapping.Contains(tableName));
            var dictionary = new Dictionary<object, string>();
            foreach (string key in items)
            {
                object i = key;
                var builder = new StringBuilder();
                bool contains = hierarchical.Contains(tableName);
                do
                {
                    builder.AppendLine(RealtyParserParsingModule.Database.GetScalar(i,
                        string.Format("{0}Title", tableName), tableName).ToString());
                } while
                    (
                    contains &&
                    Database.ConvertTo<long>(RealtyParserParsingModule.Database.GetScalar(i, "Level", tableName)) > 1 &&
                    ((i = RealtyParserParsingModule.Database.GetScalar(i, "ParentId", tableName)) != null)
                    );
                dictionary.Add(key, builder.ToString());
            }
            return dictionary;
        }

        /// <summary>
        ///     Не входит в техническое задание
        /// </summary>
        public static Dictionary<long, string> BuildMapping(Dictionary<object, string> lefts,
            Dictionary<object, string> rights)
        {
            Debug.Assert(rights.Count > 0);
            var results = new Dictionary<long, string>();
            foreach (var left in lefts)
            {
                string a = left.Value;
                string b = rights.First().Value;

                object[] index = {rights.First().Key};
                int distance = LevenshteinDistance.Compute(a, b);
                foreach (
                    var right in
                        rights.Where(right => index[0] != right.Key)
                    )
                {
                    b = right.Value;

                    int current = LevenshteinDistance.Compute(a, b);
                    if (current < distance)
                    {
                        distance = current;
                        index[0] = right.Key;
                    }
                }
                results.Add(Database.ConvertTo<long>(left.Key), index[0].ToString());
            }
            return results;
        }
    }
}