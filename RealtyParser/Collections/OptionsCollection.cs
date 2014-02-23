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
            return String.Parse(new Transformation().ParseTemplate(new Values(Keys, Values)));
        }
    }
}