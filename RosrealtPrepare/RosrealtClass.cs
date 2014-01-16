using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using RealtyParser;

namespace RosrealtPrepare
{
    public class RosrealtClass
    {
        public static string Url = "http://rosrealt.ru";
        public static long SiteId = 2;

        public static async Task<String> GetLinks()
        {
            UriBuilder builder = new UriBuilder(Url);
            LinksCollection links = await RealtyParserUtils.GetLinks(builder.Uri, "//a[@href]", "windows-1251");
            return RealtyParserDatabase.GenerateSql(SiteId, links, "Link");
        }
        public static async Task<String> GetRegionSql()
        {
            UriBuilder builder1 = new UriBuilder(Url + "/poisk.php?");
            HierarhialItemCollection collection = new HierarhialItemCollection();
            OptionsCollection options1 = await RealtyParserUtils.GetOptions(builder1.Uri, "//select[@name='Region']/option[@value]", "windows-1251");
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
                    UriBuilder builder2 = new UriBuilder(Url + "/poisk.php?Region=" + option1.Key);
                    OptionsCollection options2 = await RealtyParserUtils.GetOptions(builder2.Uri, "//select[@name='City']/option[@value]", "windows-1251");
                    foreach (var option2 in options2)
                    {
                        collection.Add(option2.Key, option2.Value, option1.Key, 2);
                    }
                    foreach (var option2 in options2)
                    {
                        try
                        {

                            Debug.Assert(!String.IsNullOrEmpty(option1.Key));
                            UriBuilder builder3 = new UriBuilder(Url + "/poisk.php?Region=" + option1.Key + "&City=" + option2.Key);
                            OptionsCollection options3 = await RealtyParserUtils.GetOptions(builder3.Uri, "//select[@name='District']/option[@value]", "windows-1251");
                            foreach (var option3 in options3)
                            {
                                collection.Add(option3.Key, option3.Value, option2.Key, 3);
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return RealtyParserDatabase.GenerateSql(SiteId, collection, "Region");
        }
        public static async Task<String> GetActionSql()
        {
            UriBuilder builder = new UriBuilder(Url);
            OptionsCollection collection = await RealtyParserUtils.GetOptions(builder.Uri, "//select[@name='Sdelka']/option[@value]", "windows-1251");
            return RealtyParserDatabase.GenerateSql(SiteId, collection, "Action");
        }
        public static async Task<String> GetRubricSql()
        {
            UriBuilder builder1 = new UriBuilder(Url + "/poisk.php?Sdelka=1");
            HierarhialItemCollection collection = new HierarhialItemCollection();
            OptionsCollection options1 = await RealtyParserUtils.GetOptions(builder1.Uri, "//select[@name='Type_realty']/option[@value]", "windows-1251");
            Debug.Assert(options1 != null, "options1 != null");
            foreach (KeyValuePair<string, string> option1 in options1)
            {
                collection.Add(option1.Key, option1.Value, "", 1);
            }
            foreach (var option1 in options1)
            {
                try
                {

                    Debug.Assert(!String.IsNullOrEmpty(option1.Key));
                    UriBuilder builder2 = new UriBuilder(Url + "/poisk.php?Sdelka=1&Type_realty=" + option1.Key);
                    OptionsCollection options2 = await RealtyParserUtils.GetOptions(builder2.Uri, "//select[@name='Kind_realty']/option[@value]", "windows-1251");
                    foreach (var option2 in options2)
                    {
                        collection.Add(option2.Key, option2.Value, option1.Key, 2);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return RealtyParserDatabase.GenerateSql(SiteId, collection, "Rubric");
        }
        public static String GetMappingSql(string mappedTableName)
        {
            var database = RealtyParserUtils.GetDatabase();
            var lefts = database.GetDictionary<long>(mappedTableName);
            var rights = database.GetDictionary<string>("Site" + mappedTableName, "Site" + mappedTableName + "Id", "Site" + mappedTableName + "Title", SiteId);
            var mapping = RealtyParserUtils.BuildMapping(lefts, rights, mappedTableName, SiteId);
            return RealtyParserDatabase.GenerateSql(SiteId, mapping, mappedTableName);
        }
    }
}
