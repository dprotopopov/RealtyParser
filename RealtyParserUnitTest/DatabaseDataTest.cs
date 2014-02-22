using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser;
using RealtyParser.Collections;
using Mappings = RealtyParser.Mappings;

namespace RealtyParserUnitTest
{
    /// <summary>
    ///     Сводное описание для DatabaseDataTest
    /// </summary>
    [TestClass]
    public class DatabaseDataTest
    {
        private static readonly ParserModule ParserModule = new ParserModule();
        private static readonly Database Database = ParserModule.Database;
        private static readonly ComparerManager ComparerManager = ParserModule.ComparerManager;

        /// <summary>
        ///     Получает или устанавливает контекст теста, в котором предоставляются
        ///     сведения о текущем тестовом запуске и обеспечивается его функциональность.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Дополнительные атрибуты тестирования

        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public void GetDictionaryTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            foreach (object tableName in Database.GetList(Database.MappingTable, Database.TableNameColumn))
            {
                Mapping table = Database.GetMapping(tableName.ToString());
                Assert.IsTrue(table.Count > 0);
            }
        }

        [TestMethod]
        public void MappingTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            object siteId = Database.GetScalar((long) 1, Database.SiteTable);
            foreach (
                Mapping dictionary in
                    Database.GetList(Database.MappingTable, Database.TableNameColumn)
                        .Select(mappedTableName => Database.GetMapping(
                            String.Format("Site{0}Mappings", mappedTableName.ToString()),
                            String.Format("{0}Id", mappedTableName.ToString()),
                            String.Format("Site{0}Id", mappedTableName.ToString()),
                            siteId)))
            {
                Assert.IsTrue(dictionary.Count > 0);
            }
        }

        [TestMethod]
        public void PropertiesTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            object siteId = Database.GetScalar((long) 1, Database.SiteTable);
            object regionId = Database.GetScalar((long) 1, "Region");
            object rubricId = Database.GetScalar((long) 1, "Rubric");
            object actionId = Database.GetScalar((long) 1, "Action");
            SiteProperties properties = Database.GetSiteProperties(siteId);
            Assert.IsTrue(Database.GetScalar(regionId, "Region", siteId) != null &&
                          Database.GetScalar(rubricId, "Rubric", siteId) != null &&
                          Database.GetScalar(actionId, "Action", siteId) != null);
        }

        [TestMethod]
        public void NodePropertyNameTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            Type type = typeof (HtmlNode);
            Assert.IsNotNull(type);
            IEnumerable<object> propertyNames = Database.GetList("NodePropertyName", "NodePropertyName");
            foreach (object propertyName in propertyNames)
            {
                PropertyInfo propertyInfo = type.GetProperty(propertyName.ToString());
                Assert.IsTrue(propertyInfo != null ||
                              String.Compare(propertyName.ToString(), "href", StringComparison.Ordinal) == 0 ||
                              String.Compare(propertyName.ToString(), "src", StringComparison.Ordinal) == 0 ||
                              String.Compare(propertyName.ToString(), "value", StringComparison.Ordinal) == 0
                    );
            }
        }

        [TestMethod]
        public void PublicationIdComparerTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            IEnumerable<object> classNames = Database.GetList("PublicationIdComparer", "ClassName");
            foreach (object className in classNames)
            {
                Assert.IsNotNull(ComparerManager.CreatePublicationComparer(className.ToString()));
            }
        }

        [TestMethod]
        public void HtmlMethodTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            IEnumerable<object> methodNames = Database.GetList("HtmlMethod", "Method");
            IList<object> enumerable = methodNames as IList<object> ?? methodNames.ToList();
            Assert.AreEqual(enumerable.Count, 2);
            Assert.IsTrue(enumerable.Contains("GET"));
            Assert.IsTrue(enumerable.Contains("POST"));
        }

        [TestMethod]
        public void FirstOrDefaultTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //

            object siteId = Database.GetScalar((long) 2, Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            object regionId = mappings.Region.Keys.FirstOrDefault();
            object rubricId = mappings.Rubric.Keys.FirstOrDefault();
            object actionId = mappings.Action.Keys.FirstOrDefault();

            Console.WriteLine(@"-------------------------------------------------------------------");
            Console.WriteLine(@"siteId -> {0}", siteId);
            Console.WriteLine(@"regionId -> {0}", regionId);
            Console.WriteLine(@"rubricId -> {0}", rubricId);
            Console.WriteLine(@"actionId -> {0}", actionId);
            Console.WriteLine(@"-------------------------------------------------------------------");

            Assert.IsTrue(mappings.Action.ContainsKey(actionId));
            Assert.IsTrue(mappings.Rubric.ContainsKey(rubricId));
            Assert.IsTrue(mappings.Region.ContainsKey(regionId));

            object mappingRegionId = mappings.Region[regionId];
            object mappingRubricId = mappings.Rubric[rubricId];
            object mappingActionId = mappings.Action[actionId];

            object antiActionId = Database.GetScalar(actionId, "AntiActionId", "Action");
            object levelRegion = Database.GetScalar(mappingRegionId, Database.LevelColumn, "Region", siteId);
            object levelRubric = Database.GetScalar(mappingRubricId, Database.LevelColumn, "Rubric", siteId);
            object mappingParentRegionId = Database.GetScalar(mappingRegionId, Database.ParentIdColumn, "Region",
                siteId);
            object mappingParentRubricd = Database.GetScalar(mappingRubricId, Database.ParentIdColumn, "Rubric",
                siteId);
            object mappingAntiActionId = mappings.Action[antiActionId];

            Console.WriteLine(@"-------------------------------------------------------------------");
            Console.WriteLine(@"Region[{0}] -> '{1}', Level={2}, Parent='{3}'", regionId, mappingRegionId, levelRegion,
                mappingParentRegionId);
            Console.WriteLine(@"Rubric[{0}] -> '{1}', Level={2}, Parent='{3}'", rubricId, mappingRubricId, levelRubric,
                mappingParentRubricd);
            Console.WriteLine(@"Action[{0}] -> '{1}', AntiAction[{2}] -> '{3}'", actionId, mappingActionId, antiActionId,
                mappingAntiActionId);
            Console.WriteLine(@"-------------------------------------------------------------------");

            var requestId = new RequestProperties
            {
                {"Action", actionId},
                {"Region", regionId},
                {"Rubric", rubricId},
                {Database.SiteTable, siteId}
            };
            IEnumerable<string> mapping =
                Database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());
            var mappedId = new RequestProperties
            {
                requestId.Where(pair => mapping.Contains(pair.Key)).ToDictionary(pair => pair.Key,
                    pair => Database.GetScalar(pair.Value, Database.LevelColumn, requestId.Site)),
                requestId.Where(pair => !mapping.Contains(pair.Key)).ToDictionary(pair => pair.Key,
                    pair => pair.Value),
            };
            var mappedLevel = new RequestProperties
            {
                mappedId.Where(pair => mapping.Contains(pair.Key)).ToDictionary(pair => pair.Key,
                    pair => Database.GetScalar(pair.Value, Database.LevelColumn, pair.Key, requestId.Site))
            };
            Values args = ParserModule.Parser.BuildValues(requestId, mappedId, mappedLevel);
            Console.WriteLine(args.ToString());

            for (long level = 1; level <= Database.ConvertTo<long>(levelRegion); level++)
                Assert.IsTrue(args.ContainsKey(string.Format("Region[{0}]", level)));

            for (long level = 1; level <= Database.ConvertTo<long>(levelRubric); level++)
                Assert.IsTrue(args.ContainsKey(string.Format("Rubric[{0}]", level)));
        }

        [TestMethod]
        public void LastOrDefaultTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //

            object siteId = Database.GetScalar((long) 2, Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            object regionId = mappings.Region.Keys.LastOrDefault();
            object rubricId = mappings.Rubric.Keys.LastOrDefault();
            object actionId = mappings.Action.Keys.LastOrDefault();

            Console.WriteLine(@"-------------------------------------------------------------------");
            Console.WriteLine(@"siteId -> {0}", siteId);
            Console.WriteLine(@"regionId -> {0}", regionId);
            Console.WriteLine(@"rubricId -> {0}", rubricId);
            Console.WriteLine(@"actionId -> {0}", actionId);
            Console.WriteLine(@"-------------------------------------------------------------------");

            Assert.IsTrue(mappings.Action.ContainsKey(actionId));
            Assert.IsTrue(mappings.Rubric.ContainsKey(rubricId));
            Assert.IsTrue(mappings.Region.ContainsKey(regionId));

            object mappingRegionId = mappings.Region[regionId];
            object mappingRubricId = mappings.Rubric[rubricId];
            object mappingActionId = mappings.Action[actionId];

            object antiActionId = Database.GetScalar(actionId, "AntiActionId", "Action");
            object levelRegion = Database.GetScalar(mappingRegionId, Database.LevelColumn, "Region", siteId);
            object levelRubric = Database.GetScalar(mappingRubricId, Database.LevelColumn, "Rubric", siteId);
            object mappingParentRegionId = Database.GetScalar(mappingRegionId, Database.ParentIdColumn, "Region",
                siteId);
            object mappingParentRubricd = Database.GetScalar(mappingRubricId, Database.ParentIdColumn, "Rubric",
                siteId);
            object mappingAntiActionId = mappings.Action[antiActionId];

            Console.WriteLine(@"-------------------------------------------------------------------");
            Console.WriteLine(@"Region[{0}] -> '{1}', Level={2}, Parent='{3}'", regionId, mappingRegionId, levelRegion,
                mappingParentRegionId);
            Console.WriteLine(@"Rubric[{0}] -> '{1}', Level={2}, Parent='{3}'", rubricId, mappingRubricId, levelRubric,
                mappingParentRubricd);
            Console.WriteLine(@"Action[{0}] -> '{1}', AntiAction[{2}] -> '{3}'", actionId, mappingActionId, antiActionId,
                mappingAntiActionId);
            Console.WriteLine(@"-------------------------------------------------------------------");

            var requestId = new RequestProperties
            {
                {"Action", actionId},
                {"Region", regionId},
                {"Rubric", rubricId},
                {Database.SiteTable, siteId}
            };
            IEnumerable<string> mapping =
                Database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());
            var mappedId = new RequestProperties
            {
                requestId.Where(pair => mapping.Contains(pair.Key)).ToDictionary(pair => pair.Key,
                    pair => Database.GetScalar(pair.Value, Database.LevelColumn, requestId.Site)),
                requestId.Where(pair => !mapping.Contains(pair.Key)).ToDictionary(pair => pair.Key,
                    pair => pair.Value),
            };
            var mappedLevel = new RequestProperties
            {
                mappedId.Where(pair => mapping.Contains(pair.Key)).ToDictionary(pair => pair.Key,
                    pair => Database.GetScalar(pair.Value, Database.LevelColumn, pair.Key, requestId.Site))
            };
            Values args = ParserModule.Parser.BuildValues(requestId, mappedId, mappedLevel);
            Console.WriteLine(args.ToString());

            for (long level = 1; level <= Database.ConvertTo<long>(levelRegion); level++)
                Assert.IsTrue(args.ContainsKey(string.Format("Region[{0}]", level)));

            for (long level = 1; level <= Database.ConvertTo<long>(levelRubric); level++)
                Assert.IsTrue(args.ContainsKey(string.Format("Rubric[{0}]", level)));
        }
    }
}