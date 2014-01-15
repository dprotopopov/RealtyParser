using System.Collections.Generic;
using System.ComponentModel;

namespace RealtyParser
{
    public class HierarhialItemCollection : Dictionary<string,HierarhialItem>
    {
        public void Add(string key, string value, string parentId, int level)
        {
            if(!ContainsKey(key)) Add(key,new HierarhialItem {Key = key, Value = value, ParentId = parentId, Level = level});
        }
    }
}