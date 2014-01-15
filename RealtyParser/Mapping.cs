using System.Collections.Generic;
using System.Reflection;

namespace RealtyParser
{
    public class Mapping : Dictionary<string, Dictionary<long, string>>
    {
        public Dictionary<long, string> Action
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
        public Dictionary<long, string> Rubric
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
        public Dictionary<long, string> Region
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
