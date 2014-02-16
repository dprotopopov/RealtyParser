using System;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser;

namespace RealtyParserUnitTest
{
    [TestClass]
    public class RealtyParserUtilsUnitTest
    {
        [TestMethod]
        public void TestInvokeNodeProperty()
        {
            HtmlDocument document = new HtmlDocument();
            document.Load("TestInvokeNodeProperty.html");
            HtmlNode node = document.DocumentNode.SelectSingleNode("//a");
            string innerText = Parser.InvokeNodeProperty(node, "InnerText");
            Assert.AreEqual(innerText, "Hello");
        }

        [TestMethod]
        public void TestHrefValue()
        {
            HtmlDocument document = new HtmlDocument();
            document.Load("TestInvokeNodeProperty.html");
            HtmlNode node = document.DocumentNode.SelectSingleNode("//a[@href]");
            string hrefValue = Parser.AttributeValue(node,"href");
            Assert.AreEqual(hrefValue, "http://protopopov.ru");
        }

        [TestMethod]
        public void TestParseTemplate()
        {
            const string template = "RegionId={{Region}}&Rubric={{Rubric[1]}}&Action={{Action}}";
            Values args = new Values
            {
                {@"\{\{Region\}\}", "1"},
                {@"\{\{Rubric\[1\]\}\}", "2"}
            };
            Assert.AreEqual(new Transformation().ParseTemplate(template, args), "Region=1&Rubric=2&Action=");
        }

        [TestMethod]
        public async void TestWebRequestHtmlDocument()
        {
            Uri uri = new Uri("http://rbc.ru");
            HtmlDocument[] documents = await ParsingModule.Parser.WebRequestHtmlDocument(uri, "GET", "utf-8");
            Assert.IsNotNull(documents);
            Assert.IsFalse(string.IsNullOrEmpty(documents[0].DocumentNode.InnerText));
        }
    }
}
