using System.Collections;
using System.Collections.Generic;

namespace RealtyParser.PublicationComparer
{
    public class AscComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return ((new CaseInsensitiveComparer()).Compare(x, y));
        }
    }
}
