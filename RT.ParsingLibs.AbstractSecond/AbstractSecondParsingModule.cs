using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using RT.Crawler;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;

namespace RT.ParsingLibs.AbstractSecond
{
    /// <summary>
    /// Пример реализации парснг-модуля
    /// </summary>
    [Export(typeof(IParsingModule))]
    [ExportMetadata("Name", "AbstractSecondParsingModule")]
    public class AbstractSecondParsingModule : IParsingModule
    {
        /// <summary>
        /// Html-граббер
        /// </summary>
        readonly ICrawler _crawler = new WebCrawler();

        /// <summary>
        /// Задача на парсинг
        /// </summary>
        /// <param name="request">Запрос на парсинг</param>
        /// <returns>Ответ от парсера</returns>
        public async Task<ParseResponse> Result(ParseRequest request)
        {
            var requestWeb = (HttpWebRequest)WebRequest.Create("http://6min.ru/");
            requestWeb.Method = "GET";

            var responce = await _crawler.GetResponse(requestWeb);
            StreamReader reader = new StreamReader(responce.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());

            return new ParseResponse()
            {
                ResponseCode = ParseResponseCode.Success,
                LastPublicationId = "tretrete",
                ModuleName = "AbstractSecondParsingModule",
                Publications = new[]
                {
                    new WebPublication()
                    {
                        ActionId = 2,
                        AdditionalInfo = new AdditionalInfo()
                        {
                            RealtyAdditionalInfo = new RealtyAdditionalInfo()
                            {
                                Address = "fdsfs",
                                AppointmentOfRoom = "fds",
                                CostAll = 3123.5m
                            }
                        },
                        Contact = new WebPublicationContact()
                        {
                            Author = "Vasya",
                            AuthorUrl = new Uri("http://www.111.ru"),
                            ContactName = "Dfrt",
                            Email = new []{"sadds@22.ru"},
                            Icq = 12121,
                            Phone = new []{"e3r32"},
                            Skype = "d222fg.lop"
                        },
                        Description = "aaaaa",
                        ModifyDate = DateTime.UtcNow,
                        Photos = new Uri[1],
                        PublicationId = "aaaa",
                        RegionId = 2,
                        RubricId = 1,
                        Site = new Uri("http://www.tret.ru"),
                        Url = new Uri("http://www.tret.ru/1")
                    },
                    new WebPublication()
                    {
                        ActionId = 3,
                        AdditionalInfo = new AdditionalInfo()
                        {
                            RealtyAdditionalInfo = new RealtyAdditionalInfo()
                            {
                                Address = "fdsfs",
                                AppointmentOfRoom = "fds",
                                CostAll = 3123.5m
                            }
                        },
                        Contact = new WebPublicationContact()
                        {
                            Author = "Vasya",
                            AuthorUrl = new Uri("http://www.111.ru"),
                            ContactName = "Dfrt",
                            Email = new []{"sadds@22.ru"},
                            Icq = 12121,
                            Phone = new []{"e3r32"},
                            Skype = "d222fg.lop"
                        },
                        Description = "aaaaa",
                        ModifyDate = DateTime.UtcNow,
                        Photos = new Uri[1],
                        PublicationId = "aaaa",
                        RegionId = 2,
                        RubricId = 1,
                        Site = new Uri("http://www.tret.ru"),
                        Url = new Uri("http://www.tret.ru/1")
                    }
                }
            };
        }

        /// <summary>
        /// Получить названия ресурсов, обрабатываемая библиотекой
        /// </summary>
        /// <param name="bind">Бинд запроса</param>
        /// <returns> Коллекция названий ресурсов (сайтов)</returns>
        public IList<string> Sources(Bind bind)
        {
            return new List<string>() 
            {
                "terot.ru",
                "asdfg.com",
            };
        }

        /// <summary>
        /// Получить информацию о разработчике
        /// </summary>
        /// <returns>Информация о разработчике</returns>
        public AboutResponse About()
        {
            return new AboutResponse()
            {
                Info = "uytutyu Korn",
                Contacts = "vab@royot.com",
                CopyRight = "All reserved"
            };
        }

        /// <summary>
        /// Получить список биндов, обрабатываемая библиотекой
        /// </summary>
        /// <returns>Коллекция биндов</returns>
        public IList<Bind> Keys()
        {
            return new List<Bind>() 
            {
                new Bind(){RegionId = 3, RubricId = 3, ActionId = 3},
                new Bind(){RegionId = 2, RubricId = 2, ActionId = 2},
            };
        }
    }
}
