using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealtyParser.PublicationIdComparer
{
    public class DatetimeFirstIdSecondComparer : IComparer<string>
    {
        public const string StructurePatten = @"\[\s*(?<id>\d+)\s*\,\s*\#\#(?<date>[^\#]+)\#\#\s*\]";

        public int Compare(string x, string y)
        {
            MatchCollection structureX = Regex.Matches(x, StructurePatten);
            MatchCollection structureY = Regex.Matches(y, StructurePatten);


            DateTime dateTimeX = (structureX.Count > 0)
                ? RealtyParserUtils.DateTimeParse(structureX[0].Groups["date"].Value)
                : DateTime.Now;
            DateTime dateTimeY = (structureY.Count > 0)
                ? RealtyParserUtils.DateTimeParse(structureY[0].Groups["date"].Value)
                : DateTime.Now;

            int i = dateTimeX.CompareTo(dateTimeY);
            if (i != 0) return i;

            string idX = (structureX.Count > 0)
                ? structureX[0].Groups["id"].Value.Trim()
                : "";
            if (string.IsNullOrEmpty(idX)) idX = "0";
            string idY = (structureX.Count > 0)
                ? structureX[0].Groups["id"].Value.Trim()
                : "";
            if (string.IsNullOrEmpty(idY)) idY = "0";

            return Convert.ToDecimal(idX).CompareTo(Convert.ToDecimal(idY));
        }
    }
}