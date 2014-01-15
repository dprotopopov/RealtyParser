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
                return document;
            }
            return null;
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
                ModifyDate = DateTime.Parse(unoReturnFields.WebPublicationModifyDate.FirstOrDefault()),
                Photos = ConvertToUris(unoReturnFields.WebPublicationPhotos.ToArray()),
                PublicationId = unoReturnFields.WebPublicationPublicationId.FirstOrDefault(),
                RegionId = regionId,
                RubricId = rubricId,
                ActionId = actionId,
                Site = new Uri(unoBuilder.Host),
                Url = unoBuilder.Uri
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

        public static string ParseTemplate(string htmlTemplate, Arguments args)
        {
            foreach (KeyValuePair<string, string> pair in args)
            {
                Regex regex = new Regex(pair.Key, RegexOptions.IgnoreCase);
                htmlTemplate = regex.Replace(htmlTemplate, pair.Value);
            }
            Regex rgx = new Regex(@"\{\{[^\}]*\}\}", RegexOptions.IgnoreCase);
            return rgx.Replace(htmlTemplate, @"");
        }

        public static ReturnFields BuildReturnFields(
            RealtyParserDatabase database,
            HtmlNode unoNode,
            Arguments args,
            List<ReturnFieldInfo> returnFieldInfos)
        {
            ReturnFields returnFields = new ReturnFields();
            Parallel.ForEach(returnFieldInfos, returnFieldInfo =>
            {
                Regex regex = new Regex(returnFieldInfo.UnoReturnFieldRegexPattern, RegexOptions.IgnoreCase);
                returnFields.Add(returnFieldInfo.ReturnFieldId,
                    unoNode.SelectNodes(ParseTemplate(returnFieldInfo.UnoReturnFieldXpathTemplate, args))
                        .Select(node => regex.Replace(ParseTemplate(returnFieldInfo.UnoReturnFieldResultTemplate,
                            BuildArgs(node)), returnFieldInfo.UnoReturnFieldRegexReplacement))
                        .ToList());

            });
            return returnFields;
        }

        public static Arguments BuildArgs(
            RealtyParserDatabase database,
            long regionId,
            long rubricId,
            long actionId,
            string publicationId,
            Mapping mapping)
        {
            Arguments args = new Arguments
            {
                {@"\{\{RegionId\}\}", mapping.Region[regionId]},
                {@"\{\{RubricId\}\}", mapping.Rubric[rubricId]},
                {@"\{\{ActionId\}\}", mapping.Action[actionId]},
                {@"\{\{PublicationId\}\}", publicationId},
                {@"\{\{AntiActionId\}\}", mapping.Action[database.GetScalar<long>(actionId,"AntiActionId","Action")]}
            };
            for (long level = database.GetScalar<long>(regionId, "Level", "Region"); regionId > 0; )
            {
                args.Add(@"\{\{RegionId\[" + level + @"\]\}\}", mapping.Region[regionId]);
                regionId = database.GetScalar<long>(regionId, "ParentId", "Region");
                level = database.GetScalar<long>(regionId, "Level", "Region");
            }
            for (long level = database.GetScalar<long>(rubricId, "Level", "Rubric"); rubricId > 0; )
            {
                args.Add(@"\{\{RubricId\[" + level + @"\]\}\}", mapping.Rubric[rubricId]);
                rubricId = database.GetScalar<long>(rubricId, "ParentId", "Rubric");
                level = database.GetScalar<long>(rubricId, "Level", "Rubric");
            }

            return args;
        }
        public static Arguments BuildArgs(HtmlNode node)
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

        public static string HrefValue(HtmlNode node)
        {
            try
            {
                HtmlAttribute href = node.Attributes["href"];
                return href.Value;
            }
            catch (Exception)
            {
                return null;
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
                return null;
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
                return null;
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
                return null;
            }
        }

        public static Dictionary<long, string> BuildMapping(Dictionary<long, string> lefts,
            Dictionary<string, string> rights)
        {
            Debug.Assert(rights.Count > 0);
            Dictionary<long, string> results = new Dictionary<long, string>();
            Regex regex = new Regex(@"\s+");
            Parallel.ForEach(lefts, left =>
            {
                string a = regex.Replace(left.Value.ToLower(), "");
                string b = regex.Replace(rights.First().Value.ToLower(), "");
                string index = rights.First().Key;
                int distance = LevenshteinDistance.Compute(a, b);
                foreach (var right in rights.Where(right => index != right.Key))
                {
                    b = regex.Replace(right.Value.ToLower(), "");
                    int current = LevenshteinDistance.Compute(a, b);
                    if (current < distance)
                    {
                        distance = current;
                        index = right.Key;
                    }
                }
                results.Add(left.Key, index);
            });
            return results;
        }
    }
}
