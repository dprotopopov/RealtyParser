using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using RT.Crawler;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;

namespace RT.ParsingLibs.AbstractFirst
{
    /// <summary>
    /// Пример реализации парснг-модуля
    /// </summary>
    [Export(typeof(IParsingModule))]
    [ExportMetadata("Name", "AbstractFirstParsingModule")]
    public class AbstractFirstParsingModule : IParsingModule
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
            var reader = new StreamReader(responce.GetResponseStream());
            var output = new StringBuilder();
            output.Append(reader.ReadToEnd());


            return new ParseResponse() 
            { 
                ResponseCode = ParseResponseCode.Success,
                LastPublicationId = "rtterterret",
                ModuleName = "AbstractFirstParsingModule",
                Publications = new[]
                {
                    new WebPublication()
                    {
                        ActionId = 1,
                        AdditionalInfo = new AdditionalInfo()
                        {
                            RealtyAdditionalInfo = new RealtyAdditionalInfo()
                            {
                                Address = "dsafsfdsfdsds",
                                AppointmentOfRoom = "rerwe",
                                CostAll = 1234.89m
                            }
                        },
                        Contact = new WebPublicationContact()
                        {
                            Author = "Vasya",
                            AuthorUrl = new Uri("http://www.vdvds.ru"),
                            ContactName = "Dfrt",
                            Email = new []{"sadds@wewq.ru"},
                            Icq = 234332243,
                            Phone = new []{"42332432432"},
                            Skype = "dfg.lop"
                        },
                        Description = "fgdgfdfdgfdgfd",
                        ModifyDate = DateTime.UtcNow,
                        Photos = new Uri[1],
                        PublicationId = "fgfdgfdgfd",
                        RegionId = 1,
                        RubricId = 1,
                        Site = new Uri("http://www.ert.ru"),
                        Url = new Uri("http://www.ert.ru/1")
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
                Info = "John Korn",
                Contacts = "vab@root.com",
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
                new Bind(){RegionId = 1, RubricId = 1, ActionId = 1},
                new Bind(){RegionId = 2, RubricId = 2, ActionId = 2},
            };
        }
    }
}
