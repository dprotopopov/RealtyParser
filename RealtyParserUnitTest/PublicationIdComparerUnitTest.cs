using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser.Comparer;

namespace RealtyParserUnitTest
{
    /// <summary>
    ///     Сводное описание для PublicationIdComparerUnitTest
    /// </summary>
    [TestClass]
    public class PublicationIdComparerUnitTest
    {
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
        public void OnlyDatetimeComparerTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            var comparer = new OnlyDatetimeComparer();
            Assert.IsTrue(comparer.Compare("##11.03.2013 14:37:42##", "##10.04.2013 14:37:42##") < 0);
            Assert.IsTrue(comparer.Compare("[888,##11.03.2014 14:37:42##]", "##10.04.2013 14:37:42##") > 0);
            Assert.IsTrue(comparer.Compare("[222,##11.03.2013 14:37:42##]", "[1111,##11.03.2013 14:37:42##]") == 0);
        }

        [TestMethod]
        public void OnlyDatetimeComparer()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            var comparer = new OnlyDatetimeComparer();
            MatchCollection matchesX = Regex.Matches("[888,##11.03.2014 14:37:42##]",
                RealtyParser.Comparer.OnlyDatetimeComparer.DateTimePatten);
            DateTime dateTimeX = (matchesX.Count > 0)
                ? RealtyParser.Types.DateTime.Parse(matchesX[0].Groups["date"].Value)
                : DateTime.Now;
            Console.WriteLine(dateTimeX);
        }
    }
}