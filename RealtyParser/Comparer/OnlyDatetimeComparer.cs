using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ServiceStack;

namespace RealtyParser.Comparer
{
    public class OnlyDatetimeComparer : IPublicationComparer
    {
        public const string DateTimePatten = @"\#\#(?<date>[^\#]+)\#\#";

        public int Compare(string x, string y)
        {
            var matches = new List<Match>
            {
                Regex.Match(x, DateTimePatten),
                Regex.Match(y, DateTimePatten)
            };
            var values = new List<DateTime>();
            foreach (Match match in matches)
                try
                {
                    values.Add(Types.DateTime.Parse(match.Groups["date"].Value.Trim()));
                }
                catch (Exception exception)
                {
                    values.Add(Types.DateTime.Default);
                }
            return values.First().CompareTo(values.Last());
        }

        public bool IsValid(string s)
        {
            Match match = Regex.Match(s, DateTimePatten);
            return match.Length > 0 && !match.Groups["date"].Value.Trim().IsNullOrEmpty();
        }

        public bool Equals(string x, string y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode(string obj)
        {
            Match match = Regex.Match(obj, DateTimePatten);
            DateTime dateTime;
            try
            {
                dateTime = Types.DateTime.Parse(match.Groups["date"].Value.Trim());
            }
            catch (Exception exception)
            {
                dateTime = Types.DateTime.Default;
            }
            return dateTime.GetHashCode();
        }
    }
}