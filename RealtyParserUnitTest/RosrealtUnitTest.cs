using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser;

namespace RealtyParserUnitTest
{
    /// <summary>
    /// Сводное описание для RosrealtUnitTest
    /// </summary>
    [TestClass]
    public class RosrealtUnitTest
    {
        static readonly RealtyParserDatabase Database = RealtyParserUtils.GetDatabase();
        const long SiteId = 2;
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
        public void TestRosrealt()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            SiteProperties properties = Database.GetSiteProperties(SiteId);
            Assert.IsTrue(properties.Mapping.Action.Count > 0);
            Assert.IsTrue(properties.Mapping.Rubric.Count > 0);
            Assert.IsTrue(properties.Mapping.Region.Count > 0);
        }
    }
}
