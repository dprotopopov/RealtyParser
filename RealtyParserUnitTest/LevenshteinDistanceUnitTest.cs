using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser;

namespace RealtyParserUnitTest
{
    /// <summary>
    /// Сводное описание для LevenshteinDistanceUnitTest
    /// </summary>
    [TestClass]
    public class LevenshteinDistanceUnitTest
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
        public void TestLevenshteinDistance()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            Assert.AreEqual(LevenshteinDistance.Compute("aunt", "ant"), 1);
            Assert.AreEqual(LevenshteinDistance.Compute("Sam", "Samantha"), 5);
            Assert.AreEqual(LevenshteinDistance.Compute("flomax", "volmax"), 3);
        }
    }
}
