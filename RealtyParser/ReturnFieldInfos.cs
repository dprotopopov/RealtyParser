using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyLibrary.Attribute;
using MyLibrary.Types;
using RealtyParser.Collections;

namespace RealtyParser
{
    public class ReturnFieldInfos : Dictionary<string, IEnumerable<ReturnFieldInfo>>, IValueable
    {
        [Value]
        public IEnumerable<ReturnFieldInfo> OptionRedirect
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        [Value]
        public IEnumerable<ReturnFieldInfo> ValueRedirect
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        [Value]
        public IEnumerable<ReturnFieldInfo> Subdomain
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        [Value]
        public IEnumerable<ReturnFieldInfo> PublicationId
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        [Value]
        public IEnumerable<ReturnFieldInfo> PublicationDatetime
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        [Value]
        public IEnumerable<ReturnFieldInfo> PublicationLink
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public Values ToValues()
        {
            return new Values(this);
        }

        public List<ReturnFieldInfo> ToList()
        {
            var list = new StackListQueue<ReturnFieldInfo>();
            foreach (var value in Values)
                list.AddRange(value.ToList());
            return list;
        }

        public override string ToString()
        {
            var values = new Values();
            foreach (var pair in this)
                values.Add(new Values
                {
                    Key = Enumerable.Repeat(pair.Key, pair.Value.Count()),
                    Value = pair.Value.Select(item => item.ToString()),
                });
            return String.Parse(new Transformation().ParseTemplate(values));
        }

        public void Add(ReturnFieldInfo returnFieldInfo)
        {
            string key = returnFieldInfo.ReturnFieldId.ToString();
            if (!ContainsKey(key)) Add(key, new StackListQueue<ReturnFieldInfo> {returnFieldInfo});
            else
            {
                this[key] = new StackListQueue<ReturnFieldInfo>(this[key]) {returnFieldInfo};
            }
        }
    }
}