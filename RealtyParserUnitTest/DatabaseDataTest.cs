using System;
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
                Assert.IsNotNull(propertyInfo);
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
    }
}
