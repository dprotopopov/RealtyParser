﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using MyLibrary;
using MyLibrary.Trace;
using RealtyParser.Collections;
using RT.ParsingLibs.Models;

namespace RealtyParser
{
    /// <summary>
    ///     Класс вспомогательных алгоритмов
    /// </summary>
    public class Parser : MyParser.Parser, ITrace, IValueable
    {
        public Parser()
        {
            ModuleNamespace = GetType().Namespace;
        }

        public Database Database { private get; set; }
        public Converter Converter { private get; set; }

        public string ModuleNamespace { private get; set; }

        public new Values ToValues()
        {
            return new Values(this);
        }

        public new Values BuildValues(string template, HtmlNode node)
        {
            return new Values(base.BuildValues(template, node));
        }

        /// <summary>
        ///     Создание инстанса WebPublication на основе разобранных текстровых полей объявления
        /// </summary>
        public WebPublication CreateWebPublication(ReturnFields returnFields, RequestProperties requestProperties)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var webPublication = new WebPublication
            {
                AdditionalInfo = new AdditionalInfo
                {
                    RealtyAdditionalInfo = new RealtyAdditionalInfo()
                },
                Contact = new WebPublicationContact()
            };

            Debug.Assert(webPublication != null);
            Debug.Assert(webPublication.AdditionalInfo != null);
            Debug.Assert(webPublication.AdditionalInfo.RealtyAdditionalInfo != null);
            Debug.Assert(webPublication.Contact != null);

            foreach (
                PropertyInfo propertyInfo in
                    returnFields.GetType()
                        .GetProperties()
                        .Where(propertyInfo => !returnFields.ContainsKey(propertyInfo.Name)))
            {
                Debug.WriteLine(string.Format("No data for property {0}", propertyInfo.Name));
            }

            var stackListQueue = new StackListQueue<KeyValuePair<object, string>>
            {
                new KeyValuePair<object, string>(webPublication, webPublication.GetType().Name),
                new KeyValuePair<object, string>(webPublication.AdditionalInfo,
                    webPublication.GetType().Name + webPublication.AdditionalInfo.GetType().Name),
                new KeyValuePair<object, string>(webPublication.AdditionalInfo.RealtyAdditionalInfo,
                    webPublication.GetType().Name + webPublication.AdditionalInfo.GetType().Name +
                    webPublication.AdditionalInfo.RealtyAdditionalInfo.GetType().Name),
                new KeyValuePair<object, string>(webPublication.Contact,
                    webPublication.GetType().Name + "Contact"),
            };

            foreach (var pair in stackListQueue)
            {
                foreach (PropertyInfo propertyInfo in pair.Key.GetType().GetProperties())
                {
                    string propertyName = propertyInfo.Name;
                    string propertyFullName = pair.Value + propertyName;
                    PropertyInfo returnPropertyInfo = returnFields.GetType().GetProperty(propertyFullName);
                    if (returnPropertyInfo == null) continue;
                    Type propertyType = propertyInfo.PropertyType;
                    Type returnPropertyType = returnPropertyInfo.PropertyType;
                    try
                    {
                        var enumerable = (IEnumerable<string>) returnPropertyInfo.GetValue(returnFields);
                        List<string> list = enumerable.ToList();
                        object value = Converter.Convert(list, propertyType);
                        propertyInfo.SetValue(pair.Key, value);
                        Debug.WriteLine("Assign data for property {0} from {1}", propertyInfo.Name, propertyFullName);
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.ToString());
                        Debug.WriteLine("Try assign {0}->{1}", propertyFullName, propertyName);
                        Debug.WriteLine("Try convert {0}{1}->{2}{3}", returnPropertyType.Name,
                            (returnPropertyType.IsArray ? " As Array" : string.Empty), propertyType.Name,
                            (propertyType.IsArray ? " As Array" : string.Empty));
                    }
                }
            }

            Debug.Assert(webPublication != null);
            Debug.Assert(webPublication.AdditionalInfo != null);
            Debug.Assert(webPublication.AdditionalInfo.RealtyAdditionalInfo != null);
            Debug.Assert(webPublication.Contact != null);

            foreach (var pair in requestProperties)
            {
                string propertyName = string.Format("{0}Id", pair.Key);
                PropertyInfo propertyInfo = webPublication.GetType().GetProperty(propertyName);
                if (propertyInfo == null) continue;
                try
                {
                    propertyInfo.SetValue(webPublication, MyDatabase.Database.ConvertTo<long>(pair.Value));
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.ToString());
                    propertyInfo.SetValue(webPublication, (long) 0);
                }
            }

            foreach (var pair in stackListQueue)
            {
                foreach (PropertyInfo propertyInfo in pair.Key.GetType().GetProperties())
                {
                    try
                    {
                        object propertyValue = propertyInfo.GetValue(pair.Key);
                        Debug.WriteLine(propertyValue != null
                            ? string.Format("Property {0} is ok ({1})", propertyInfo.Name,
                                pair.Value + propertyInfo.Name)
                            : string.Format("Property {0} is null or empty ({1})", propertyInfo.Name,
                                pair.Value + propertyInfo.Name));
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine("Error in property {0} ({1})", propertyInfo.Name, pair.Value + propertyInfo.Name);
                        Debug.WriteLine(exception.ToString());
                    }
                }
            }

            Debug.Assert(webPublication != null);
            Debug.Assert(webPublication.AdditionalInfo != null);
            Debug.Assert(webPublication.AdditionalInfo.RealtyAdditionalInfo != null);
            Debug.Assert(webPublication.Contact != null);

            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return webPublication;
        }

        /// <summary>
        ///     Поиск и формирование значений возвращаемых полей загруженного с сайта объявления
        /// </summary>
        public ReturnFields BuildReturnFields(IEnumerable<HtmlDocument> parentDocuments, Values parentValues,
            ReturnFieldInfos returnFieldInfos)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var returnFields = new ReturnFields();
            long current = 0;
            long total = returnFieldInfos.ToList().Count*(parentDocuments.Count() + 1);
            foreach (ReturnFieldInfo returnFieldInfo in returnFieldInfos.ToList())
            {
                var agregated = new Values();
                foreach (HtmlDocument document in parentDocuments)
                {
                    var values = new Values(parentValues);
                    IEnumerable<string> xpaths =
                        Transformation.ParseTemplate(returnFieldInfo.ReturnFieldXpathTemplate.ToString(),
                            parentValues);

                    foreach (string xpath in xpaths) Debug.WriteLine(xpath);

                    foreach (
                        HtmlNode htmlNode in
                            xpaths.Select(xpath => document.DocumentNode.SelectNodes(xpath))
                                .Where(nodes => nodes != null)
                                .SelectMany(nodes => nodes))
                    {
                        values.Add(BuildValues(returnFieldInfo.ReturnFieldResultTemplate.ToString(), htmlNode));
                    }

                    foreach (var pair in values)
                        if (!agregated.ContainsKey((pair.Key))) agregated.Add(pair.Key, pair.Value);
                        else if (agregated[pair.Key].Count() < pair.Value.Count())
                            agregated[pair.Key] = pair.Value;

                    if (ProgressCallback != null) ProgressCallback(++current, total);
                }


                var regex = new Regex(returnFieldInfo.ReturnFieldRegexPattern.ToString(),
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);
                IEnumerable<string> list =
                    Transformation.ParseTemplate(returnFieldInfo.ReturnFieldResultTemplate.ToString(), agregated)
                        .SelectMany(
                            replace =>
                                (from Match match in
                                    Regex.Matches(replace,
                                        returnFieldInfo.ReturnFieldRegexPattern.ToString())
                                    select match.Value.Trim()))
                        .Select(
                            input => regex.Replace(input, returnFieldInfo.ReturnFieldRegexReplacement.ToString()).Trim())
                        .Where(value => !string.IsNullOrWhiteSpace(value));
                returnFields.Add(returnFieldInfo.ReturnFieldId.ToString(), list);
                Debug.WriteLine("{0}:{1}", returnFieldInfo.ReturnFieldId, string.Join(Environment.NewLine, list));
                if (ProgressCallback != null) ProgressCallback(++current, total);
            }

            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return returnFields;
        }

        /// <summary>
        ///     Формирование пар идентификатор параметра - значение параметра
        ///     для замены в строке-шаблоне
        /// </summary>
        public Values BuildValues(RequestProperties requestId, RequestProperties mappedId, RequestProperties mappedLevel)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine("requestId {0}", requestId);
            Debug.WriteLine("mappedId {0}", mappedId);
            IEnumerable<string> mapping =
                Database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());
            var values = new Values();

            long current = 0;
            long total = mapping.Count();
            foreach (string table in mapping)
            {
                Debug.Assert(requestId[table] != null &&
                             !string.IsNullOrWhiteSpace(requestId[table].ToString()));
                Debug.Assert(mappedId[table] != null &&
                             !string.IsNullOrWhiteSpace(mappedId[table].ToString()));

                string[] parents = mappedId[table].ToString().Split(SplitChar);
                values.Add(string.Format("{0}", table),
                    parents[Math.Min(MyDatabase.Database.ConvertTo<long>(mappedLevel[table]), parents.Length - 1)]);
                for (int i = 0; i < parents.Count(); i++)
                    values.Add(string.Format("{0}[{1}]", table, i), parents[Math.Min(i, parents.Length - 1)]);

                Collections.Properties fields = Database.GetUserFields(requestId[table],
                    mappedId[table],
                    table, requestId.Site);
                values.Add(fields.Keys.Select(item => item.ToString()), fields.Values.Select(item => item.ToString()));
                foreach (var field in fields)
                    try
                    {
                        values.Add(string.Format("{0}-1", field.Key),
                            (MyDatabase.Database.ConvertTo<long>(field.Value) - 1).ToString(CultureInfo.InvariantCulture));
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.ToString());
                    }

                if (ProgressCallback != null) ProgressCallback(++current, total);
            }

            {
                var tables = new[] {"Rubric", "Action"};
                List<string[]> parentses = tables.Select(table => mappedId[table].ToString().Split(SplitChar)).ToList();
                try
                {
                    IEnumerable<string> list =
                        parentses.Select(
                            (v, i) =>
                                v[Math.Min(MyDatabase.Database.ConvertTo<long>(mappedLevel[tables[i]]), v.Length - 1)]);
                    values.Add(string.Join(string.Empty, tables),
                        Database.GetScalar(list, tables, requestId.Site).ToString());
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    LastError = exception;
                }
                for (int i = 0; i < parentses[0].Count(); i++)
                    for (int j = 0; j < parentses[1].Count(); j++)
                        try
                        {
                            IEnumerable<string> list = new List<string> {parentses[0][i], parentses[1][j]};
                            values.Add(string.Format("{0}[{1},{2}]", string.Join(string.Empty, tables), i, j),
                                Database.GetScalar(list, tables, requestId.Site).ToString());
                        }
                        catch (Exception exception)
                        {
                            Debug.WriteLine(exception);
                            LastError = exception;
                        }
            }

            Debug.WriteLine("values {0}", values);
            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return values;
        }
    }
}