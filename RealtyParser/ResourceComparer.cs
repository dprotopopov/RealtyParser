using System.Collections.Generic;
using MyParser.Comparer;

namespace RealtyParser
{
    public class ResourceComparer : IEqualityComparer<Resource>, IComparer<Resource>
    {
        private static readonly ObjectComparer ObjectComparer = new ObjectComparer();
        public IPublicationComparer PublicationComparer { get; set; }
        public IEnumerable<string> Mapping { get; set; }

        public int Compare(Resource x, Resource y)
        {
            int value = ObjectComparer.Compare(x, y, Mapping);
            if (value != 0) return value;
            value = PublicationComparer.Compare(x.ToString(), y.ToString());
            return value != 0 ? value : string.CompareOrdinal(x.ToString(), y.ToString());
        }

        public bool Equals(Resource x, Resource y)
        {
            return Compare(x,y)==0;
        }

        public int GetHashCode(Resource obj)
        {
            return ToString().GetHashCode();
        }
    }
}