using System.Collections;
using System.Collections.Generic;

namespace RealtyParser.PublicationIdComparer
{
    public class DescComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return ((new CaseInsensitiveComparer()).Compare(y, x));
        }
    }
}
