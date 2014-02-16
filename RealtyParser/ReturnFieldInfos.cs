using System.Collections.Generic;
using System.Linq;

namespace RealtyParser
{
    public class ReturnFieldInfos : Dictionary<string, ReturnFieldInfo>
    {
        public override string ToString()
        {
            var values = new Values
            {
                {Regex.Escape(@"{{Key}}"), Keys.ToList()},
                {Regex.Escape(@"{{Value}}"), this.Select(item => item.ToString()).ToList()},
            };
            return string.Join("\n", new Transformation().ParseTemplate(@"{{Key}}:{{Value}}", values));
        }

        public void Add(ReturnFieldInfo returnFieldInfo)
        {
            Add(returnFieldInfo.ReturnFieldId.ToString(), returnFieldInfo);
        }
    }
}