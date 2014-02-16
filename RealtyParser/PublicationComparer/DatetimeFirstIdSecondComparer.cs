using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealtyParser.PublicationComparer
{
    public class DatetimeFirstIdSecondComparer : IComparer<string>
    {
        private const string StructurePatten = @"\[\s*(?<id>\d+)\s*\,\s*\#\#(?<date>[^\#]+)\#\#\s*\]";

        public int Compare(string x, string y)
        {
            MatchCollection structureX = System.Text.RegularExpressions.Regex.Matches(x, StructurePatten);
            MatchCollection structureY = System.Text.RegularExpressions.Regex.Matches(y, StructurePatten);


            System.DateTime dateTimeX = (structureX.Count > 0)
                ? DateTime.Parse(structureX[0].Groups["date"].Value)
                : System.DateTime.Now;
            System.DateTime dateTimeY = (structureY.Count > 0)
                ? DateTime.Parse(structureY[0].Groups["date"].Value)
                : System.DateTime.Now;

            int i = dateTimeX.CompareTo(dateTimeY);
            if (i != 0) return i;

            string idX = (structureX.Count > 0)
                ? structureX[0].Groups["id"].Value.Trim()
                : "";
            if (string.IsNullOrEmpty(idX)) idX = "0";
            string idY = (structureY.Count > 0)
                ? structureY[0].Groups["id"].Value.Trim()
                : "";
            if (string.IsNullOrEmpty(idY)) idY = "0";

            return Convert.ToDecimal(idX).CompareTo(Convert.ToDecimal(idY));
        }
    }
}