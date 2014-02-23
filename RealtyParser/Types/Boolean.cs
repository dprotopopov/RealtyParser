using System;
using System.Collections.Generic;
using System.Linq;

namespace RealtyParser.Types
{
    public static class Boolean
    {
        public const System.Boolean Default = false;
        private static readonly string[] No = {"\bFalse\b", "\bF\b", "\bNo\b", "\bНет\b", "\bОтсутствует\b", "\b0\b"};
        private static readonly string[] Yes = {"\bTrue\b", "\bT\b", "\bYes\b", "\bДа\b", "\bЕсть\b", "\b1\b"};

        public static System.Boolean Parse(string s)
        {
            bool yes = Yes.Aggregate(false,
                (current, pattern) => current != System.Text.RegularExpressions.Regex.Matches(s, pattern).Count > 0);
            if (yes) return true;
            bool no = No.Aggregate(false,
                (current, pattern) => current != System.Text.RegularExpressions.Regex.Matches(s, pattern).Count > 0);
            if (no) return false;
            return Convert.ToBoolean(s);
        }

        public static System.Boolean Parse(IEnumerable<string> list)
        {
            try
            {
                return String.ParseAsList(list).Aggregate(Default,
                    (current, item) => current != Parse(item));
            }
            catch (Exception exception)
            {
                return Default;
            }
        }

        public static bool And(bool b, bool b1)
        {
            return b && b1;
        }
    }
}