using System;
using System.Collections.Generic;
using System.Linq;
using String = RealtyParser.Types.String;

namespace RealtyParser.Collections
{
    public class Properties : Dictionary<string, object>
    {
        public override string ToString()
        {
            var values = new Values
            {
                {Transformation.KeyKey, Keys.ToList()},
                {
                    Transformation.ValueKey, Values.Select(item => (item != null) ? item.ToString() : "(null)").ToList()
                }
            };
            return String.Parse(new Transformation().ParseTemplate(
                    string.Format(@"{{{{{0}}}}}:{{{{{1}}}}}", Transformation.KeyKey, Transformation.ValueKey), values));
        }
    }
}