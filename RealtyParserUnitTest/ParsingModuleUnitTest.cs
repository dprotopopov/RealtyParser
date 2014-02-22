using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Responses;

namespace RealtyParserUnitTest
{
    /// <summary>
    /// Сводное описание для ParsingModuleUnitTest
    /// </summary>
    [TestClass]
    public class ParsingModuleUnitTest
    {
        ParserModule module = new ParserModule();

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
        public void TestAbout()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            AboutResponse response = module.About();
            Assert.AreEqual(response.Info, "Dmitry Protopopov");
            Assert.AreEqual(response.Contacts, "dmitry@protopopov.ru");
            Assert.AreEqual(response.CopyRight, "All reserved");
        }
        [TestMethod]
        public void TestSources()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            Bind bind = new Bind { ActionId = 1, RubricId = 1, RegionId = 1 };
            IList<string> sources = module.Sources(bind);
            Assert.IsTrue(sources.Count > 0);
        }
    }
}
