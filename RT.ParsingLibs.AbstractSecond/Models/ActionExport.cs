
using ServiceStack.DataAnnotations;

namespace RT.ParsingLibs.AbstractSecond.Models
{
    /// <summary>
    /// Действие
    /// </summary>
    [Alias("Action")]
    public class ActionExport 
    {
        /// <summary>
        /// ИД действия
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Противоположное действие
        /// </summary>
        public int AntiActionId { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        [Required]
        public string Title { get; set; }
    }
}
