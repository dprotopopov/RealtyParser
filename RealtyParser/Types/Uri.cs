using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RealtyParser.Types
{
    public static class Uri
    {
        public const System.Uri Default = null;

        private static readonly string Pattern =
            @"^(?<shema>([^:/?#]+)://)?(?<domain>[^/?#]*)?(?<path>[^?#]*)(?<query>\?([^#]*))?(?<part>#(.*))?";

        public static System.Uri Parse(string s)
        {
            return string.IsNullOrEmpty(s) ? Default : new System.Uri(s);
        }

        public static System.Uri Combine(string baseUrl, string url)
        {
            Match baseMatch = System.Text.RegularExpressions.Regex.Match(baseUrl, Pattern);
            Match match = System.Text.RegularExpressions.Regex.Match(url, Pattern);
            string shema = string.IsNullOrEmpty(match.Groups["shema"].Value)
                ? baseMatch.Groups["shema"].Value
                : match.Groups["shema"].Value;
            string domain = string.IsNullOrEmpty(match.Groups["domain"].Value)
                ? baseMatch.Groups["domain"].Value
                : match.Groups["domain"].Value;
            string path = string.IsNullOrEmpty(match.Groups["path"].Value)
                ? baseMatch.Groups["path"].Value
                : match.Groups["path"].Value;
            string query = string.IsNullOrEmpty(match.Groups["query"].Value)
                ? baseMatch.Groups["query"].Value
                : match.Groups["query"].Value;
            string part = string.IsNullOrEmpty(match.Groups["part"].Value)
                ? baseMatch.Groups["part"].Value
                : match.Groups["part"].Value;
            return new System.Uri(shema + domain + path + query + part);
        }

        /// <summary>
        ///     Конвертация списка строк в список Uri
        /// </summary>
        public static System.Uri Parse(IEnumerable<string> strings)
        {
            try
            {
                return Parse(string.Join("", String.ParseAsList(strings)));
            }
            catch (Exception exception)
            {
                return Default;
            }
        }

        public static IList<System.Uri> ParseAsList(IEnumerable<string> strings)
        {
            return String.ParseAsList(strings).Select(Parse).ToList();
        }
    }
}