using System.Collections.Generic;
using System.Reflection;

namespace RealtyParser
{
    public class RequestProperties : Collections.Properties
    {
        public object Site
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, 0);
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

        public object Action
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, 0);
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

        public object Rubric
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, 0);
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

        public object Region
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, 0);
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

        public void Add(Dictionary<string, object> dictionary)
        {
            foreach (var pair in dictionary)
            {
                if (ContainsKey(pair.Key)) this[pair.Key] = pair.Value;
                else Add(pair.Key, pair.Value);
            }
        }
    }
}