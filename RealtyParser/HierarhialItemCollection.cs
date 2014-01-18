using System.Collections.Generic;

namespace RealtyParser
{
    /// <summary>
    /// Не входит в техническое задание
    /// </summary>
    public class HierarhialItemCollection : Dictionary<string,HierarhialItem>
    {
        public void Add(string key, string value, string parentId, int level)
        {
            if(!ContainsKey(key)) Add(key,new HierarhialItem {Key = key, Value = value, ParentId = parentId, Level = level});
        }
    }
}