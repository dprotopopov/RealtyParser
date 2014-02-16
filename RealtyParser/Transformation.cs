using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace RealtyParser
{
    public class Transformation
    {
        public const string NameGroup = @"name";
        public const string FieldPattern = @"\{\{(?<" + NameGroup + @">[^\}]*)\}\}";

        public ProgressCallback ProgressCallback { get; set; }
        public AppendLineCallback AppendLineCallback { get; set; }
        public CompliteCallback CompliteCallback { get; set; }

        /// <summary>
        ///     Замена в строке-шаблоне идентификаторов-параметров на их значения
        /// </summary>
        public List<string> ParseTemplate(string template, Values values)
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
                    string key = Regex.Escape(string.Format("{{{{{0}}}}}", parts[i + 1]));
                    if (values.ContainsKey(key) && values[key].Count > index) list1.Add(values[key][index]);
                }
                list1.Add(parts.Last());
                string value = System.String.Join("", list1).Trim();
                if (!string.IsNullOrEmpty(value)) list.Add(value);
                if (ProgressCallback != null) ProgressCallback(++current, total);
            }
            if (CompliteCallback != null) CompliteCallback();
            return list;
        }
    }
}