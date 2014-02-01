using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealtyParser.PublicationIdComparer
{
    class OnlyDigitsDecimalComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            Regex regex = new Regex(@"\D+");
            x = regex.Replace(x, @"").Trim();
            if (string.IsNullOrEmpty(x)) x = "0";
            y = regex.Replace(y, @"").Trim();
            if (string.IsNullOrEmpty(y)) y = "0";
            return (int)(Convert.ToDecimal(x) - Convert.ToDecimal(y));
        }
    }
}
