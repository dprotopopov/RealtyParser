using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser;

namespace RealtyParser.UnitTest
{
    /// <summary>
    ///     Сводное описание для RosrealtUnitTest
    /// </summary>
    [TestClass]
    public class RosrealtUnitTest
    {
        private const long SiteId = 2;
        private static readonly ParserModule ParserModule = new ParserModule();
        private static readonly Database Database = ParserModule.Database;

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
        public void TestRosrealt()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            object siteId = Database.GetScalar(SiteId, Database.SiteTable);
            Mappings mappings = Database.GetMappings(siteId);
            Assert.IsTrue(mappings.Action.Count > 0);
            Assert.IsTrue(mappings.Rubric.Count > 0);
            Assert.IsTrue(mappings.Region.Count > 0);
        }
    }
}