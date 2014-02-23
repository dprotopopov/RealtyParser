using System.Collections.Generic;
using System.Linq;
using RealtyParser.Types;

namespace RealtyParser
{
    public class BuilderInfos : Dictionary<string, BuilderInfo>
    {
        public override string ToString()
        {
            var values = new Values
            {
                Key = Keys.ToList(),
                Value = this.Select(item => item.ToString()).ToList(),
            };
            return String.Parse(new Transformation().ParseTemplate(values));
        }

        public void Add(BuilderInfo builderInfo)
        {
            Add(builderInfo.TableName.ToString(), builderInfo);
        }
    }
}