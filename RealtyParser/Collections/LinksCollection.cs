using System.Collections.Generic;
using System.Linq;
using RealtyParser.Types;

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
                {Transformation.KeyKey, Keys.ToList()},
                {Transformation.ValueKey, Values.ToList()}
            };
            return String.Parse(new Transformation().ParseTemplate(
                string.Format(@"{{{{{0}}}}}:{{{{{1}}}}}", Transformation.KeyKey, Transformation.ValueKey), values));
        }
    }
}