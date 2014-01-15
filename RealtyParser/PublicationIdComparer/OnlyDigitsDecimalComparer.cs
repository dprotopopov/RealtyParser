using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealtyParser.PublicationIdComparer
{
    class OnlyDigitsDecimalComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            Regex regex = new Regex(@"[^\d]");
            return (int) (Convert.ToDecimal(regex.Replace(y, @"")) - Convert.ToDecimal(regex.Replace(x, @"")));
        }
    }
}
