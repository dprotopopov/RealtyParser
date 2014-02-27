using System;
using System.Collections.Generic;

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
                return Parse(string.Join("", String.ParseAsList(list)));
            }
            catch (Exception exception)
            {
                return Default;
            }
        }
    }
}