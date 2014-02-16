using System;
using System.Collections.Generic;

namespace RealtyParser
{
    public class ObjectComparer : IEqualityComparer<object>, IComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            return string.Compare(x.ToString(), y.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
        }

        public int GetHashCode(object obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }

        public int Compare(object x, object y)
        {
            return string.Compare(x.ToString(), y.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}