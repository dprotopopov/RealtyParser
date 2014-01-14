using System.Collections.Generic;
using System.Threading.Tasks;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;

namespace RT.ParsingLibs
{
    /// <summary>
    /// Интерфейс модуля парсера
    /// </summary>
    public interface IParsingModule
    {
        /// <summary>
        /// Получить информацию о разработчике
        /// </summary>
        /// <returns>Информация о разработчике</returns>
        AboutResponse About();

        /// <summary>
        /// Получить названия ресурсов, обрабатываемая библиотекой
        /// </summary>
        /// <param name="bind">Бинд запроса</param>
        /// <returns> Коллекция названий ресурсов (сайтов)</returns>
        IList<string> Sources(Bind bind);

        /// <summary>
        /// Получить список биндов, обрабатываемая библиотекой
        /// </summary>
        /// <returns>Коллекция биндов</returns>
        IList<Bind> Keys();

        /// <summary>
        /// Задача на парсинг
        /// </summary>
        /// <param name="request">Запрос на парсинг</param>
        /// <returns>Ответ от парсера (awaitable Task!)</returns>
        Task<ParseResponse> Result(ParseRequest request);
    }
}