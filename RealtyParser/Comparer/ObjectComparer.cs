using System;
using System.Collections.Generic;
using System.Linq;
using Boolean = RealtyParser.Types.Boolean;

namespace RealtyParser.Comparer
{
    public class ObjectComparer : IEqualityComparer<object>, IComparer<object>
    {
        public int Compare(object x, object y)
        {
            return string.Compare(x.ToString(), y.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        public new bool Equals(object x, object y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode(object obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }

        public int GetHashCode(object obj, IEnumerable<string> propertyNames)
        {
            Type type = obj.GetType();
            return propertyNames.Aggregate(type.GetHashCode(),
                (current, name) => current ^ GetHashCode(type.GetProperty(name).GetValue(obj)));
        }

        public bool Equals(object x, object y, IEnumerable<string> propertyNames)
        {
            Type type = x.GetType();
            Type type1 = y.GetType();
            return propertyNames.Select(name =>
                Equals(type.GetProperty(name).GetValue(x),
                    type1.GetProperty(name).GetValue(y)))
                .Aggregate(Boolean.And);
        }

        public int Compare(object x, object y, IEnumerable<string> propertyNames)
        {
            Type type = x.GetType();
            Type type1 = y.GetType();
            return
                propertyNames.Select(
                    name => Compare(type.GetProperty(name).GetValue(x), type1.GetProperty(name).GetValue(y)))
                    .FirstOrDefault(value => value != 0);
        }
    }
}