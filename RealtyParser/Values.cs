using System.Collections.Generic;
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

        public Values(ReturnFields returnFields)
        {
            foreach (var returnField in returnFields)
            {
                Add(Regex.Escape(System.String.Format(@"{{{{{0}}}}}", returnField.Key)), returnField.Value);
            }
        }

        public Values()
        {
        }

        public List<string> RegionId
        {
            get
            {
                string propertyName =
                    Regex.Escape(System.String.Format(@"{{{{{0}}}}}", MethodBase.GetCurrentMethod().Name.Substring(4)));
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    Regex.Escape(System.String.Format(@"{{{{{0}}}}}", MethodBase.GetCurrentMethod().Name.Substring(4)));
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public List<string> RubricId
        {
            get
            {
                string propertyName =
                    Regex.Escape(System.String.Format(@"{{{{{0}}}}}", MethodBase.GetCurrentMethod().Name.Substring(4)));
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    Regex.Escape(System.String.Format(@"{{{{{0}}}}}", MethodBase.GetCurrentMethod().Name.Substring(4)));
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public List<string> ActionId
        {
            get
            {
                string propertyName =
                    Regex.Escape(System.String.Format(@"{{{{{0}}}}}", MethodBase.GetCurrentMethod().Name.Substring(4)));
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    Regex.Escape(System.String.Format(@"{{{{{0}}}}}", MethodBase.GetCurrentMethod().Name.Substring(4)));
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public List<string> PublicationId
        {
            get
            {
                string propertyName =
                    Regex.Escape(System.String.Format(@"{{{{{0}}}}}", MethodBase.GetCurrentMethod().Name.Substring(4)));
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    Regex.Escape(System.String.Format(@"{{{{{0}}}}}", MethodBase.GetCurrentMethod().Name.Substring(4)));
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public Values InsertOrAppend(Values values)
        {
            foreach (var pair in values)
            {
                if (!ContainsKey(pair.Key))
                    Add(pair.Key, new List<string>());
                this[pair.Key].AddRange(pair.Value);
            }

            return this;
        }

        public Values InsertOrReplace(Values values)
        {
            foreach (var pair in values)
            {
                if (!ContainsKey(pair.Key))
                    Add(pair.Key, pair.Value);
                this[pair.Key] = pair.Value;
            }

            return this;
        }
    }
}