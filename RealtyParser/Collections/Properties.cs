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
                {
                    Regex.Escape(@"{{Value}}"), Values.Select(item => (item != null) ? item.ToString() : "(null)").ToList()
                }
            };
            return string.Join("\n", new Transformation().ParseTemplate(@"{{Key}}:{{Value}}", values));
        }
    }
}