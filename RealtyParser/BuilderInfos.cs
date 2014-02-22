using System;
using System.Collections.Generic;
using System.Linq;
using String = RealtyParser.Types.String;

namespace RealtyParser
{
    public class BuilderInfos : Dictionary<string, BuilderInfo>
    {
        public override string ToString()
        {
            var values = new Values
            {
                {Transformation.KeyKey, Keys.ToList()},
                {Transformation.ValueKey, this.Select(item => item.ToString()).ToList()},
            };
            return String.Parse(new Transformation().ParseTemplate(
                    string.Format(@"{{{{{0}}}}}:{{{{{1}}}}}", Transformation.KeyKey, Transformation.ValueKey), values));
        }

        public void Add(BuilderInfo builderInfo)
        {
            Add(builderInfo.TableName.ToString(), builderInfo);
        }
    }
}