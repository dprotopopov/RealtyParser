using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RealtyParser.Compression;
using RT.Crawler;
using TidyManaged;

namespace RealtyParser
{
    public class Crawler
    {
        public Crawler()
        {
            Method = "GET";
            Encoding = "utf-8";
            Compression = "NoCompression";
        }

        public ProgressCallback ProgressCallback { get; set; }
        public AppendLineCallback AppendLineCallback { get; set; }
        public CompliteCallback CompliteCallback { get; set; }
        public CompressionManager CompressionManager { get; set; }
        public string Method { get; set; }
        public string Encoding { get; set; }
        public string Compression { get; set; }

        /// <summary>
        ///     Запрос к сайту с использованием RT.Crawler
        /// </summary>
        public async Task<IEnumerable<HtmlDocument>> WebRequestHtmlDocument(Uri uri, bool async = true)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine(uri.ToString());
            long current = 0;
            long total = 1;
            var collection = new List<HtmlDocument>();
            try
            {
                ICompression compression = CompressionManager.CreateCompression(Compression);
                Encoding encoder = System.Text.Encoding.GetEncoding(Encoding);

                MemoryStream memoryStream = null;
                for (int i = 0; i < 3 && memoryStream == null; i++)
                    try
                    {
                        ICrawler crawler = new WebCrawler();
                        HttpWebRequest requestWeb = WebRequest.CreateHttp(uri);
                        requestWeb.KeepAlive = false;
                        requestWeb.AutomaticDecompression = DecompressionMethods.None;
                        WebResponse responce;
                        if (async)
                        {
                            responce = await crawler.GetResponse(requestWeb);
                        }
                        else
                        {
                            responce = requestWeb.GetResponse();
                        }
                        var decompressedStream = new MemoryStream();
                        Stream responceStream = responce.GetResponseStream();
                        compression.Decompress(responceStream, decompressedStream);
                        decompressedStream.Seek(0, SeekOrigin.Begin);
                        var decodedReader = new StreamReader(decompressedStream, encoder);
                        memoryStream = new MemoryStream(System.Text.Encoding.Default.GetBytes(decodedReader.ReadToEnd()));
                    }
                    catch (WebException exception)
                    {
                        Debug.WriteLine(exception.ToString());
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.ToString());
                    }

                if (ProgressCallback != null) ProgressCallback(++current, ++total);

                if (memoryStream != null)
                {
                    var memoryStreams = new List<MemoryStream> {memoryStream};
                    if (ProgressCallback != null) ProgressCallback(++current, ++total);

                    memoryStreams.First().Seek(0, SeekOrigin.Begin);
                    Document tidy = Document.FromStream(memoryStreams.First());

                    tidy.ForceOutput = true;
                    tidy.PreserveEntities = true;
                    tidy.InputCharacterEncoding = EncodingType.Raw;
                    tidy.OutputCharacterEncoding = EncodingType.Raw;
                    tidy.CharacterEncoding = EncodingType.Raw;
                    tidy.ShowWarnings = false;
                    tidy.Quiet = true;
                    tidy.OutputXhtml = true;
                    tidy.CleanAndRepair();

                    memoryStreams.Add(new MemoryStream());
                    tidy.Save(memoryStreams.Last());

                    if (ProgressCallback != null) ProgressCallback(++current, ++total);

                    foreach (MemoryStream stream in memoryStreams)
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        var edition = new HtmlDocument();
                        edition.Load(stream, System.Text.Encoding.Default);
                        collection.Add(edition);
                    }
                    if (ProgressCallback != null) ProgressCallback(++current, total);
                }
            }
            catch (WebException exception)
            {
            }
            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return collection;
        }
    }
}