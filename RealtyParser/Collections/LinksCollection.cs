using System.Collections.Generic;
using System.Linq;

namespace RealtyParser.Collections
{
    /// <summary>
    ///     Вспомогательный класс
    ///     Используется для доступа к значениям словаря по ключу
    /// </summary>
    public class LinksCollection : Dictionary<string, string>
    {
        public override string ToString()
        {
            var values = new Values
            {
                {Regex.Escape(@"{{Key}}"), Keys.ToList()},
                {Regex.Escape(@"{{Value}}"), Values.ToList()}
            };
            return string.Join("\n", new Transformation().ParseTemplate(@"{{Key}}:{{Value}}", values).ToArray());
        }
    }
}