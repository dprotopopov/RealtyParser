using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TidyManaged;

namespace RealtyParser.UnitTest
{
    /// <summary>
    ///     Сводное описание для TidyManagedUnitTest
    /// </summary>
    [TestClass]
    public class TidyManagedUnitTest
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
        public void TidyManagedStringTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            using (
                Document doc =
                    Document.FromString(
                        @"


<hTml><META content=""text/html; charset=windows-1251"" http-equiv=Content-Type>
<title>test</tootle><body>Привет"))
            {
                doc.ShowWarnings = false;
                doc.Quiet = true;
                doc.OutputXhtml = true;
                doc.CleanAndRepair();
                string parsed = doc.Save();
                Console.WriteLine(parsed);
                Assert.AreEqual(parsed, @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN""
    ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
<meta name=""generator"" content=
""HTML Tidy for Windows (vers 14 October 2008), see www.w3.org"" />
<title>test</title>
</head>
<body>
Привет
</body>
</html>");
            }
        }

        [TestMethod]
        public void TidyManagedFileTest()
        {
            //
            // TODO: добавьте здесь логику теста
            //
            using (Document doc = Document.FromFile("TidyManagedTest.html"))
            {
                doc.ForceOutput = true;
                doc.ShowWarnings = false;
                doc.Quiet = true;
                doc.OutputXhtml = true;
                doc.CleanAndRepair();
                string parsed = doc.Save();
                Console.WriteLine(parsed);
                Assert.AreEqual(2, Regex.Matches(parsed, @"\<\/(body|html)\>", RegexOptions.IgnoreCase).Count);
            }
        }
    }
}