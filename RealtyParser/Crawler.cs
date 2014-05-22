using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MyLibrary.Trace;
using MyParser;
using MyParser.Compression;
using RealtyParser.Collections;
using RT.Crawler;
using TidyManaged;
using ICrawler = RT.Crawler.ICrawler;

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
        ///     Запрос к сайту с использованием RT.LookupCrawler
        /// </summary>
        public async Task<IEnumerable<MemoryStream>> WebRequest(Uri uri, bool async = true)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine(uri.ToString());
            long current = 0;
            long total = 1;
            var memoryStreams = new StackListQueue<MemoryStream>();
            try
            {
                ICompression compression = CompressionManager.CreateCompression(Compression);
                Encoding encoder = System.Text.Encoding.GetEncoding(Encoding);

                for (int i = 0; i < 3 && !memoryStreams.Any(); i++)
                    try
                    {
                        ICrawler crawler = new WebCrawler();
                        var httpWebRequest = (HttpWebRequest) System.Net.WebRequest.Create(uri);
                        httpWebRequest.CookieContainer = new CookieContainer();
                        httpWebRequest.AutomaticDecompression = DecompressionMethods.None;
                        httpWebRequest.Referer = @"http://yandex.ru";
                        httpWebRequest.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                        httpWebRequest.UserAgent =
                            @"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:20.0) Gecko/20100101 Firefox/20.0";
                        httpWebRequest.KeepAlive = true;
                        if (string.Compare(Method, "JSON", StringComparison.Ordinal) == 0)
                        {
                            httpWebRequest.ContentType = "application/json; charset=utf-8";
                            httpWebRequest.Accept = "application/json, text/javascript, */*";
                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                Debug.WriteLine("Request: {0}", Request);
                                streamWriter.Write(Request);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }
                        }

                        if (string.Compare(Method, "POST", StringComparison.Ordinal) == 0)
                        {
                            httpWebRequest.Method = "POST";
                            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                Debug.WriteLine("Request: {0}", HttpUtility.UrlEncode(Request));
                                streamWriter.Write(Request);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }
                        }

                        if (string.Compare(Method, "GET", StringComparison.Ordinal) == 0)
                        {
                            httpWebRequest.Method = Method;
                            httpWebRequest.ContentType = string.Format(@"text/html; charset={0}", Encoding);
                        }

                        WebResponse responce = async
                            ? await crawler.GetResponse(httpWebRequest)
                            : httpWebRequest.GetResponse();

                        var decompressedStream = new MemoryStream();
                        Stream responceStream = responce.GetResponseStream();
                        compression.Decompress(responceStream, decompressedStream);
                        decompressedStream.Seek(0, SeekOrigin.Begin);
                        var decodedReader = new StreamReader(decompressedStream, encoder);
                        memoryStreams.Add(
                            new MemoryStream(System.Text.Encoding.Default.GetBytes(decodedReader.ReadToEnd())));
                    }
                    catch (AggregateException)
                    {
                        throw;
                    }
                    catch (WebException)
                    {
                        throw;
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.ToString());
                    }

                if (ProgressCallback != null) ProgressCallback(++current, ++total);

                if (memoryStreams.Any())
                {
                    if ((Edition & (int) DocumentEdition.Tided) != 0)
                    {
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
                    }
                    if ((Edition & (int) DocumentEdition.Original) == 0)
                        memoryStreams.Dequeue();

                    if (ProgressCallback != null) ProgressCallback(++current, ++total);

                    if (ProgressCallback != null) ProgressCallback(++current, total);
                }
            }
            catch (AggregateException exception)
            {
                Debug.WriteLine(exception.ToString());
            }
            catch (WebException exception)
            {
                Debug.WriteLine(exception.ToString());
            }
            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return memoryStreams;
        }
    }
}