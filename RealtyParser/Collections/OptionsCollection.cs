using System.Collections.Generic;
using System.Linq;
using RealtyParser.Types;

namespace RealtyParser.Collections
{
    /// <summary>
    ///     Не входит в техническое задание
    /// </summary>
    public class OptionsCollection : Dictionary<string, string>
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