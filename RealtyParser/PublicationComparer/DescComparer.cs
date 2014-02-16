using System.Collections;
using System.Collections.Generic;

namespace RealtyParser.PublicationComparer
{
    public class DescComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return ((new CaseInsensitiveComparer()).Compare(y, x));
        }
    }
}