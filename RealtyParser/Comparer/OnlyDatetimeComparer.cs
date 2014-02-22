using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealtyParser.Comparer
{
    public class OnlyDatetimeComparer : IComparer<string>
    {
        public const string DateTimePatten = @"\#\#(?<date>[^\#]+)\#\#";

        public int Compare(string x, string y)
        {
            MatchCollection matchesX = Regex.Matches(x, DateTimePatten);
            MatchCollection matchesY = Regex.Matches(y, DateTimePatten);
            DateTime dateTimeX;
            DateTime dateTimeY;
            try
            {
                dateTimeX = Types.DateTime.Parse(matchesX[0].Groups["date"].Value.Trim());
            }
            catch (Exception exception)
            {
                dateTimeX = Types.DateTime.Default;
            }
            try
            {
                dateTimeY = Types.DateTime.Parse(matchesY[0].Groups["date"].Value.Trim());
            }
            catch (Exception exception)
            {
                dateTimeY = Types.DateTime.Default;
            }
            return dateTimeX.CompareTo(dateTimeY);
        }
    }
}