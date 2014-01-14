using ServiceStack.DataAnnotations;

namespace RT.ParsingLibs.AbstractFirst.Models
{
    /// <summary>
    /// Рубрика
    /// </summary>
    [Alias("Rubric")]
    public class RubricExport
    {
        /// <summary>
        /// Ид рубрики
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Заголовок рубрики
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// ИД родительской рубрики
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Уровень рубрики
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Наличие дочерних узлов рубрики
        /// </summary>
        public bool HasChild { get; set; }
    }
}