using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using MyLibrary;
using MyLibrary.Attribute;
using MyLibrary.Comparer;
using String = MyLibrary.Types.String;

namespace RealtyParser
{
    public class Resource : RequestProperties, IValueable
    {
        public Resource(string str, string template)
        {
            Template = template;
            ObjectComparer = new ObjectComparer();
            Converter = new Converter();
            foreach (
                Match match in
                    str.Split(',')
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Select(s => Regex.Match(s, StructurePattern))
                        .Where(match => !string.IsNullOrWhiteSpace(match.Groups["key"].Value)))
                try
                {
                    PropertyInfo propertyInfo = GetType()
                        .GetProperty(match.Groups["key"].Value,
                            BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                    propertyInfo.SetValue(this,
                        Converter.Convert(match.Groups["value"].Value, propertyInfo.PropertyType), null);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.ToString());
                    Add(match.Groups["key"].Value, match.Groups["value"].Value);
                }
        }

        private string Template { get; set; }
        public Transformation Transformation { get; set; }

        [Value]
        public new object PublicationId
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, MyLibrary.Types.DateTime.Default);
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

        [Value]
        public new DateTime PublicationDatetime
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (!ContainsKey(propertyName)) Add(propertyName, MyLibrary.Types.DateTime.Default);
                return (DateTime) this[propertyName];
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

        [Value]
        public new object Action
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

        [Value]
        public new object Rubric
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

        [Value]
        public new object Region
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

        public new Values ToValues()
        {
            return new Values(this);
        }

        public override string ToString()
        {
            return String.Parse(new MyLibrary.Transformation().ParseTemplate(Template, ToValues()));
        }
    }
}