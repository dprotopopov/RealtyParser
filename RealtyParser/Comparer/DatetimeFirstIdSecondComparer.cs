using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealtyParser.Comparer
{
    public class DatetimeFirstIdSecondComparer : IComparer<string>
    {
        private const string StructurePatten = @"\[\s*(?<id>\d+)\s*\,\s*\#\#(?<date>[^\#]+)\#\#\s*\]";

        public int Compare(string x, string y)
        {
            MatchCollection structureX = Regex.Matches(x, StructurePatten);
            MatchCollection structureY = Regex.Matches(y, StructurePatten);

            DateTime dateTimeX;
            DateTime dateTimeY;
            try
            {
                dateTimeX = Types.DateTime.Parse(structureX[0].Groups["date"].Value.Trim());
            }
            catch (Exception exception)
            {
                dateTimeX = Types.DateTime.Default;
            }
            try
            {
                dateTimeY = Types.DateTime.Parse(structureY[0].Groups["date"].Value.Trim());
            }
            catch (Exception exception)
            {
                dateTimeY = Types.DateTime.Default;
            }

            int i = dateTimeX.CompareTo(dateTimeY);
            if (i != 0) return i;

            Decimal idX;
            Decimal idY;
            try
            {
                idX = Types.Decimal.Parse(structureX[0].Groups["id"].Value.Trim());
            }
            catch (Exception exception)
            {
                idX = Types.Decimal.Default;
            }
            try
            {
                idY = Types.Decimal.Parse(structureY[0].Groups["id"].Value.Trim());
            }
            catch (Exception exception)
            {
                idY = Types.Decimal.Default;
            }

            return idX.CompareTo(idY);
        }
    }
}