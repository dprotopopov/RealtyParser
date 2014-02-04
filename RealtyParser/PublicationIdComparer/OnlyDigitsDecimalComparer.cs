using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealtyParser.PublicationIdComparer
{
    internal class OnlyDigitsDecimalComparer : IComparer<string>
    {
        public const string NonDigitPattern = @"\D+";

        public int Compare(string x, string y)
        {
            var regex = new Regex(NonDigitPattern);
            string valueX = regex.Replace(x, @"").Trim();
            if (string.IsNullOrEmpty(valueX)) valueX = "0";
            string valueY = regex.Replace(y, @"").Trim();
            if (string.IsNullOrEmpty(valueY)) valueY = "0";
            return Convert.ToDecimal(valueX).CompareTo(Convert.ToDecimal(valueY));
        }
    }
}