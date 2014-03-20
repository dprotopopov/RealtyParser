using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RealtyParser.Types
{
    public static class String
    {
        public const System.String Default = @"";
        private static readonly CultureInfo CultureInfo = CultureInfo.InvariantCulture;


        public static string IntroText(string text, int introLength = 120)
        {
            if (text.Length < introLength || introLength <= 0) return text;
            return text.Substring(0, introLength) + " ...";
        }

        public static string ToTitleCase(string str)
        {
            string[] parts = System.Text.RegularExpressions.Regex.Split(str, @"(\w+)");
            return string.Join("", parts.Select(part => CultureInfo.TextInfo.ToTitleCase(part)));
        }

        public static string Parse(string s)
        {
            return s.Trim();
        }

        public static string Parse(IEnumerable<string> list)
        {
            try
            {
                return Parse(string.Join(Environment.NewLine, ParseAsList(list)));
            }
            catch (Exception exception)
            {
                return Default;
            }
        }

        public static IList<string> ParseAsList(IEnumerable<string> list)
        {
            return list.Select(Parse).Where(s => !string.IsNullOrEmpty(s)).ToList();
        }

        #region

        private static readonly Dictionary<string, string> AddressReplacements = new Dictionary<string, string>
        {
            {@"ё", @"е"},
            {@"\bг\.(.*)", @"$1"},
            {@"\bпгт\s(.*)", @"$1"},
            {@"\bп\.(.*)", @"$1 (поселок)"},
            {@"\bдер\.(.*)", @"$1 (деревня)"},
            {@"\bх\.(.*)", @"$1 (хутор)"},
            {@"\bс\.(.*)", @"$1 (село)"},
            {@"\bст\-ца\s(.*)", @"$1 (станица)"},
            {@"\bаул\s(.*)", @"$1 (аул)"},
            {@"\bст\.(.*)", @"$1 (станция)"},
            {@"\bсвх\s(.*)", @"$1"},
            {@"\b(республика|автономная область|область|край)\s(.*)", @"$2 $1"},
        };

        public static string NormalizeAddress(string str)
        {
            str = AddressReplacements.Aggregate(str.ToLower(),
                (current, pair) =>
                    new System.Text.RegularExpressions.Regex(pair.Key, RegexOptions.IgnoreCase).Replace(current,
                        pair.Value));
            str = new System.Text.RegularExpressions.Regex(@"\s+").Replace(str, @" ");
            return str.Trim();
        }

        #endregion
    }
}