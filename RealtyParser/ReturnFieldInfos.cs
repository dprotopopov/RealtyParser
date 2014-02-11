using System.Collections.Generic;
using System.Linq;

namespace RealtyParser
{
    public class ReturnFieldInfos : List<ReturnFieldInfo>
    {
        public override string ToString()
        {
            return System.String.Join("\n", this.Select(item => item.ToString()).ToArray());
        }
    }
}