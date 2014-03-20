using System.Collections.Generic;

namespace RealtyParser.Collections
{
    public class Mappings : Dictionary<string, Mapping>, IValueable
    {
        public Values ToValues()
        {
            return new Values(this);
        }
    }
}