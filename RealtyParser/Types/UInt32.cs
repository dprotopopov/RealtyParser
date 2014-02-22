using System;
using System.Collections.Generic;
using System.Linq;

namespace RealtyParser.Types
{
    public static class UInt32
    {
        public const uint Default = 0u;

        public static uint Parse(string s)
        {
            try
            {
                return Convert.ToUInt32(s);
            }
            catch (Exception exception)
            {
                return Default;
            }
        }

        public static uint Parse(IEnumerable<string> list)
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