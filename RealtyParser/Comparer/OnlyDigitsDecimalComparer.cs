using System;
using System.Collections.Generic;

namespace RealtyParser.Comparer
{
    internal class OnlyDigitsDecimalComparer : IComparer<string>
    {
        private const string NonDigitPattern = @"\D+";

        private readonly System.Text.RegularExpressions.Regex _regex =
            new System.Text.RegularExpressions.Regex(NonDigitPattern);

        public int Compare(string x, string y)
        {
            Decimal idX;
            Decimal idY;
            try
            {
                idX = Types.Decimal.Parse(_regex.Replace(x, @"").Trim());
            }
            catch (Exception exception)
            {
                idX = Types.Decimal.Default;
            }
            try
            {
                idY = Types.Decimal.Parse(_regex.Replace(y, @"").Trim());
            }
            catch (Exception exception)
            {
                idY = Types.Decimal.Default;
            }

            return idX.CompareTo(idY);
        }
    }
}