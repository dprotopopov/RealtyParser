using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RealtyParser.Types;
using String = System.String;

namespace RealtyParser
{
    public class Transformation
    {
        public const string NameGroup = @"name";
        public const string KeyKey = @"Key";
        public const string ValueKey = @"Value";

        public readonly string FieldPattern = String.Format("{0}(?<{1}>[^\\}}]*){2}", Regex.Escape(@"{{"),
            NameGroup,
            Regex.Escape(@"}}"));

        public ProgressCallback ProgressCallback { get; set; }
        public AppendLineCallback AppendLineCallback { get; set; }
        public CompliteCallback CompliteCallback { get; set; }

        /// <summary>
        ///     Замена в строке-шаблоне идентификаторов-параметров на их значения
        /// </summary>
        public IEnumerable<string> ParseTemplate(string template, Values values)
        {
            var list = new List<string>();
            int maxCount = values.MaxCount;
            string[] parts = System.Text.RegularExpressions.Regex.Split(template, FieldPattern);
            Debug.Assert(parts.Length%2 == 1);
            long current = 0;
            long total = maxCount;
            for (int index = 0; index < maxCount; index++)
            {
                var list1 = new List<string>();
                for (int i = 0; i < (parts.Length & ~1); i += 2)
                {
                    list1.Add(parts[i]);
                    string key = String.Format("{0}", parts[i + 1]);
                    if (values.ContainsKey(key) && values[key].Count() > index) list1.Add(values[key].ToList()[index]);
                }
                list1.Add(parts.Last());
                string value = String.Join("", list1).Trim();
                if (!String.IsNullOrEmpty(value)) list.Add(value);
                if (ProgressCallback != null) ProgressCallback(++current, total);
            }
            if (CompliteCallback != null) CompliteCallback();
            return list;
        }
    }
}