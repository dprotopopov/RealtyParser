using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using RealtyParser.Collections;

namespace RealtyParser
{
    /// <summary>
    ///     Класс для хранения значения параметров, передаваемых в процедуру замены полей в шаблоне
    ///     Используется для доступа к значениям словаря по ключу
    ///     Ключи словаря представляют собой строки, передаваемые в качестве регулярного выражения
    ///     в функцию Regex.Replace для замены полей в шаблоне на значения данного словаря
    /// </summary>
    public class Values : DictionaryOfList
    {
        public Values(Values values)
        {
            InsertOrReplace(values);
        }

        public Values(Values values, int i)
        {
            InsertOrReplace(values.Slice(i));
        }

        public Values(ReturnFields returnFields)
        {
            foreach (var returnField in returnFields)
            {
                Add(string.Format("{0}", returnField.Key), returnField.Value);
            }
        }

        public Values()
        {
        }

        public IEnumerable<string> Option
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> Value
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> Key
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> Page
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> Url
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> Region
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> Rubric
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> Action
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> PublicationId
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> Title
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public IEnumerable<string> Table
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public void InsertOrAppend(Values values)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Type[] types = {typeof (DictionaryOfList)};
            MethodInfo methodInfo = GetType().GetMethod(methodName, types);
            Debug.Assert(methodInfo != null);
            object[] objects = {values};
            methodInfo.Invoke(this, objects);
        }

        public void InsertOrReplace(Values values)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Type[] types = {typeof (DictionaryOfList)};
            MethodInfo methodInfo = GetType().GetMethod(methodName, types);
            Debug.Assert(methodInfo != null);
            object[] objects = {values};
            methodInfo.Invoke(this, objects);
        }

        public new Values Slice(int i)
        {
            var values = new Values();
            foreach (var pair in this.Where(pair => i < pair.Value.Count()))
                values.Add(pair.Key, pair.Value.ToList()[i]);
            return values;
        }
    }
}