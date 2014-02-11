using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using RealtyParser;
using RealtyParser.Collections;
using String = System.String;

namespace RosrealtPrepare
{
    public static class RosrealtClass
    {
        private const string Url = "http://rosrealt.ru";
        private const long SiteId = 2;

        public static async Task<String> GetLinks()
        {
            var builder = new UriBuilder(Url);
            LinksCollection links = await SiteClass.GetLinks(builder.Uri, "//a[@href]", "windows-1251");
            return Database.GenerateSql(SiteId, links, "Link");
        }

        public static async Task<String> GetRegionSql()
        {
            var builder1 = new UriBuilder(Url + "/poisk.php?");
            var collection = new HierarchicalItemCollection();
            OptionsCollection options1 =
                await
                    SiteClass.GetOptions(builder1.Uri, "//select[@name='Region']/option[@value]", "windows-1251");
            Debug.Assert(options1 != null, "options1 != null");
            foreach (var option1 in options1)
            {
                collection.Add(option1.Key, option1.Value, "", 1);
            }
            foreach (var option1 in options1)
            {
                try
                {
                    Debug.Assert(!String.IsNullOrEmpty(option1.Key));
                    var builder2 = new UriBuilder(string.Format("{0}/poisk.php?Region={1}", Url, option1.Key));
                    OptionsCollection options2 =
                        await
                            SiteClass.GetOptions(builder2.Uri, "//select[@name='City']/option[@value]",
                                "windows-1251");
                    foreach (var option2 in options2)
                    {
                        collection.Add(option2.Key, option2.Value, option1.Key, 2);
                    }
                    foreach (var option2 in options2)
                    {
                        try
                        {
                            Debug.Assert(!String.IsNullOrEmpty(option1.Key));
                            var builder3 =
                                new UriBuilder(string.Format("{0}/poisk.php?Region={1}&City={2}", Url, option1.Key,
                                    option2.Key));
                            OptionsCollection options3 =
                                await
                                    SiteClass.GetOptions(builder3.Uri,
                                        "//select[@name='District']/option[@value]", "windows-1251");
                            foreach (var option3 in options3)
                            {
                                collection.Add(option3.Key, option3.Value, option2.Key, 3);
                            }
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
            return Database.GenerateSql(SiteId, collection, "Region");
        }

        public static async Task<String> GetActionSql()
        {
            var builder = new UriBuilder(Url);
            OptionsCollection collection =
                await
                    SiteClass.GetOptions(builder.Uri, "//select[@name='Sdelka']/option[@value]", "windows-1251");
            return Database.GenerateSql(SiteId, collection, "Action");
        }

        public static async Task<String> GetRubricSql()
        {
            var builder1 = new UriBuilder(Url + "/poisk.php?Sdelka=1");
            var collection = new HierarchicalItemCollection();
            OptionsCollection options1 =
                await
                    SiteClass.GetOptions(builder1.Uri, "//select[@name='Type_realty']/option[@value]",
                        "windows-1251");
            Debug.Assert(options1 != null, "options1 != null");
            foreach (var option1 in options1)
            {
                collection.Add(option1.Key, option1.Value, "", 1);
            }
            foreach (var option1 in options1)
            {
                try
                {
                    Debug.Assert(!String.IsNullOrEmpty(option1.Key));
                    var builder2 =
                        new UriBuilder(string.Format("{0}/poisk.php?Sdelka=1&Type_realty={1}", Url, option1.Key));
                    OptionsCollection options2 =
                        await
                            SiteClass.GetOptions(builder2.Uri, "//select[@name='Kind_realty']/option[@value]",
                                "windows-1251");
                    foreach (var option2 in options2)
                    {
                        collection.Add(option2.Key, option2.Value, option1.Key, 2);
                    }
                }
                catch (Exception)
                {
                }
            }
            return Database.GenerateSql(SiteId, collection, "Rubric");
        }

        public static String GetMappingSql(string mappedTableName)
        {
            Database database = RealtyParserParsingModule.Database;
            object siteId = database.GetScalar(SiteId, "Site");
            Dictionary<object, object> lefts = database.GetDictionary(mappedTableName);
            Dictionary<object, object> rights = database.GetDictionary("Site" + mappedTableName,
                string.Format("Site{0}Id", mappedTableName), string.Format("Site{0}Title", mappedTableName), siteId);
            Dictionary<object, string> leftTitles = SiteClass.BuildFullTitle(lefts.Keys, mappedTableName);
            Dictionary<object, string> rightTitles = SiteClass.BuildFullTitle(rights.Keys, mappedTableName, SiteId);
            Dictionary<long, string> mapping = SiteClass.BuildMapping(leftTitles, rightTitles);
            return Database.GenerateSql(SiteId, mapping, mappedTableName);
        }
    }
}