using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using RealtyParser.Comparer;
using Boolean = MyLibrary.Types.Boolean;
using String = MyLibrary.Types.String;

namespace RealtyParser.Collections
{
    public class Properties : Dictionary<string, object>, IValueable
    {
        private const string StructurePatten = @"(\[)?\s*(?<key>\w+)\s*\:\s*(?<value>[^\]]*)\s*(\])?";
        private static readonly ObjectComparer ObjectComparer = new ObjectComparer();

        public Properties()
        {
        }

        public Properties(string str)
        {
            MethodInfo methodInfo = GetType().GetMethod("Add", new[] {typeof (string), typeof (object)});

            List<object> invoke = (from s in str.Split(',')
                select s.Trim()
                into s1
                where !string.IsNullOrEmpty(s1)
                select Regex.Match(s1, StructurePatten)
                into match
                where !string.IsNullOrEmpty(match.Groups["key"].Value)
                select methodInfo.Invoke(this, new object[] {match.Groups["key"].Value, match.Groups["value"].Value}))
                .ToList();
        }

        public Properties(object obj, IEnumerable<string> propertyNames)
        {
            Type type = obj.GetType();
            foreach (string key in propertyNames)
                Add(key, type.GetProperty(key).GetValue(obj));
        }

        public Values ToValues()
        {
            return new Values(this);
        }

        public override string ToString()
        {
            return
                String.Parse(
                    new Transformation().ParseTemplate(new Values(Keys,
                        Values.Select(item => (item != null) ? item.ToString() : "(null)"))));
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var p = (Properties) obj;
            return Count == p.Count &&
                   p.Select(item => ContainsKey(item.Key)).Aggregate(Boolean.And) &&
                   ObjectComparer.Equals(this, obj, Keys);
        }

        public override int GetHashCode()
        {
            return ObjectComparer.GetHashCode(this, Keys);
        }
    }
}