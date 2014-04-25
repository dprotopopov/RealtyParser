using System.Collections.Generic;
using System.Linq;

namespace RealtyParser.Collections
{
    public class StackListQueue<T> : MyLibrary.Collections.StackListQueue<T>, IValueable
    {
        public new Values ToValues()
        {
            return new Values(this);
        }

        #region

        public StackListQueue(IEnumerable<T> value) : base(value)
        {
        }

        public StackListQueue(T value)
            : base(value)
        {
        }

        public StackListQueue()
        {
        }

        #endregion

        public override bool Equals(object obj)
        {
            var collection = obj as SortedStackListQueue<T>;
            return collection != null && this.SequenceEqual(collection);
        }

        public override int GetHashCode()
        {
            return this.Aggregate(0,
                (current, item) => (current << 1) ^ (current >> (8*sizeof (int) - 1)) ^ item.GetHashCode());
        }

        public override string ToString()
        {
            return string.Join(",", this.Select(item => item.ToString()));
        }
    }
}