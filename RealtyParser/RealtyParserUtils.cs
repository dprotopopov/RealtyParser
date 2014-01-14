using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        public static async Task<HtmlDocument> WebRequestHtmlDocument(Uri uri, string method)
        {
            ICrawler crawler = new WebCrawler();
            var requestWeb = (HttpWebRequest)WebRequest.Create(uri);
            requestWeb.Method = method;
            var responce = await crawler.GetResponse(requestWeb);
            if (responce != null)
            {
                var reader = new StreamReader(responce.GetResponseStream());
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
                Regex regex = new Regex(@"\{\{" + pair.Key + @"\}\}", RegexOptions.IgnoreCase);
                htmlTemplate = regex.Replace(htmlTemplate, pair.Value);
            }
            Regex rgx = new Regex(@"\{\{\w+\}\}", RegexOptions.IgnoreCase);
            return rgx.Replace(htmlTemplate, @"");
        }

        public static ReturnFields BuildReturnFields(
            RealtyParserDatabase database,
            HtmlNode unoNode,
            Arguments args,
            long siteId)
        {
            ReturnFields returnFields = new ReturnFields();
            database.Connection.Open();
            using (SQLiteCommand command = database.Connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM SiteReturnFieldMapping JOIN ReturnFields USING (SiteId) WHERE SiteReturnFieldMapping.SiteId=@SiteId";
                SQLiteParameter p = new SQLiteParameter("@SiteId", System.Data.DbType.Int32) { Value = siteId };
                command.Parameters.Add(p);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string key = (string)reader["ReturnFieldId"];
                    string xpathTemplate = (string)reader["UnoSearchReturnFieldXpathTemplate"];
                    string nodePropertyName = (string)reader["UnoSearchReturnFieldNodePropertyName"];
                    returnFields.Add(key, new List<string>());
                    foreach (string value in unoNode.SelectNodes(ParseTemplate(xpathTemplate, args)).Select(node => InvokeNodeProperty(node, nodePropertyName)))
                    {
                        returnFields[key].Add(value);
                    }
                }
                database.Connection.Close();
            }
            return returnFields;
        }

        public static Arguments BuildArgs(
            RealtyParserDatabase database,
            long regionId,
            long rubricId,
            long actionId,
            string publicationId,
            long siteId)
        {
            Arguments args = new Arguments
            {
                {"RegionId", database.Mapping(siteId,regionId,"Region")},
                {"RubricId", database.Mapping(siteId,rubricId,"Rubric")},
                {"ActionId", database.Mapping(siteId,actionId,"Action")},
                {"PublicationId", publicationId}
            };
            return args;
        }
    }
}
