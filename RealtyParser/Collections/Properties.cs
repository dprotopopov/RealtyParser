using System.Collections.Generic;

namespace RealtyParser.Collections
{
    public class Properties : MyLibrary.Collections.Properties, IValueable
    {
        public Properties()
        {
        }

        public Properties(string str) : base(str)
        {
        }

        public Properties(object obj, IEnumerable<string> propertyNames) : base(obj, propertyNames)
        {
        }

        public new Values ToValues()
        {
            return new Values(this);
        }
    }
}