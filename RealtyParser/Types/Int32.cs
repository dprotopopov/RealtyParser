using System;
using System.Collections.Generic;
using System.Linq;

namespace RealtyParser.Types
{
    public static class Int32
    {
        public const int Default = 0;

        public static int Parse(string s)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch (Exception exception)
            {
                return Default;
            }
        }

        public static int Parse(IEnumerable<string> list)
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