using System;
using System.Collections.Generic;

namespace RealtyParser.PublicationComparer
{
    internal class OnlyDigitsDecimalComparer : IComparer<string>
    {
        private const string NonDigitPattern = @"\D+";

        public int Compare(string x, string y)
        {
            var regex = new System.Text.RegularExpressions.Regex(NonDigitPattern);
            string valueX = regex.Replace(x, @"").Trim();
            if (string.IsNullOrEmpty(valueX)) valueX = "0";
            string valueY = regex.Replace(y, @"").Trim();
            if (string.IsNullOrEmpty(valueY)) valueY = "0";
            return Convert.ToDecimal(valueX).CompareTo(Convert.ToDecimal(valueY));
        }
    }
}