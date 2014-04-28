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
using MyLibrary.Trace;
using MyParser.Compression;
using RealtyParser.Collections;
using RT.Crawler;
using TidyManaged;

namespace RealtyParser
{
    public class Crawler : MyParser.Crawler, ITrace, IValueable
    {
        public Crawler()
        {
            Method = "GET";
            Encoding = "utf-8";
            Compression = "NoCompression";
        }

        public new Values ToValues()
        {
            return new Values(this);
        }

        /// <summary>
        ///     Запрос к сайту с использованием RT.Crawler
        /// </summary>
        public async Task<IEnumerable<HtmlDocument>> WebRequestHtmlDocument(Uri uri, bool async = true)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine(uri.ToString());
            long current = 0;
            long total = 1;
            var collection = new StackListQueue<HtmlDocument>();
            try
            {
                ICompression compression = CompressionManager.CreateCompression(Compression);
                Encoding encoder = System.Text.Encoding.GetEncoding(Encoding);

                MemoryStream memoryStream = null;
                for (int i = 0; i < 3 && memoryStream == null; i++)
                    try
                    {
                        ICrawler crawler = new WebCrawler();
                        var httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
                        httpWebRequest.CookieContainer = new CookieContainer();
                        httpWebRequest.AutomaticDecompression = DecompressionMethods.None;
                        httpWebRequest.ContentType = string.Format(@"text/html; charset={0}", Encoding);
                        httpWebRequest.Referer = @"http://yandex.ru";
                        httpWebRequest.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                        httpWebRequest.UserAgent =
                            @"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:20.0) Gecko/20100101 Firefox/20.0";
                        httpWebRequest.KeepAlive = true;
                        if (string.Compare(Method, "JSON", StringComparison.Ordinal) == 0)
                        {
                            httpWebRequest.ContentType = "application/json; charset=utf-8";
                            httpWebRequest.Accept = "application/json, text/javascript, */*";
                        }

                        if (string.Compare(Method, "JSON", StringComparison.Ordinal) == 0 ||
                            string.Compare(Method, "POST", StringComparison.Ordinal) == 0)
                        {
                            httpWebRequest.Method = "POST";

                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                Debug.WriteLine("Request: {0}", Request);
                                streamWriter.Write(Request);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }
                        }
                        else
                            httpWebRequest.Method = Method;

                        WebResponse responce;
                        if (async)
                        {
                            responce = await crawler.GetResponse(httpWebRequest);
                        }
                        else
                        {
                            responce = httpWebRequest.GetResponse();
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
                        Debug.WriteLine(exception.Message);
                        Debug.WriteLine(exception.InnerException.ToString());
                        Debug.WriteLine(exception.InnerException.InnerException.ToString());
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.ToString());
                    }

                if (ProgressCallback != null) ProgressCallback(++current, ++total);

                if (memoryStream != null)
                {
                    var memoryStreams = new StackListQueue<MemoryStream> {memoryStream};
                    if (ProgressCallback != null) ProgressCallback(++current, ++total);

                    Debug.WriteLine("Run Tydy.Document");
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
                Debug.WriteLine(exception.ToString());
            }
            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return collection;
        }
    }
}