using System;
using System.Collections.Generic;

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
                return Parse(string.Join("", String.ParseAsList(list)));
            }
            catch (Exception exception)
            {
                return Default;
            }
        }
    }
}