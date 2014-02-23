using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RealtyParser.Types;

namespace RealtyParser
{
    public class ReturnFieldInfos : Dictionary<string, IEnumerable<ReturnFieldInfo>>
    {
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

        public List<ReturnFieldInfo> ToList()
        {
            var list = new List<ReturnFieldInfo>();
            foreach (var value in Values)
                list.AddRange(value.ToList());
            return list;
        }

        public override string ToString()
        {
            var values = new Values();
            foreach (var pair in this)
                values.InsertOrAppend(new Values
                {
                    Key = Enumerable.Repeat(pair.Key, pair.Value.Count()),
                    Value = pair.Value.Select(item => item.ToString()),
                });
            return String.Parse(new Transformation().ParseTemplate(values));
        }

        public void Add(ReturnFieldInfo returnFieldInfo)
        {
            string key = returnFieldInfo.ReturnFieldId.ToString();
            if (!ContainsKey(key)) Add(key, new List<ReturnFieldInfo> {returnFieldInfo});
            else
            {
                List<ReturnFieldInfo> list = this[key].ToList();
                list.Add(returnFieldInfo);
                this[key] = list;
            }
        }
    }
}