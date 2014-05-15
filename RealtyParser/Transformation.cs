using System.Collections.Generic;

namespace RealtyParser
{
    public class Transformation : MyLibrary.Transformation, IValueable
    {
        private const string KeyKey = @"Key";
        private const string ValueKey = @"Value";

        public Values ToValues()
        {
            return new Values(this);
        }

        public IEnumerable<string> ParseTemplate(Values values)
        {
            return ParseTemplate(
                string.Format(@"{{{{{0}}}}}:{{{{{1}}}}}", KeyKey, ValueKey), values);
        }
    }
}