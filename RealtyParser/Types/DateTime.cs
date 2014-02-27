using System;
using System.Collections.Generic;

namespace RealtyParser.Types
{
    public static class DateTime
    {
        public static System.DateTime Default = System.DateTime.Now;

        public static System.DateTime Parse(string s)
        {
            return string.IsNullOrEmpty(s) ? Default : System.DateTime.Parse(s);
        }

        public static System.DateTime Parse(IEnumerable<string> list)
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