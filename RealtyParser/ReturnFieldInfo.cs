using System.Collections.Generic;
using System.Reflection;

namespace RealtyParser
{
    public class ReturnFieldInfo : Dictionary<string, string>
    {
        public string SiteId
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return ContainsKey(propertyName) ? this[propertyName] : null;
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
        public string ReturnFieldId
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return ContainsKey(propertyName) ? this[propertyName] : null;
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
        public string UnoReturnFieldXpathTemplate
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return ContainsKey(propertyName) ? this[propertyName] : null;
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
        public string UnoReturnFieldResultTemplate
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return ContainsKey(propertyName) ? this[propertyName] : null;
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
        public string UnoReturnFieldRegexPattern
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return ContainsKey(propertyName) ? this[propertyName] : null;
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
        public string UnoReturnFieldRegexReplacement
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return ContainsKey(propertyName) ? this[propertyName] : null;
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
    }
}
