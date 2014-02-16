using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RealtyParser
{
    public static class String
    {
        private static readonly CultureInfo CultureInfo = CultureInfo.InvariantCulture;

        private static readonly string[] AddressPatterns =
        {
            @"\bг\.\s", @"\bпгт\.\s", @"\bп\.\s", @"\bс\.\s", @"\bдер\.\s", @"\bх\.\s", @"\bст\-ца\s", @"\bаул\s",
            @"\bг\b", @"\bп\b", @"\bс\b", @"\bдер\b", @"\bх\b", @"\bпгт\b", @"\bстца\b", @"\bаул\b",
            @"г\.", @"пгт", @"п\.", @"с\.", @"дер\.", @"х\.", @"ст\-ца", @"аул",
            @"\.", @"\-"
        };

        public static string IntroText(string text, int introLength = 120)
        {
            if (text.Length < introLength || introLength <= 0) return text;
            return text.Substring(0, introLength) + " ...";
        }

        /// <summary>
        ///     Конвертация списка строк в список Uri
        /// </summary>
        public static IList<Uri> ConvertToUri(IEnumerable<string> strings)
        {
            return strings.Select(s => new Uri(s)).ToList();
        }

        public static string ToTitleCase(string str)
        {
            string[] parts = System.Text.RegularExpressions.Regex.Split(str, @"(\w+)");
            return string.Join("", parts.Select(part => CultureInfo.TextInfo.ToTitleCase(part)));
        }

        public static string NormalizeAddress(string str)
        {
            str = AddressPatterns.Aggregate(str.ToLower(),
                (current, pattern) =>
                    new System.Text.RegularExpressions.Regex(pattern, RegexOptions.IgnoreCase).Replace(current, @" "));
            str = new System.Text.RegularExpressions.Regex(@"\s+").Replace(str, @" ");
            return str.Trim();
        }
    }
}