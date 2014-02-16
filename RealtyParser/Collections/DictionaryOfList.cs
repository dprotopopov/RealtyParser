using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RealtyParser.Collections
{
    public class DictionaryOfList : Dictionary<string, List<string>>
    {
        public int MaxCount
        {
            get { return (from item in this where item.Value != null select item.Value.Count).Concat(new[] {0}).Max(); }
        }

        public void Add(string key, string value)
        {
            if (!ContainsKey(key)) Add(key, new List<string>());
            this[key].Add(value);
        }

        public void Add(IList<string> keys, IList<string> values)
        {
            if (keys == null) throw new ArgumentNullException("keys");
            if (values == null) throw new ArgumentNullException("values");
            Debug.Assert(keys.Count() == values.Count());
            for (int i = 0; i < keys.Count() && i < values.Count(); i++) Add(keys[i], values[i]);
        }

        public override string ToString()
        {
            var values = new Values();
            foreach (var item in this)
            {
                values.InsertOrAppend(new Values
                {
                    {Regex.Escape(@"{{Key}}"), Enumerable.Repeat(item.Key, item.Value.Count).ToList()},
                    {Regex.Escape(@"{{Value}}"), item.Value.ToList()},
                });
            }
            return string.Join("\n", new Transformation().ParseTemplate(@"{{Key}}:{{Value}}", values).ToArray());
        }

        public ListViewItem ToListViewItem(string name, IEnumerable<string> fieldNames)
        {
            var viewItem = new ListViewItem(name);
            Debug.Assert(fieldNames != null, "fieldNames != null");
            foreach (string fieldName in fieldNames)
            {
                viewItem.SubItems.Add(this[fieldName].Aggregate((i, j) => string.Format("{0}\t{1}", i, j)));
            }
            return viewItem;
        }
    }
}