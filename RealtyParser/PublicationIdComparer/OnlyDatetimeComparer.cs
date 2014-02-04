using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealtyParser.PublicationIdComparer
{
    public class OnlyDatetimeComparer : IComparer<string>
    {
        public const string DateTimePatten = @"\#\#(?<date>[^\#]+)\#\#";

        public int Compare(string x, string y)
        {
            MatchCollection matchesX = Regex.Matches(x, DateTimePatten);
            MatchCollection matchesY = Regex.Matches(y, DateTimePatten);
            DateTime dateTimeX = (matchesX.Count > 0)
                ? RealtyParserUtils.DateTimeParse(matchesX[0].Groups["date"].Value)
                : DateTime.Now;
            DateTime dateTimeY = (matchesY.Count > 0)
                ? RealtyParserUtils.DateTimeParse(matchesY[0].Groups["date"].Value)
                : DateTime.Now;
            return dateTimeX.CompareTo(dateTimeY);
        }
    }
}