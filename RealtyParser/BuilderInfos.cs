using System.Collections.Generic;
using System.Linq;

namespace RealtyParser
{
    public class BuilderInfos : Dictionary<string, BuilderInfo>
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

        public void Add(BuilderInfo builderInfo)
        {
            Add(builderInfo.TableName.ToString(), builderInfo);
        }
    }
}