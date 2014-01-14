using ServiceStack.DataAnnotations;

namespace RT.ParsingLibs.AbstractSecond.Models
{
    /// <summary>
    /// Регион
    /// </summary>
    [Alias("Region")]
    public class RegionExport 
    {
        /// <summary>
        /// ИД региона
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Заголовок региона
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// ИД родительского региона
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Уровень региона
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Наличие дочерних узлов
        /// </summary>
        public bool HasChild { get; set; }
    }
}
