using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using RealtyParser.Collections;

namespace RealtyParser
{
    public class Transformation : MyLibrary.Transformation, IValueable
    {
        private const string KeyKey = @"Key";
        private const string ValueKey = @"Value";


        public Values ToValues()
        {
            return new Values(this);
        }

        /// <summary>
        ///     Замена в строке-шаблоне идентификаторов-параметров на их значения
        /// </summary>
        public IEnumerable<string> ParseTemplate(string template, Values values)
        {
            var list = new StackListQueue<string>();
            int maxCount = values.MaxCount;
            string[] parts = Regex.Split(template, FieldPattern);
            Debug.Assert(parts.Length%2 == 1);
            int current = 0;
            int total = maxCount;
            for (int index = 0; index < maxCount; index++)
            {
                var list1 = new StackListQueue<string>();
                for (int i = 0; i < (parts.Length & ~1); i += 2)
                {
                    list1.Add(parts[i]);
                    string key = string.Format("{0}", parts[i + 1]);
                    if (values.ContainsKey(key) && values[key].Count() > index) list1.Add(values[key].ToList()[index]);
                }
                list1.Add(parts.Last());
                string value = string.Join("", list1).Trim();
                if (!string.IsNullOrEmpty(value)) list.Add(value);
                if (ProgressCallback != null) ProgressCallback(++current, total);
            }
            if (CompliteCallback != null) CompliteCallback();
            return list;
        }

        public IEnumerable<string> ParseTemplate(Values values)
        {
            return ParseTemplate(
                string.Format(@"{{{{{0}}}}}:{{{{{1}}}}}", KeyKey, ValueKey), values);
        }
    }
}