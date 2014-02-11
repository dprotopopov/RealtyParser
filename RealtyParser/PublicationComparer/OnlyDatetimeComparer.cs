using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealtyParser.PublicationComparer
{
    public class OnlyDatetimeComparer : IComparer<string>
    {
        public const string DateTimePatten = @"\#\#(?<date>[^\#]+)\#\#";

        public int Compare(string x, string y)
        {
            MatchCollection matchesX = System.Text.RegularExpressions.Regex.Matches(x, DateTimePatten);
            MatchCollection matchesY = System.Text.RegularExpressions.Regex.Matches(y, DateTimePatten);
            System.DateTime dateTimeX = (matchesX.Count > 0)
                ? DateTime.Parse(matchesX[0].Groups["date"].Value)
                : System.DateTime.Now;
            System.DateTime dateTimeY = (matchesY.Count > 0)
                ? DateTime.Parse(matchesY[0].Groups["date"].Value)
                : System.DateTime.Now;
            return dateTimeX.CompareTo(dateTimeY);
        }
    }
}