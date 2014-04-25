using System;
using System.Collections.Generic;
using System.Linq;
using String = MyLibrary.Types.String;

namespace RealtyParser.Collections
{
    public class DictionaryOfListOfString : MyLibrary.Collections.DictionaryOfListOfString, IValueable
    {
        protected DictionaryOfListOfString(IEnumerable<KeyValuePair<string, IEnumerable<string>>> list) : base(list)
        {
        }

        protected DictionaryOfListOfString()
        {
        }

        protected DictionaryOfListOfString(Object obj) : base(obj)
        {
        }

        public new Values ToValues()
        {
            return new Values(this);
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


        public new DictionaryOfListOfString Slice(int row)
        {
            return new DictionaryOfListOfString(base.Slice(row));
        }
    }
}