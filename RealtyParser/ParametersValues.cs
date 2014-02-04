using System.Collections.Generic;
using System.Reflection;

namespace RealtyParser
{
    /// <summary>
    ///     Класс для хранения значения параметров, передаваемых в процедуру замены полей в шаблоне
    ///     Используется для доступа к значениям словаря по ключу
    ///     Ключи словаря представляют собой строки, передаваемые в качестве регулярного выражения
    ///     в функцию Regex.Replace для замены полей в шаблоне на значения данного словаря
    /// </summary>
    public class ParametersValues : DictionaryOfList
    {
        public ParametersValues(ParametersValues args)
        {
            InsertOrReplace(args);
        }

        public ParametersValues(ReturnFields returnFields)
        {
            foreach (var returnField in returnFields)
            {
                Add(RealtyParserUtils.RegexEscape(@"{{" + returnField.Key + @"}}"), returnField.Value);
            }
        }

        public ParametersValues()
        {
        }

        public List<string> RegionId
        {
            get
            {
                string propertyName =
                    RealtyParserUtils.RegexEscape(@"{{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"}}");
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    RealtyParserUtils.RegexEscape(@"{{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"}}");
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
                    RealtyParserUtils.RegexEscape(@"{{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"}}");
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    RealtyParserUtils.RegexEscape(@"{{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"}}");
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
                    RealtyParserUtils.RegexEscape(@"{{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"}}");
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    RealtyParserUtils.RegexEscape(@"{{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"}}");
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
                    RealtyParserUtils.RegexEscape(@"{{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"}}");
                if (!ContainsKey(propertyName)) Add(propertyName, new List<string>());
                return this[propertyName];
            }
            set
            {
                string propertyName =
                    RealtyParserUtils.RegexEscape(@"{{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"}}");
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public ParametersValues InsertOrAppend(ParametersValues dictionary)
        {
            foreach (var arg in dictionary)
            {
                if (!ContainsKey(arg.Key))
                    Add(arg.Key, new List<string>());
                this[arg.Key].AddRange(arg.Value);
            }

            return this;
        }

        public ParametersValues InsertOrReplace(ParametersValues dictionary)
        {
            foreach (var arg in dictionary)
            {
                if (!ContainsKey(arg.Key))
                    Add(arg.Key, arg.Value);
                this[arg.Key] = arg.Value;
            }

            return this;
        }
    }
}