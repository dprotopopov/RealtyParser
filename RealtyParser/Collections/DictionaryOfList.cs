﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using String = RealtyParser.Types.String;

namespace RealtyParser.Collections
{
    public class DictionaryOfList : Dictionary<string, IEnumerable<string>>, IValueable
    {
        protected DictionaryOfList(IEnumerable<KeyValuePair<string, IEnumerable<string>>> list)
        {
            AddOrReplace(list);
        }

        protected DictionaryOfList()
        {
        }

        protected DictionaryOfList(Object obj)
        {
            AddOrReplace(obj.GetType().GetProperties()
                .Select(propertyInfo => new {key = propertyInfo, value = propertyInfo.GetValue(obj, null)})
                .Where(@t => @t.value != null)
                .Select(
                    @t =>
                        new KeyValuePair<string, IEnumerable<string>>(@t.key.ToString(),
                            new StackListQueue<string> {@t.value.ToString()})));
        }

        public int MaxCount
        {
            get
            {
                return (from item in this where item.Value != null select item.Value.Count()).Concat(new[] {0}).Max();
            }
        }

        public Values ToValues()
        {
            return new Values(this);
        }

        public new void Add(string key, IEnumerable<string> list)
        {
            if (!ContainsKey(key)) base.Add(key, list);
            else
            {
                List<string> list1 = this[key].ToList();
                list1.AddRange(list);
                this[key] = list1;
            }
        }

        public void Add(IEnumerable<KeyValuePair<string, IEnumerable<string>>> list)
        {
            foreach (var pair in list)
                if (!ContainsKey(pair.Key)) Add(pair.Key, pair.Value);
                else
                {
                    List<string> list1 = this[pair.Key].ToList();
                    list1.AddRange(pair.Value);
                    this[pair.Key] = list1;
                }
        }

        public void Add(string key, string value)
        {
            if (!ContainsKey(key)) Add(key, new StackListQueue<string> {value});
            else
            {
                List<string> list1 = this[key].ToList();
                list1.Add(value);
                this[key] = list1;
            }
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
                values.Add(new Values
                {
                    Key = Enumerable.Repeat(item.Key, item.Value.Count()),
                    Value = item.Value,
                });
            }
            return String.Parse(new Transformation().ParseTemplate(values));
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

        public void AddOrReplace(IEnumerable<KeyValuePair<string, IEnumerable<string>>> list)
        {
            foreach (var pair in list)
            {
                if (!ContainsKey(pair.Key)) Add(pair.Key, pair.Value);
                else this[pair.Key] = pair.Value;
            }
        }

        public DictionaryOfList Slice(int row)
        {
            var dictionaryOfList = new DictionaryOfList();
            foreach (var pair in this.Where(pair => row < pair.Value.Count()))
                dictionaryOfList.Add(pair.Key, pair.Value.ElementAt(row));
            return dictionaryOfList;
        }
    }
}