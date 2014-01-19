using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser;

namespace RealtyParserUnitTest
{
    /// <summary>
    /// Сводное описание для DatabaseDataTest
    /// </summary>
    [TestClass]
    public class DatabaseDataTest
    {
        readonly RealtyParserDatabase _database = new RealtyParserDatabase();

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
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
            foreach (var tableName in _database.GetList<string>("Mapping", "TableName"))
            {
                var table = _database.GetDictionary<long>(tableName);
                Assert.IsTrue(table.Count > 0);
            }
        }
        [TestMethod]
        public void MappingTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            const long siteId = 1;
            foreach (var mappedTableName in _database.GetList<string>("Mapping", "TableName"))
            {
                var dictionary = _database.GetDictionary<long>("Site" + mappedTableName + "Mapping", "" + mappedTableName + "Id", "Site" + mappedTableName + "Id", siteId);
                Assert.IsTrue(dictionary.Count > 0);
                Assert.IsTrue(!String.IsNullOrEmpty(dictionary[1]));
            }
        }
        [TestMethod]
        public void PropertiesTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            const long siteId = 1;
            SiteProperties properties = _database.GetSiteProperties(siteId);
            Assert.IsTrue(!String.IsNullOrEmpty(properties.Mapping.Region[1]) && !String.IsNullOrEmpty(properties.Mapping.Rubric[1]) && !String.IsNullOrEmpty(properties.Mapping.Action[1]));
        }

        [TestMethod]
        public void NodePropertyNameTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            Type type = typeof(HtmlAgilityPack.HtmlNode);
            Assert.IsNotNull(type);
            List<string> propertyNames = _database.GetList<string>("NodePropertyName", "NodePropertyName");
            foreach (string propertyName in propertyNames)
            {
                PropertyInfo propertyInfo = type.GetProperty(propertyName);
                Assert.IsTrue(propertyInfo != null ||
                    String.Compare(propertyName, "href", StringComparison.Ordinal) == 0 ||
                    String.Compare(propertyName, "src", StringComparison.Ordinal) == 0 ||
                    String.Compare(propertyName, "value", StringComparison.Ordinal) == 0
                    );
            }
        }
        [TestMethod]
        public void PublicationIdComparerTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            List<string> classNames = _database.GetList<string>("PublicationIdComparer", "ClassName");
            foreach (string className in classNames)
            {
                Assert.IsNotNull(RealtyParserUtils.CreatePublicationIdComparer(className));
            }
        }
        [TestMethod]
        public void HtmlMethodTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            List<string> methodNames = _database.GetList<string>("HtmlMethod", "Method");
            Assert.AreEqual(methodNames.Count, 2);
            Assert.IsTrue(methodNames.Contains("GET"));
            Assert.IsTrue(methodNames.Contains("POST"));
        }
        [TestMethod]
        public void FirstOrDefaultTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //

            long siteId = 2;

            long regionId;
            long rubricId;
            long actionId;

            SiteProperties properties = _database.GetSiteProperties(siteId);
            Mapping mapping = properties.Mapping;

            regionId = properties.Mapping.Region.Keys.FirstOrDefault();
            rubricId = properties.Mapping.Rubric.Keys.FirstOrDefault();
            actionId = properties.Mapping.Action.Keys.FirstOrDefault();

            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("siteId -> " + siteId);
            Console.WriteLine("regionId -> " + regionId);
            Console.WriteLine("rubricId -> " + rubricId);
            Console.WriteLine("actionId -> " + actionId);
            Console.WriteLine("-------------------------------------------------------------------");

            Assert.IsTrue(properties.Mapping.Action.ContainsKey(actionId));
            Assert.IsTrue(properties.Mapping.Rubric.ContainsKey(rubricId));
            Assert.IsTrue(properties.Mapping.Region.ContainsKey(regionId));

            string mappingRegionId = mapping.Region[regionId];
            string mappingRubricId = mapping.Rubric[rubricId];
            string mappingActionId = mapping.Action[actionId];

            long antiActionId = _database.GetScalar<long, long>(actionId, "AntiActionId", "Action");
            long levelRegion = _database.GetScalar<long, string>(mappingRegionId, "Level", "Region", siteId);
            long levelRubric = _database.GetScalar<long, string>(mappingRubricId, "Level", "Rubric", siteId);
            string mappingParentRegionId = _database.GetScalar<string, string>(mappingRegionId, "ParentId", "Region", siteId);
            string mappingParentRubricd = _database.GetScalar<string, string>(mappingRubricId, "ParentId", "Rubric", siteId);
            string mappingAntiActionId = mapping.Action[antiActionId];

            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Region[" + regionId + "] -> '" + mappingRegionId + "', Level=" + levelRegion + ", Parent='" + mappingParentRegionId + "'");
            Console.WriteLine("Rubric[" + rubricId + "] -> '" + mappingRubricId + "', Level=" + levelRubric + ", Parent='" + mappingParentRubricd + "'");
            Console.WriteLine("Action[" + actionId + "] -> '" + mappingActionId + "', AntiAction[" + antiActionId + "] -> '" + mappingAntiActionId + "'");
            Console.WriteLine("-------------------------------------------------------------------");

            Arguments args = RealtyParserUtils.BuildArguments(_database, regionId, rubricId, actionId, "lastPublicationId", properties.Mapping, siteId);
            foreach (var arg in args)
                Console.WriteLine(arg.Key + " <-> " + arg.Value);

            for (long level = 1; level <= levelRegion; level++)
                Assert.IsTrue(args.ContainsKey(@"\{\{RegionId\[" + level + @"\]\}\}"));

            for (long level = 1; level <= levelRubric; level++)
                Assert.IsTrue(args.ContainsKey(@"\{\{RubricId\[" + level + @"\]\}\}"));
        }
        [TestMethod]
        public void LastOrDefaultTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //

            long siteId = 2;

            long regionId;
            long rubricId;
            long actionId;

            SiteProperties properties = _database.GetSiteProperties(siteId);
            Mapping mapping = properties.Mapping;

            regionId = properties.Mapping.Region.Keys.LastOrDefault();
            rubricId = properties.Mapping.Rubric.Keys.LastOrDefault();
            actionId = properties.Mapping.Action.Keys.LastOrDefault();

            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("siteId -> " + siteId);
            Console.WriteLine("regionId -> " + regionId);
            Console.WriteLine("rubricId -> " + rubricId);
            Console.WriteLine("actionId -> " + actionId);
            Console.WriteLine("-------------------------------------------------------------------");

            Assert.IsTrue(properties.Mapping.Action.ContainsKey(actionId));
            Assert.IsTrue(properties.Mapping.Rubric.ContainsKey(rubricId));
            Assert.IsTrue(properties.Mapping.Region.ContainsKey(regionId));

            string mappingRegionId = mapping.Region[regionId];
            string mappingRubricId = mapping.Rubric[rubricId];
            string mappingActionId = mapping.Action[actionId];

            long antiActionId = _database.GetScalar<long, long>(actionId, "AntiActionId", "Action");
            long levelRegion = _database.GetScalar<long, string>(mappingRegionId, "Level", "Region", siteId);
            long levelRubric = _database.GetScalar<long, string>(mappingRubricId, "Level", "Rubric", siteId);
            string mappingParentRegionId = _database.GetScalar<string, string>(mappingRegionId, "ParentId", "Region", siteId);
            string mappingParentRubricd = _database.GetScalar<string, string>(mappingRubricId, "ParentId", "Rubric", siteId);
            string mappingAntiActionId = mapping.Action[antiActionId];

            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Region[" + regionId + "] -> '" + mappingRegionId + "', Level=" + levelRegion + ", Parent='" + mappingParentRegionId + "'");
            Console.WriteLine("Rubric[" + rubricId + "] -> '" + mappingRubricId + "', Level=" + levelRubric + ", Parent='" + mappingParentRubricd + "'");
            Console.WriteLine("Action[" + actionId + "] -> '" + mappingActionId + "', AntiAction[" + antiActionId + "] -> '" + mappingAntiActionId + "'");
            Console.WriteLine("-------------------------------------------------------------------");

            Arguments args = RealtyParserUtils.BuildArguments(_database, regionId, rubricId, actionId, "lastPublicationId", properties.Mapping, siteId);
            foreach (var arg in args)
                Console.WriteLine(arg.Key + " <-> " + arg.Value);

            for (long level = 1; level <= levelRegion; level++)
                Assert.IsTrue(args.ContainsKey(@"\{\{RegionId\[" + level + @"\]\}\}"));

            for (long level = 1; level <= levelRubric; level++)
                Assert.IsTrue(args.ContainsKey(@"\{\{RubricId\[" + level + @"\]\}\}"));
        }
    }
}
