using System.Collections.Generic;
using System.Reflection;

namespace RealtyParser
{
    /// <summary>
    /// Настройки для сайта, хранимые в базе данных
    /// </summary>
    public class SiteProperties : Dictionary<string, string>
    {
        /// <summary>
        /// Парамтры для процедуры возвращающей вычисленные поля при разборе страницы одного объявления
        /// </summary>
        public List<ReturnFieldInfo> ReturnFieldInfos { get; set; }

        /// <summary>
        /// Список таблиц мапирования из внутренних справочников действий, рубрик, регионов в справочники сайта
        /// </summary>
        public Mapping Mapping { get; set; }
        /// <summary>
        /// Таблица мапирования из внутреннего справочника действий в справочники сайта
        /// </summary>
        public Dictionary<long, string> ActionMapping
        {
            get { return Mapping.Action; }
            set { Mapping.Action = value; }
        }
        /// <summary>
        /// Таблица мапирования из внутреннего справочника рубрик в справочники сайта
        /// </summary>
        public Dictionary<long, string> RubricMapping
        {
            get { return Mapping.Rubric; }
            set { Mapping.Rubric = value; }
        }
        /// <summary>
        /// Таблица мапирования из внутреннего справочника регионов в справочники сайта
        /// </summary>
        public Dictionary<long, string> RegionMapping
        {
            get { return Mapping.Region; }
            set { Mapping.Region = value; }
        }
        /// <summary>
        /// Идентификатор сайта
        /// </summary>
        public string SiteId
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
        /// Название сайта
        /// </summary>
        public string SiteTitle
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
        /// Url сайта
        /// </summary>
        public string Url
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
        /// Метод отправки запроса GET/POST
        /// </summary>
        public string Method
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
        /// Шаблон формирования запроса определения LastPublicationId
        /// </summary>
        public string LastPublicationIdSearchTemplate
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
        /// Шаблон формирования расширенного запроса
        /// </summary>
       public string ExtSearchTemplate
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
        /// Шаблон формирования одиночного запроса
        /// </summary>
        public string UnoSearchTemplate
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
        /// Xpath для нахождения LastPublicationId на загруженной странице
        /// </summary>
        public string LastPublicationIdXpathTemplate
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
        /// Xpath для нахождения списка идентификаторов объявлений на загруженной странице
        /// </summary>
        public string ExtXpathTemplate
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
        /// Иля пользователя при отправке запроса к сайту
        /// </summary>
        public string UserName
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
        /// Пароль пользователя при отправке запроса к сайту
        /// </summary>
        public string Password
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
        /// Максимальное число возвращаемых обхявлений
        /// </summary>
        public string CountAd
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
        /// Название класса из данной сборки, реализующего Icomparer<string> для сравнения двух идентификаторов обхявлений
        /// </summary>
        public string PublicationIdComparerClassName
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
        /// Шаблон возвращаемого найденого текста LastPublicationId на загруженной странице
        /// </summary>
        public string LastPublicationIdResultTemplate
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
        /// Шаблон возвращаемого найденого текста номера объявления на загруженной странице
        /// </summary>
        public string ExtResultTemplate
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
        /// Шаблон регулярного выражения, 
        /// используемого при замене найденого текста с LastPublicationId на загруженной странице
        /// </summary>
        public string LastPublicationIdRegexPattern
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
        /// Шаблон для замены у регулярного выражения, 
        /// используемого при замене найденого текста с LastPublicationId на загруженной странице
        /// </summary>
        public string LastPublicationIdRegexReplacement
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
        /// Шаблон регулярного выражения, 
        /// используемого при замене найденого текста с идентификатором объявления на загруженной странице
        /// </summary>
       public string ExtRegexPattern
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
        /// Шаблон для замены у регулярного выражения, 
        /// используемого при замене найденого текста с идентификатором объявления на загруженной странице
        /// </summary>
        public string ExtRegexReplacement
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
        /// Идентификатор кодировки сайта
        ///     windows-1351
        ///     utf-8
        ///     ...
        /// </summary>
       public string Encoding
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
    }
}
