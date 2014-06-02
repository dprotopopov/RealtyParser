using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyLibrary;
using MyLibrary.Attributes;
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
            Parallel.ForEach(
                str.Split(',')
                    .Select(s => s.Trim())
                    .Where(String.IsNotNullAndNotEmpty)
                    .Select(s => Regex.Match(s, StructurePattern))
                    .Where(match => !string.IsNullOrWhiteSpace(match.Groups["key"].Value)), match =>
                    {
                        PropertyInfo propertyInfo = GetType()
                            .GetProperty(match.Groups["key"].Value,
                                BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                        object value = (propertyInfo == null)
                            ? null
                            : Converter.Convert(match.Groups["value"].Value, propertyInfo.PropertyType);
                        if (propertyInfo == null || value == null)
                            lock (this) Add(match.Groups["key"].Value, match.Groups["value"].Value);
                        else
                            lock (this) propertyInfo.SetValue(this, value, null);
                    });
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