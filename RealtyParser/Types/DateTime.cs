using System;
using System.Collections.Generic;
using System.Linq;

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
                return Parse(String.ParseAsList(list).Aggregate((i, j) => string.Format("{0}{1}", i, j)));
            }
            catch (Exception exception)
            {
                return Default;
            }
        }
    }
}