using System.Collections.Generic;

namespace RealtyParser.Collections
{
    public class Mapping : Dictionary<object, object>, IValueable
    {
        public Values ToValues()
        {
            return new Values(this);
        }
    }
}