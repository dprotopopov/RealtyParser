using System;
using System.Collections.Generic;

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
                return Parse(string.Join("", String.ParseAsList(list)));
            }
            catch (Exception exception)
            {
                return Default;
            }
        }
    }
}