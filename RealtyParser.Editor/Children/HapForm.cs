// compile with: /reference:HtmlAgilityPack=HtmlAgilityPack.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using HtmlAgilityPack;
using RT.Crawler;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace RealtyParser.Editor.Children
{
    public partial class HapForm : Form, IChildForm
    {
        private static readonly ParserModule ParserModule = new ParserModule();
        private static readonly Database Database = ParserModule.Database;
        private static readonly Crawler Crawler = ParserModule.LookupCrawler;
        private readonly Workspace _workspace = new Workspace();

        public HapForm()
        {
            InitializeComponent();

            Database.Connect();

            repositoryItemComboBoxMethod.Items.AddRange(
                Database.GetList("HtmlMethod", "Method").Select(s => s.ToString()).Cast<object>().ToArray());
            repositoryItemComboBoxEncoding.Items.AddRange(
                Database.GetList("HtmlEncoding", "Encoding").Select(s => s.ToString()).Cast<object>().ToArray());
            repositoryItemComboBoxUrl.Items.AddRange(
                Database.GetList("Site", "Url").Select(s => s.ToString()).Cast<object>().ToArray());
            repositoryItemComboBoxCompression.Items.AddRange(
                Database.GetList("Compression", "ClassName").Select(s => s.ToString()).Cast<object>().ToArray());
            propertyGridControlWorkspace.SelectedObject = _workspace;
        }

        public async void Save()
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            var builder = new UriBuilder(_workspace.Url);
            ICrawler crawler = new WebCrawler();
            var requestWeb = (HttpWebRequest) WebRequest.Create(builder.Uri);
            requestWeb.Method = _workspace.Method;
            WebResponse responce = await crawler.GetResponse(requestWeb);
            Stream responceStream = responce.GetResponseStream();
            using (Stream writer = File.Create(saveFileDialog1.FileName))
            {
                responceStream.CopyTo(writer);
                writer.Close();
            }
        }

        public void ClearResults()
        {
        }

        public async void Execute()
        {
            propertyGridControlNode.SelectedObject = null;
            listBoxNode.SelectedItem = null;
            listBoxDocument.SelectedItem = null;
            listBoxNode.Items.Clear();
            listBoxDocument.Items.Clear();
            textBox1.Clear();
            try
            {
                var builder = new UriBuilder(_workspace.Url);
                Crawler.Method = _workspace.Method;
                Crawler.Encoding = _workspace.Encoding;
                Crawler.Compression = _workspace.Compression;
                Crawler.Request = _workspace.Request;
                IEnumerable<MemoryStream> documents =
                    await Crawler.WebRequest(builder.Uri);

                listBoxDocument.Items.AddRange(documents.Cast<object>().ToArray());
//                webBrowser1.Navigate(_workspace.Url);
            }
            catch (Exception exception)
            {
                textBox1.Text = exception.ToString();
            }
        }

        private void listBoxDocument_SelectedValueChanged(object sender, EventArgs e)
        {
            propertyGridControlNode.SelectedObject = null;
            listBoxNode.SelectedItem = null;
            listBoxNode.Items.Clear();
            textBox1.Clear();
            try
            {
                var stream = (MemoryStream) listBoxDocument.SelectedItem;
                stream.Seek(0, SeekOrigin.Begin);
                if (!string.IsNullOrEmpty(_workspace.Xpath))
                {
                    var document = new HtmlDocument();
                    document.Load(stream, System.Text.Encoding.Default);
                    textBox1.Text = document.DocumentNode.OuterHtml;
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(_workspace.Xpath);
                    listBoxNode.Items.AddRange(nodes.Cast<object>().ToArray());
                }
                else
                {
                    using (var sr = new StreamReader(stream))
                        textBox1.Text = sr.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                textBox1.Text = exception.ToString();
            }
        }

        private void listBoxNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlNode.SelectedObject = null;
            propertyGridControlNode.SelectedObject = listBoxNode.SelectedItem;
        }

        private class Workspace
        {
            public string Url { get; set; }
            public string Xpath { get; set; }
            public string Method { get; set; }
            public string Encoding { get; set; }
            public string Compression { get; set; }
            public string Request { get; set; }
        }
    }
}