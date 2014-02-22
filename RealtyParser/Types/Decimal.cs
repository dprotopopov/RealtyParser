using System;
using System.Collections.Generic;
using System.Linq;

namespace RealtyParser.Types
{
    public static class Decimal
    {
        public const System.Decimal Default = 0m;

        public static System.Decimal Parse(string s)
        {
            return string.IsNullOrEmpty(s) ? Default : Convert.ToDecimal(s);
        }

        public static System.Decimal Parse(IEnumerable<string> list)
        {
            try
            {
                return Parse(String.ParseAsList(list).Aggregate((i, j) => string.Format("{0}{1}", i, j)));
            }
            catch (Exception exception)
            {
                return Default;
            }
        }
    }
}