using System.Linq;
using System.Reflection;
using System.Text;

namespace RT.ParsingLibs.Models
{
    /// <summary>
    /// Дополнительная информация специфичная для каждой рубрики
    /// </summary>
    public class AdditionalInfo
    {
        /// <summary>
        /// Дополнительная информация для рубрики "Недвижимость"
        /// </summary>
        public RealtyAdditionalInfo RealtyAdditionalInfo { get; set; }

        /// <summary>
        /// Преобразовать в строку
        /// </summary>
        /// <returns>Объект в виде строки</returns>
        public override string ToString()
        {
            var properties = GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(this, null));
            var stringBuilder = new StringBuilder();
            foreach (var property in properties)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("{0} = {1}", property.Key, property.Value);
            }
            return stringBuilder.ToString();
        }
    }
}