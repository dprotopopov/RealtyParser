using System.Reflection;

namespace RealtyParser
{
    /// <summary>
    ///     Парамтры для процедуры возвращающей вычисленные поля при разборе страницы одного объявления
    ///     Данные хранятся в базе данных
    /// </summary>
    public class ReturnFieldInfo : MyParser.ReturnFieldInfo, IValueable
    {
        /// <summary>
        ///     Идентификатор сайта
        /// </summary>
        public new object SiteId
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, "");
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

        /// <summary>
        ///     Идентификатор возвращаемого поля
        /// </summary>
        public new object ReturnFieldId
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, "");
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

        /// <summary>
        ///     Xpath для нахождения поля на загруженной странице
        /// </summary>
        public new object ReturnFieldXpathTemplate
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, "");
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

        /// <summary>
        ///     Шаблон возвращаемого найденого текста
        /// </summary>
        public new object ReturnFieldResultTemplate
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, "");
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

        /// <summary>
        ///     Шаблон регулярного выражения,
        ///     используемого при замене найденого текста
        /// </summary>
        public new object ReturnFieldRegexPattern
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, "");
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

        /// <summary>
        ///     Шаблон для замены у регулярного выражения,
        ///     используемого при замене найденого текста
        /// </summary>
        public new object ReturnFieldRegexReplacement
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, "");
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

        public new Values ToValues()
        {
            return new Values(this);
        }

        /// <summary>
        ///     Стандартный конвертер содержания в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", SiteId, ReturnFieldId, ReturnFieldXpathTemplate,
                ReturnFieldResultTemplate, ReturnFieldRegexPattern, ReturnFieldRegexReplacement);
        }
    }
}