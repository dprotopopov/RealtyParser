using System;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RealtyParser.UnitTest
{
    /// <summary>
    /// Сводное описание для HapUnitTest
    /// </summary>
    [TestClass]
    public class HapUnitTest
    {
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
        public void TestXPath1()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            HtmlDocument document = new HtmlDocument();
            document.Load("xpath1.html");
            string xpath = @"(//p[contains(text(),'Параметры')]/..//p[@class='obj_profile'])";
            var nodes = document.DocumentNode.SelectNodes(xpath);
            
            Assert.IsNotNull(nodes);
            foreach (var node in nodes)
            {
                Console.WriteLine(node.InnerText);
            }
        }
    }
}
