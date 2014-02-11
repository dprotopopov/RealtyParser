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
            const string template = "RegionId={{RegionId}}&RubricId={{RubricId[1]}}&ActionId={{ActionId}}";
            Values args = new Values
            {
                {@"\{\{RegionId\}\}", "1"},
                {@"\{\{RubricId\[1\]\}\}", "2"}
            };
            Assert.AreEqual(RealtyParserParsingModule.Parser.ParseTemplate(template, args), "RegionId=1&RubricId=2&ActionId=");
        }

        [TestMethod]
        public async void TestWebRequestHtmlDocument()
        {
            Uri uri = new Uri("http://rbc.ru");
            HtmlDocument[] documents = await RealtyParserParsingModule.Parser.WebRequestHtmlDocument(uri, "GET", "utf-8");
            Assert.IsNotNull(documents);
            Assert.IsFalse(System.String.IsNullOrEmpty(documents[0].DocumentNode.InnerText));
        }
    }
}
