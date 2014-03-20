using System.Collections.Generic;
using RealtyParser.Types;

namespace RealtyParser.Collections
{
    /// <summary>
    ///     Не входит в техническое задание
    /// </summary>
    public class OptionsCollection : Dictionary<string, string>, IValueable
    {
        public Values ToValues()
        {
            return new Values(this);
        }

        public override string ToString()
        {
            return String.Parse(new Transformation().ParseTemplate(new Values(Keys, Values)));
        }
    }
}