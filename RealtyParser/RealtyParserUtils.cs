﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RT.Crawler;
using RT.ParsingLibs.Models;

namespace RealtyParser
{
    public static class RealtyParserUtils
    {
        static readonly RealtyParserDatabase Database = new RealtyParserDatabase();
        public static RealtyParserDatabase GetDatabase()
        {
            return Database;
        }
        public static async Task<HtmlDocument> WebRequestHtmlDocument(Uri uri, string method, string encoding)
        {
            Debug.WriteLine(uri.ToString());
            ICrawler crawler = new WebCrawler();
            var requestWeb = (HttpWebRequest)WebRequest.Create(uri);
            requestWeb.Method = method;
            var responce = await crawler.GetResponse(requestWeb);
            if (responce != null)
            {
                Encoding encoder = Encoding.GetEncoding(encoding);
                var reader = new StreamReader(responce.GetResponseStream(), encoder);
                var output = new StringBuilder(reader.ReadToEnd());
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(output.ToString());
                Debug.WriteLine(output.ToString());
                return document;
            }
            return new HtmlDocument();
        }

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

        public static WebPublication CreateWebPublication(
            ReturnFields unoReturnFields,
            long regionId,
            long rubricId,
            long actionId,
            UriBuilder unoBuilder
)
        {
            Uri site = null;
            Uri uri = null;
            try
            {
                site = new Uri(unoBuilder.Host);
            }
            catch
            {
            }
            try
            {
                uri = unoBuilder.Uri;
            }
            catch
            {
            }

            return new WebPublication
            {
                AdditionalInfo = new AdditionalInfo
                {
                    RealtyAdditionalInfo = new RealtyAdditionalInfo
                    {
                        Address = unoReturnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoAddress.FirstOrDefault(),
                        AppointmentOfRoom =
                            unoReturnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoAppointmentOfRoom
                                .FirstOrDefault(),
                        CostAll =
                            Convert.ToDecimal(
                                unoReturnFields.WebPublicationAdditionalInfoRealtyAdditionalInfoCostAll.FirstOrDefault())
                    }
                },
                Contact = new WebPublicationContact
                {
                    Author = unoReturnFields.WebPublicationContactAuthor.FirstOrDefault(),
                    AuthorUrl = ConvertToUris(unoReturnFields.WebPublicationContactAuthorUrl.ToArray()).FirstOrDefault(),
                    ContactName = unoReturnFields.WebPublicationContactContactName.FirstOrDefault(),
                    Email = unoReturnFields.WebPublicationContactEmail.ToArray(),
                    Icq = Convert.ToUInt32(unoReturnFields.WebPublicationContactIcq.FirstOrDefault()),
                    Phone = unoReturnFields.WebPublicationContactPhone.ToArray(),
                    Skype = unoReturnFields.WebPublicationContactSkype.FirstOrDefault()
                },
                Description = unoReturnFields.WebPublicationDescription.FirstOrDefault(),
                ModifyDate = string.IsNullOrEmpty(unoReturnFields.WebPublicationModifyDate.FirstOrDefault())?DateTime.Now:DateTime.Parse(unoReturnFields.WebPublicationModifyDate.FirstOrDefault()),
                Photos = ConvertToUris(unoReturnFields.WebPublicationPhotos.ToArray()),
                PublicationId = unoReturnFields.WebPublicationPublicationId.FirstOrDefault(),
                RegionId = regionId,
                RubricId = rubricId,
                ActionId = actionId,
                Site =site,
                Url = uri
            };
        }
        public static Uri[] ConvertToUris(string[] strings)
        {
            return strings.Select(s => new Uri(s)).ToArray();
        }
        public static string InvokeNodeProperty(HtmlNode node, string propertyName)
        {
            Type type = typeof(HtmlNode);
            Debug.Assert(type != null, "type != null");
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            Debug.Assert(propertyInfo != null, "propertyInfo != null");
            return (string)propertyInfo.GetValue(node, null);
        }

        public static string ParseTemplate(string template, Arguments args)
        {
            foreach (KeyValuePair<string, string> pair in args)
            {
                Debug.Assert(pair.Key != null);
                Debug.Assert(pair.Value != null);

                Debug.WriteLine("ParseTemplate: " + pair.Key + " , " + template + " , " + pair.Value);
                Regex regex = new Regex(pair.Key, RegexOptions.IgnoreCase);
                template = regex.Replace(template, pair.Value);
            }
            Regex rgx = new Regex(@"\{\{[^\}]*\}\}", RegexOptions.IgnoreCase);
            return rgx.Replace(template, @"");
        }

        public static ReturnFields BuildReturnFields(
            RealtyParserDatabase database,
            HtmlNode unoNode,
            Arguments args,
            List<ReturnFieldInfo> returnFieldInfos)
        {
            ReturnFields returnFields = new ReturnFields();
            foreach (var returnFieldInfo in returnFieldInfos)
            {
                Regex regex = new Regex(returnFieldInfo.UnoReturnFieldRegexPattern, RegexOptions.IgnoreCase);
                var nodes = unoNode.SelectNodes(ParseTemplate(returnFieldInfo.UnoReturnFieldXpathTemplate, args));
                if (nodes != null)
                {
                    var list = nodes.Select(node => regex.Replace(ParseTemplate(returnFieldInfo.UnoReturnFieldResultTemplate, BuildArgs(node)), returnFieldInfo.UnoReturnFieldRegexReplacement)).ToList();
                    returnFields.Add(returnFieldInfo.ReturnFieldId, list);
                }
            }
            return returnFields;
        }

        public static Arguments BuildArgs(
            RealtyParserDatabase database,
            long regionId,
            long rubricId,
            long actionId,
            string publicationId,
            Mapping mapping,
            long siteId)
        {
            string mappingRegionId = mapping.Region[regionId];
            string mappingRubricId = mapping.Rubric[rubricId];
            string mappingActionId = mapping.Action[actionId];
            string mappingAntiActionId = mapping.Action[database.GetScalar<long, long>(actionId, "AntiActionId", "Action")];
            Arguments args = new Arguments
            {
                {@"\{\{RegionId\}\}", mappingRegionId},
                {@"\{\{RubricId\}\}", mappingRubricId},
                {@"\{\{ActionId\}\}", mappingActionId},
                {@"\{\{PublicationId\}\}", publicationId},
                {@"\{\{AntiActionId\}\}", mappingAntiActionId}
            };
            for (long level = database.GetScalar<long, string>(mappingRegionId, "Level", "Region", siteId); level > 0 && !String.IsNullOrEmpty(mappingRegionId); level = database.GetScalar<long, string>(mappingRegionId, "Level", "Region", siteId))
            {
                string key = @"\{\{RegionId\[" + level + @"\]\}\}";
                if (!args.ContainsKey(key)) args.Add(key, mappingRegionId);
                mappingRegionId = database.GetScalar<string, string>(mappingRegionId, "ParentId", "Region", siteId);
            }
            for (long level = database.GetScalar<long, string>(mappingRubricId, "Level", "Rubric", siteId); level > 0 && !String.IsNullOrEmpty(mappingRegionId); level = database.GetScalar<long, string>(mappingRubricId, "Level", "Rubric", siteId))
            {
                string key = @"\{\{RubricId\[" + level + @"\]\}\}";
                if (!args.ContainsKey(key)) args.Add(key, mappingRubricId);
                mappingRubricId = database.GetScalar<string, string>(mappingRubricId, "ParentId", "Rubric", siteId);
            }
            return args;
        }
        public static Arguments BuildArgs(HtmlNode node)
        {
            Debug.Assert(node != null);
            try
            {
                Arguments args = new Arguments
                {
                    {@"\{\{Id\}\}", node.Id},
                    {@"\{\{InnerText\}\}", node.InnerText},
                    {@"\{\{HrefValue\}\}", HrefValue(node)},
                    {@"\{\{Name\}\}", node.Name}
                };
                return args;
            }
            catch (Exception)
            {
                return new Arguments();
            }
        }

        public static string HrefValue(HtmlNode node)
        {
            try
            {
                HtmlAttribute href = node.Attributes["href"];
                return href.Value;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string OptionValue(HtmlNode node)
        {
            try
            {
                HtmlAttribute value = node.Attributes["value"];
                return value.Value;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static async Task<LinksCollection> GetLinks(Uri uri, string xpath, string encoding)
        {
            try
            {
                HtmlDocument document = await WebRequestHtmlDocument(uri, "GET", encoding);
                LinksCollection links = new LinksCollection();
                Debug.Assert(document != null, "document != null");
                Debug.WriteLine("xpath: " + xpath);
                Debug.WriteLine("document: " + document.DocumentNode.OuterHtml);
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                foreach (var node in nodes)
                {
                    string link = HrefValue(node);
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
        public static async Task<OptionsCollection> GetOptions(Uri uri, string xpath, string encoding)
        {
            try
            {

                HtmlDocument document = await WebRequestHtmlDocument(uri, "GET", encoding);
                OptionsCollection options = new OptionsCollection();
                Debug.Assert(document != null, "document != null");
                Debug.WriteLine("xpath: " + xpath);
                Debug.WriteLine("document: " + document.DocumentNode.OuterHtml);
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                foreach (var node in nodes)
                {
                    string option = OptionValue(node);
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

        public static Dictionary<long, string> BuildMapping(Dictionary<long, string> lefts,
            Dictionary<string, string> rights, string tableName, long siteId)
        {
            Debug.Assert(rights.Count > 0);
            Dictionary<long, string> results = new Dictionary<long, string>();
            foreach (var left in lefts)
            {
                string a = left.Value;
                StringBuilder builder = new StringBuilder();
                do
                {
                    builder.AppendLine(a);
                }
                while
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
                }
                while
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
                    }
                    while
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
    }
}
