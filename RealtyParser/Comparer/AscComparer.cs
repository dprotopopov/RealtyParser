using System.Collections;
using System.Collections.Generic;

namespace RealtyParser.Comparer
{
    public class AscComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return ((new CaseInsensitiveComparer()).Compare(x, y));
        }
    }
}