using System;
using System.Collections.Generic;
using System.Linq;

namespace RealtyParser
{
    public static class String
    {
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
    }
}