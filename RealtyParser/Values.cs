using System;
using System.Collections.Generic;
using System.Reflection;
using MyLibrary.Attribute;
using RealtyParser.Collections;

namespace RealtyParser
{
    /// <summary>
    ///     Класс для хранения значения параметров, передаваемых в процедуру замены полей в шаблоне
    ///     Используется для доступа к значениям словаря по ключу
    ///     Ключи словаря представляют собой строки, передаваемые в качестве регулярного выражения
    ///     в функцию Regex.Replace для замены полей в шаблоне на значения данного словаря
    /// </summary>
    public class Values : MyParser.Values, IValueable
    {
        public Values(IEnumerable<KeyValuePair<string, IEnumerable<string>>> list)
            : base(list)
        {
        }

        public Values()
        {
        }

        public Values(IEnumerable<string> keys, IEnumerable<string> values)
        {
            Key = keys;
            Value = values;
        }

        public Values(Object obj)
            : base(obj)
        {
        }

        [Value]
        public new IEnumerable<string> Option
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public new IEnumerable<string> Value
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public new IEnumerable<string> Key
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public new IEnumerable<string> Page
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public IEnumerable<string> PageStart
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public new IEnumerable<string> Url
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public IEnumerable<string> Region
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public IEnumerable<string> Rubric
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public IEnumerable<string> RubricAction
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public IEnumerable<string> Action
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public IEnumerable<string> PublicationId
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public new IEnumerable<string> Title
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        [Value]
        public new IEnumerable<string> Table
        {
            get
            {
                string propertyName =
                    MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, new StackListQueue<string>());
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

        public new Values Slice(int row)
        {
            return new Values(base.Slice(row));
        }

        public new Values ToValues()
        {
            return new Values(this);
        }
    }
}