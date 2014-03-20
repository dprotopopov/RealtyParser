using System.Collections.Generic;

namespace RealtyParser.Collections
{
    /// <summary>
    ///     Не входит в техническое задание
    /// </summary>
    public class HierarchicalItemCollection : Dictionary<string, HierarchicalItem>, IValueable
    {
        public Values ToValues()
        {
            return new Values(this);
        }

        public void Add(string key, string value, string parentId, int level)
        {
            if (!ContainsKey(key))
                Add(key, new HierarchicalItem {Key = key, Value = value, ParentId = parentId, Level = level});
        }

        public void Add(HierarchicalItem item)
        {
            if (!ContainsKey(item.Key)) Add(item.Key, item);
        }
    }
}