using System.Collections.Generic;

namespace RealtyParser
{
    public class LinkComparer : IEqualityComparer<Link>, IComparer<Link>
    {
        public int Compare(Link x, Link y)
        {
            return string.CompareOrdinal(x.ToString().ToLower(), y.ToString().ToLower());
        }

        public bool Equals(Link x, Link y)
        {
            return string.CompareOrdinal(x.ToString().ToLower(), y.ToString().ToLower()) == 0;
        }

        public int GetHashCode(Link obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
}