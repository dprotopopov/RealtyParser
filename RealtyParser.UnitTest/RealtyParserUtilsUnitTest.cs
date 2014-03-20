using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealtyParser;

namespace RealtyParser.UnitTest
{
    [TestClass]
    public class RealtyParserUtilsUnitTest
    {
        private static readonly ParserModule ParserModule = new ParserModule();
        private static readonly Parser Parser = ParserModule.Parser;
        private static readonly Transformation Transformation = ParserModule.Transformation;
        private static readonly Crawler Crawler = ParserModule.Crawler;

        [TestMethod]
        public void TestInvokeNodeProperty()
        {
            var document = new HtmlDocument();
            document.Load("TestInvokeNodeProperty.html");
            HtmlNode node = document.DocumentNode.SelectSingleNode("//a");
            string innerText = Parser.InvokeNodeProperty(node, "InnerText");
            Assert.AreEqual(innerText, "Hello");
        }

        [TestMethod]
        public void TestHrefValue()
        {
            var document = new HtmlDocument();
            document.Load("TestInvokeNodeProperty.html");
            HtmlNode node = document.DocumentNode.SelectSingleNode("//a[@href]");
            string hrefValue = Parser.AttributeValue(node, "href");
            Assert.AreEqual(hrefValue, "http://protopopov.ru");
        }

        [TestMethod]
        public void TestParseTemplate()
        {
            const string template = "RegionId={{Region}}&Rubric={{Rubric[1]}}&Action={{Action}}";
            var args = new Values
            {
                {@"Region", "1"},
                {@"Rubric[1]", "2"}
            };
            Assert.AreEqual(Transformation.ParseTemplate(template, args), "Region=1&Rubric=2&Action=");
        }

        [TestMethod]
        public async void TestWebRequestHtmlDocument()
        {
            var uri = new Uri("http://rbc.ru");
            Crawler.Method = "GET";
            Crawler.Encoding = "utf-8";
            Crawler.Compression = "NoCompression";
            IEnumerable<HtmlDocument> documents = await Crawler.WebRequestHtmlDocument(uri);
            Assert.IsNotNull(documents);
            Assert.IsFalse(string.IsNullOrEmpty(documents.First().DocumentNode.InnerText));
        }
    }
}