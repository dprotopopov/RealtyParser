using System.Collections.Generic;
using System.Linq;

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
                {Regex.Escape(@"{{Key}}"), Keys.ToList()},
                {Regex.Escape(@"{{Value}}"), Values.ToList()}
            };
            return string.Join("\n", new Transformation().ParseTemplate(@"{{Key}}:{{Value}}", values).ToArray());
        }
    }
}