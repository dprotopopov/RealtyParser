using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser;
using RealtyParser.Collections;
using Mappings = RealtyParser.Mappings;
using String = System.String;

namespace RealtyParserUnitTest
{
    /// <summary>
    ///     Сводное описание для DatabaseDataTest
    /// </summary>
    [TestClass]
    public class DatabaseDataTest
    {
        private readonly Database _database = new Database();

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
            foreach (object tableName in _database.GetList(Database.MappingTable, Database.TableNameColumn))
            {
                Mapping table = _database.GetMapping(tableName.ToString());
                Assert.IsTrue(table.Count > 0);
            }
        }

        [TestMethod]
        public void MappingTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            object siteId = _database.GetScalar((long) 1, Database.SiteTable);
            foreach (
                var dictionary in
                    _database.GetList(Database.MappingTable, Database.TableNameColumn)
                        .Select(mappedTableName => _database.GetMapping(
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
            object siteId = _database.GetScalar((long) 1, Database.SiteTable);
            object regionId = _database.GetScalar((long) 1, "Region");
            object rubricId = _database.GetScalar((long) 1, "Rubric");
            object actionId = _database.GetScalar((long) 1, "Action");
            SiteProperties properties = _database.GetSiteProperties(siteId);
            Assert.IsTrue(_database.GetScalar(regionId, "Region", siteId) != null &&
                          _database.GetScalar(rubricId, "Rubric", siteId) != null &&
                          _database.GetScalar(actionId, "Action", siteId) != null);
        }

        [TestMethod]
        public void NodePropertyNameTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            System.Type type = typeof(HtmlNode);
            Assert.IsNotNull(type);
            IEnumerable<object> propertyNames = _database.GetList("NodePropertyName", "NodePropertyName");
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
            IEnumerable<object> classNames = _database.GetList("PublicationIdComparer", "ClassName");
            foreach (object className in classNames)
            {
                Assert.IsNotNull(Comparer.CreatePublicationComparer(className.ToString()));
            }
        }

        [TestMethod]
        public void HtmlMethodTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            IEnumerable<object> methodNames = _database.GetList("HtmlMethod", "MethodInfo");
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

            object siteId = _database.GetScalar((long) 2, Database.SiteTable);

            Mappings mappings = _database.GetMappings(siteId);

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

            object antiActionId = _database.GetScalar(actionId, "AntiActionId", "Action");
            object levelRegion = _database.GetScalar(mappingRegionId, Database.LevelColumn, "Region", siteId);
            object levelRubric = _database.GetScalar(mappingRubricId, Database.LevelColumn, "Rubric", siteId);
            object mappingParentRegionId = _database.GetScalar(mappingRegionId, Database.ParentIdColumn, "Region",
                siteId);
            object mappingParentRubricd = _database.GetScalar(mappingRubricId, Database.ParentIdColumn, "Rubric",
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

            var id = new RequestProperties
            {
                {"Action", actionId},
                {"Region", regionId},
                {"Rubric", rubricId},
                {Database.SiteTable, siteId}
            };
            IEnumerable<string> mappingTable = _database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());
            var mappingId = (RequestProperties) mappingTable.ToDictionary(item => item,
                item => _database.GetScalar(id[item], item, id.Site));
            mappingId.Site = id.Site;
            Values args = ParsingModule.Parser.BuildValues(_database, id, mappingId);
            Console.WriteLine(args.ToString());

            for (long level = 1; level <= Database.ConvertTo<long>(levelRegion); level++)
                Assert.IsTrue(args.ContainsKey(Regex.Escape(string.Format("{{{{RegionId[{0}]}}}}", level))));

            for (long level = 1; level <= Database.ConvertTo<long>(levelRubric); level++)
                Assert.IsTrue(args.ContainsKey(Regex.Escape(string.Format("{{{{RubricId[{0}]}}}}", level))));
        }

        [TestMethod]
        public void LastOrDefaultTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //

            object siteId = _database.GetScalar((long) 2, Database.SiteTable);

            Mappings mappings = _database.GetMappings(siteId);

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

            object antiActionId = _database.GetScalar(actionId, "AntiActionId", "Action");
            object levelRegion = _database.GetScalar(mappingRegionId, Database.LevelColumn, "Region", siteId);
            object levelRubric = _database.GetScalar(mappingRubricId, Database.LevelColumn, "Rubric", siteId);
            object mappingParentRegionId = _database.GetScalar(mappingRegionId, Database.ParentIdColumn, "Region",
                siteId);
            object mappingParentRubricd = _database.GetScalar(mappingRubricId, Database.ParentIdColumn, "Rubric",
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

            var id = new RequestProperties
            {
                {"Action", actionId},
                {"Region", regionId},
                {"Rubric", rubricId},
                {Database.SiteTable, siteId}
            };
            IEnumerable<string> mappingTable = _database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());
            var mappingId = (RequestProperties) mappingTable.ToDictionary(item => item,
                item => _database.GetScalar(id[item], item, id.Site));
            mappingId.Site = id.Site;
            Values args = ParsingModule.Parser.BuildValues(_database, id, mappingId);
            Console.WriteLine(args.ToString());

            for (long level = 1; level <= Database.ConvertTo<long>(levelRegion); level++)
                Assert.IsTrue(args.ContainsKey(Regex.Escape(string.Format("{{{{RegionId[{0}]}}}}", level))));

            for (long level = 1; level <= Database.ConvertTo<long>(levelRubric); level++)
                Assert.IsTrue(args.ContainsKey(Regex.Escape(string.Format("{{{{RubricId[{0}]}}}}", level))));
        }
    }
}