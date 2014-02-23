using System.Collections;

namespace RealtyParser.Comparer
{
    public class DescComparer : IPublicationComparer
    {
        public int Compare(string x, string y)
        {
            return ((new CaseInsensitiveComparer()).Compare(y, x));
        }

        public bool IsValid(string s)
        {
            return true;
        }

        public bool Equals(string x, string y)
        {
            return x.ToLower().Equals(y.ToLower());
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}