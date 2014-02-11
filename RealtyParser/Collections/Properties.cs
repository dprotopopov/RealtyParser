using System.Collections.Generic;
using System.Linq;

namespace RealtyParser.Collections
{
    public class Properties : Dictionary<string, object>
    {
        public override string ToString()
        {
            var values = new Values
            {
                {Regex.Escape(@"{{Key}}"), Keys.ToList()},
                {Regex.Escape(@"{{Value}}"), Values.Select(item => (item != null) ? item.ToString() : "(null)").ToList()}
            };
            return System.String.Join("\n", RealtyParserParsingModule.Parser.ParseTemplate(@"{{Key}}:{{Value}}", values).ToArray());
        }
    }
}