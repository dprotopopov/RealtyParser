﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RealtyParser
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in this)
            {
                foreach (string value in item.Value)
                {
                    sb.AppendFormat("{0}\t:\t{1}\n", item.Key, value);
                }
            }
            return sb.ToString();
        }

        public ListViewItem ToListViewItem(string name, List<string> fieldNames)
        {
            var viewItem = new ListViewItem(name);
            Debug.Assert(fieldNames != null, "fieldNames != null");
            foreach (string fieldName in fieldNames)
            {
                viewItem.SubItems.Add(this[fieldName].Aggregate((i, j) => i + "\t" + j));
            }
            return viewItem;
        }
    }
}