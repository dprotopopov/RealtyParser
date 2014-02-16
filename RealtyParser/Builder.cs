using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RealtyParser.Collections;

namespace RealtyParser
{
    public class Builder
    {
        private const string Empty = @"";
        private const string ReturnTitle = @"Title";

        #region

        private static readonly Database Database = new Database();
        private static readonly Transformation Transformation = new Transformation();
        private static readonly Parser Parser = new Parser {Transformation = Transformation};
        private static readonly ObjectComparer Comparer = new ObjectComparer();

        #endregion

        #region

        private readonly MethodInfo _getListMethodInfo = typeof (Database).GetMethod("GetList");
        private readonly MethodInfo _getMappingMethodInfo = typeof (Database).GetMethod("GetMapping");
        private readonly MethodInfo _scalarMethodInfo = typeof (Database).GetMethod("GetScalar");

        #endregion

        private readonly string _optionKey = Regex.Escape(@"{{Option}}");

        public Builder()
        {
            GridItems = new List<GridItem>();
            MinLevel = 0;
            MaxLevel = 1;
            SiteMinLevel = 0;
            SiteMaxLevel = 1;
            MaxDistance = Int32.MaxValue;
        }

        public ProgressCallback ProgressCallback { get; set; }
        public AppendLineCallback AppendLineCallback { get; set; }
        public CompliteCallback CompliteCallback { get; set; }

        public string CommandText { get; set; }
        public object TableName { get; set; }

        public object SiteId
        {
            get { return Site.Key; }
        }

        public KeyValuePair<object, object> Site { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public long MinLevel { get; set; }
        public long MaxLevel { get; set; }
        public long SiteMinLevel { get; set; }
        public long SiteMaxLevel { get; set; }
        public int MaxDistance { get; set; }
        public long Total { get; set; }
        public long Current { get; set; }

        public List<GridItem> GridItems { get; set; }
        public KeyValuePair<Dictionary<object, string>, Dictionary<object, string>> TitleData { get; set; }
        public KeyValuePair<Dictionary<object, string>, Dictionary<object, string>> IndexData { get; set; }


        public void BuildGridItems()
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var stack = new Stack<object>();
            stack.Push(CompliteCallback);
            CompliteCallback = null;

            var
                titles = new KeyValuePair<
                    Dictionary<object, KeyValuePair<object, string>>,
                    Dictionary<object, KeyValuePair<object, string>>>(
                    TitleData.Key.ToDictionary(
                        item => item.Key, item => item, Comparer),
                    TitleData.Value.ToDictionary(item => item.Key, item => item, Comparer));

            Mapping mapping = Database.GetMapping(TableName.ToString(), MinLevel, MaxLevel, SiteMinLevel, SiteMaxLevel,
                SiteId);

            GridItems.Clear();

            Total += mapping.Count;
            foreach (GridItem item in mapping.Select(pair => new GridItem
            {
                Key = titles.Key[pair.Key],
                Value = titles.Value[pair.Value]
            }))
            {
                GridItems.Add(item);
                if (ProgressCallback != null) ProgressCallback(++Current, Total);
            }


            CompliteCallback = (CompliteCallback) stack.Pop();


            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        public void RefreshGridItems()
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var stack = new Stack<object>();
            stack.Push(CompliteCallback);
            CompliteCallback = null;


            Total += GridItems.Count;

            AppendLineCallback(string.Format("CREATE TABLE IF NOT EXISTS {0}{1}{2}(", Database.SiteTable,
                TableName, Database.MappingTable));
            AppendLineCallback(string.Format("{0}{1} INTEGER,", Database.SiteTable, Database.IdColumn));
            AppendLineCallback(string.Format("{0}{1} INTEGER,", TableName, Database.IdColumn));
            AppendLineCallback(string.Format("{0}{1}{2} VARCHAR,", Database.SiteTable, TableName,
                Database.IdColumn));
            AppendLineCallback(string.Format("PRIMARY KEY({0}{2},{1}{2}));", Database.SiteTable,
                TableName,
                Database.IdColumn));

            string insertOrReplaceString =
                string.Format(
                    "INSERT OR REPLACE INTO {0}{1}{2}({0}{3},{1}{3},{0}{1}{3}) VALUES ({{{{{0}{3}}}}},{{{{{1}{3}}}}},'{{{{{0}{1}{3}}}}}');",
                    Database.SiteTable,
                    TableName, Database.MappingTable, Database.IdColumn);

            string siteIdPattern =
                Regex.Escape(string.Format("{{{{{0}{1}}}}}", Database.SiteTable, Database.IdColumn));
            string tableIdPattern =
                Regex.Escape(string.Format("{{{{{0}{1}}}}}", TableName, Database.IdColumn));
            string siteTableIdPattern =
                Regex.Escape(string.Format("{{{{{0}{1}{2}}}}}", Database.SiteTable, TableName, Database.IdColumn));

            var values = new Values
            {
                {siteIdPattern, Enumerable.Repeat(SiteId.ToString(), GridItems.Count).ToList()},
                {
                    tableIdPattern,
                    GridItems.Select(item => item.Key.Key.ToString()).ToList()
                },
                {
                    siteTableIdPattern,
                    GridItems.Select(item => item.Value.Key.ToString()).ToList()
                },
            };
            List<string> commandTexts = Transformation.ParseTemplate(insertOrReplaceString, values);
            AppendLineCallback("BEGIN;");
            foreach (string commandText in commandTexts)
            {
                AppendLineCallback(commandText);
                if (ProgressCallback != null) ProgressCallback(++Current, Total);
            }
            AppendLineCallback("COMMIT;");


            CompliteCallback = (CompliteCallback) stack.Pop();


            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        private Dictionary<object, string> BuildFullTitle(ICollection<object> items, IList<object[]> objects)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Debug.WriteLine("items.Length={0}", items.Count);
            var stack = new Stack<object>();
            stack.Push(CompliteCallback);
            CompliteCallback = null;

            Mapping parentMapping;
            Mapping levelMapping;
            Mapping titleMapping;

            lock (Database) titleMapping = (Mapping) _getMappingMethodInfo.Invoke(Database, new object[] {objects[0]});
            lock (Database) levelMapping = (Mapping) _getMappingMethodInfo.Invoke(Database, new object[] {objects[1]});
            lock (Database) parentMapping = (Mapping) _getMappingMethodInfo.Invoke(Database, new object[] {objects[2]});

            IEnumerable<string> mapping;
            IEnumerable<string> hierarchical;

            lock (Database)
                mapping = Database.GetList(Database.MappingTable, Database.TableNameColumn)
                    .Select(item => item.ToString());
            lock (Database)
                hierarchical = Database.GetList(Database.HierarchicalTable, Database.TableNameColumn)
                    .Select(item => item.ToString());

            Debug.Assert(mapping.Contains(TableName.ToString()));
            var dictionary = new Dictionary<object, string>();

            Total += items.Count();
            string[] enumerable = hierarchical as string[] ?? hierarchical.ToArray();
            bool contains = enumerable.Contains(TableName);
            foreach (object key in items)
            {
                object i = key;
                var list = new List<string>();
                do
                {
                    list.Add(titleMapping[i].ToString());
                } while
                    (
                    contains &&
                    Database.ConvertTo<long>(levelMapping[i]) > 1 &&
                    parentMapping.ContainsKey(i) && (i = parentMapping[i]) != null &&
                    !string.IsNullOrEmpty(i.ToString())
                    );
                string value = string.Join("::", list);
                dictionary.Add(key, value);
                if (ProgressCallback != null) ProgressCallback(++Current, Total);
            }

            CompliteCallback = (CompliteCallback) stack.Pop();


            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return dictionary;
        }

        private KeyValuePair<object[], object[]> GetAllChildren(KeyValuePair<object, object> pair,
            KeyValuePair<Dictionary<object, object[]>, Dictionary<object, object[]>> childrenPair)
        {
            var list = new List<object> {pair.Key};
            for (int i = 0; i < list.Count(); i++)
            {
                if (childrenPair.Key.ContainsKey(list[i]))
                    list.AddRange(childrenPair.Key[list[i]]);
            }
            var list1 = new List<object> {pair.Value};
            for (int i = 0; i < list1.Count(); i++)
            {
                if (childrenPair.Value.ContainsKey(list1[i]))
                    list1.AddRange(childrenPair.Value[list1[i]]);
            }
            return new KeyValuePair<object[], object[]>(list.ToArray(), list1.ToArray());
        }

        public void BuildMappingData()
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var stack = new Stack<object>();
            stack.Push(CompliteCallback);
            CompliteCallback = null;


            object[][] objects1 =
            {
                new object[]
                {
                    string.Format("{0}", TableName),
                    MinLevel,
                    MaxLevel
                },
                new[]
                {
                    string.Format("{0}", TableName),
                    SiteMinLevel,
                    SiteMaxLevel,
                    SiteId
                }
            };

            Mapping[] items;
            lock (Database)
                items =
                    objects1.Select(obj => (Mapping) _getMappingMethodInfo.Invoke(Database, new object[] {obj}))
                        .ToArray();

            IndexData =
                new KeyValuePair<Dictionary<object, string>, Dictionary<object, string>>(
                    items[0].ToDictionary(pair => pair.Key,
                        pair => String.ToTitleCase(String.NormalizeAddress(pair.Value.ToString().ToLower()))),
                    items[1].ToDictionary(pair => pair.Key,
                        pair => String.ToTitleCase(String.NormalizeAddress(pair.Value.ToString().ToLower()))));

            object[][][] objects =
            {
                new[]
                {
                    new object[]
                    {
                        string.Format("{0}", TableName),
                        string.Format("{0}{1}", TableName, Database.IdColumn),
                        string.Format("{0}{1}", TableName, Database.TitleColumn)
                    },
                    new object[]
                    {
                        string.Format("{0}", TableName),
                        string.Format("{0}{1}", TableName, Database.IdColumn),
                        Database.LevelColumn
                    },
                    new object[]
                    {
                        string.Format("{0}", TableName),
                        string.Format("{0}{1}", TableName, Database.IdColumn),
                        Database.ParentIdColumn
                    }
                },
                new[]
                {
                    new[]
                    {
                        string.Format("{0}{1}", Database.SiteTable, TableName),
                        string.Format("{0}{1}{2}", Database.SiteTable, TableName, Database.IdColumn),
                        string.Format("{0}{1}{2}", Database.SiteTable, TableName, Database.TitleColumn),
                        SiteId
                    },
                    new[]
                    {
                        string.Format("{0}{1}", Database.SiteTable, TableName),
                        string.Format("{0}{1}{2}", Database.SiteTable, TableName, Database.IdColumn),
                        Database.LevelColumn,
                        SiteId
                    },
                    new[]
                    {
                        string.Format("{0}{1}", Database.SiteTable, TableName),
                        string.Format("{0}{1}{2}", Database.SiteTable, TableName, Database.IdColumn),
                        Database.ParentIdColumn,
                        SiteId
                    }
                }
            };

            Dictionary<object, string>[] titles =
                objects.Select((x, i) => BuildFullTitle(items[i].Keys.ToArray(), x)).ToArray();

            TitleData =
                new KeyValuePair<Dictionary<object, string>, Dictionary<object, string>>(titles[0], titles[1]);

            CompliteCallback = (CompliteCallback) stack.Pop();

            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        public void BuildMapping()
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var stack = new Stack<object>();
            stack.Push(CompliteCallback);
            CompliteCallback = null;


            Dictionary<object, string> key = IndexData.Key;
            Dictionary<object, string> value = IndexData.Value;

            var data = new KeyValuePair<object[], object[]>(key.Keys.ToArray(), value.Keys.ToArray());
            Array.Sort(data.Key, Comparer);
            Array.Sort(data.Value, Comparer);

            var
                titles = new KeyValuePair<
                    Dictionary<object, KeyValuePair<object, string>>,
                    Dictionary<object, KeyValuePair<object, string>>>(
                    TitleData.Key.ToDictionary(
                        item => item.Key, item => item, Comparer),
                    TitleData.Value.ToDictionary(item => item.Key, item => item, Comparer));

            GridItems.Clear();

            var progress = new object();
            var queueStack = new QueueStack<KeyValuePair<object[], object[]>>();

            if (MinLevel > 0 && SiteMinLevel > 0)
            {
                Mapping mapping = Database.GetMapping(TableName.ToString(), MinLevel - 1, MinLevel - 1, SiteMinLevel - 1,
                    SiteMinLevel - 1,
                    SiteId);

                Debug.WriteLine("mapping.Count {0}", mapping.Count);
                Total += mapping.Count;


                object[][] objects =
                {
                    new object[]
                    {
                        string.Format("{0}", TableName),
                        string.Format("{0}{1}", TableName, Database.IdColumn),
                        Database.ParentIdColumn,
                        MinLevel,
                        MaxLevel
                    },
                    new[]
                    {
                        string.Format("{0}{1}", Database.SiteTable, TableName),
                        string.Format("{0}{1}{2}", Database.SiteTable, TableName, Database.IdColumn),
                        Database.ParentIdColumn,
                        SiteMinLevel,
                        SiteMaxLevel,
                        SiteId
                    }
                };

                Mapping[] parentMappings =
                    objects.Select(obj => (Mapping) _getMappingMethodInfo.Invoke(Database, new object[] {obj}))
                        .ToArray();

                lock (progress) Total += parentMappings[0].Count + parentMappings[1].Count;
                lock (progress) if (ProgressCallback != null) ProgressCallback(Current, Total);

                Dictionary<object, object[]>[] children =
                    parentMappings.Select(
                        parentMapping =>
                            parentMapping.Values.Distinct()
                                .ToDictionary(item => item,
                                    item =>
                                        parentMapping.Where(pair => Comparer.Equals(pair.Value, item))
                                            .Select(pair => pair.Key).ToArray(), Comparer)).ToArray();
                var childrenPair =
                    new KeyValuePair<Dictionary<object, object[]>, Dictionary<object, object[]>>(children[0],
                        children[1]);

                lock (progress) Current += parentMappings[0].Count + parentMappings[1].Count;
                lock (progress) if (ProgressCallback != null) ProgressCallback(Current, Total);

                Parallel.ForEach(mapping, map =>
                {
                    KeyValuePair<object[], object[]> pair = GetAllChildren(map, childrenPair);
                    if (pair.Key.Any() && pair.Value.Any())
                    {
                        lock (progress) Total += pair.Key.Length + pair.Value.Length;
                        lock (progress) if (ProgressCallback != null) ProgressCallback(Current, Total);

                        Array.Sort(pair.Key, Comparer);
                        Array.Sort(pair.Value, Comparer);

                        var keyValuePair =
                            new KeyValuePair<object[], object[]>(
                                Array<object>.IntersectSorted(pair.Key, data.Key, Comparer),
                                Array<object>.IntersectSorted(pair.Value, data.Value, Comparer));

                        lock (progress) Current += pair.Key.Length + pair.Value.Length;
                        lock (progress) if (ProgressCallback != null) ProgressCallback(Current, Total);

                        lock (queueStack) queueStack.Enqueue(keyValuePair);

                        lock (progress) Total += keyValuePair.Key.Length + keyValuePair.Value.Length;
                        lock (progress) if (ProgressCallback != null) ProgressCallback(Current, Total);
                    }

                    lock (progress) if (ProgressCallback != null) ProgressCallback(++Current, Total);
                });
            }
            else
            {
                queueStack.Enqueue(data);
                Total += data.Key.Length + data.Value.Length;
                if (ProgressCallback != null) ProgressCallback(Current, Total);
            }

            var gridItems = new object();

            Total += queueStack.Count;

            Parallel.ForEach(queueStack, keyValuePair =>
            {
                List<string> list = keyValuePair.Key.Select(item => key[item]).ToList();
                list.AddRange(keyValuePair.Value.Select(item => value[item]).ToList());
                Dictionary<string, KeyValuePair<object[], object[]>> map = list.Distinct().ToDictionary(name => name,
                    name =>
                        new KeyValuePair<object[], object[]>(
                            (keyValuePair.Key.Where(
                                item => System.String.Compare(key[item], name, StringComparison.OrdinalIgnoreCase) == 0)
                                ).ToArray(),
                            (keyValuePair.Value.Where(
                                item =>
                                    System.String.Compare(value[item], name, StringComparison.OrdinalIgnoreCase) == 0)
                                ).ToArray()));

                lock (progress) Current += keyValuePair.Key.Length + keyValuePair.Value.Length;
                lock (progress) if (ProgressCallback != null) ProgressCallback(Current, Total);

                IEnumerable<string> bad =
                    (from item in map where item.Value.Value.Length == 0 select item.Key);
                IEnumerable<string> good =
                    (from item in map where item.Value.Value.Length > 0 select item.Key);

                lock (progress) Total += bad.Count()*good.Count();
                lock (progress) if (ProgressCallback != null) ProgressCallback(Current, Total);

                Dictionary<string, string> dictionary = bad.ToDictionary(s => s,
                    s => LevenshteinDistance.FindNeighbour(s, good, MaxDistance));
                foreach (
                    var pair in
                        dictionary.Where(pair => !string.IsNullOrEmpty(pair.Key) && !string.IsNullOrEmpty(pair.Value)))
                {
                    map[pair.Key] = new KeyValuePair<object[], object[]>(map[pair.Key].Key, map[pair.Value].Value);
                }

                lock (progress) Current += bad.Count()*good.Count();
                lock (progress) if (ProgressCallback != null) ProgressCallback(Current, Total);

                lock (gridItems)
                    GridItems.AddRange(from pair in map.Values
                        from keyIndex in pair.Key
                        from valueIndex in pair.Value
                        select new GridItem
                        {
                            Key = titles.Key[keyIndex],
                            Value = titles.Value[valueIndex]
                        });

                lock (progress) if (ProgressCallback != null) ProgressCallback(++Current, Total);
            });


            if (CompliteCallback != null) CompliteCallback();

            CompliteCallback = (CompliteCallback) stack.Pop();

            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        public void ExecuteNonQuery()
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var stack = new Stack<object>();
            stack.Push(CompliteCallback);
            CompliteCallback = null;


            string[] commandStrings = CommandText.Split(';');

            Total += commandStrings.Length;
            Database.Connection.Open();
            foreach (string commandString in commandStrings)
                using (SQLiteCommand command = Database.Connection.CreateCommand())
                {
                    string commandText = commandString.Trim();
                    if (!string.IsNullOrEmpty(commandText))
                    {
                        command.CommandText = commandText;
                        command.ExecuteNonQuery();
                    }
                    if (ProgressCallback != null) ProgressCallback(++Current, Total);
                }
            Database.Connection.Close();

            CompliteCallback = (CompliteCallback) stack.Pop();


            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        public async void DownloadTable()
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var stack = new Stack<object>();
            stack.Push(CompliteCallback);
            CompliteCallback = null;


            AppendLineCallback(string.Format("CREATE TABLE IF NOT EXISTS {0}{1}(", Database.SiteTable,
                TableName));
            AppendLineCallback(string.Format("{0}{1} INTEGER,", Database.SiteTable, Database.IdColumn));
            AppendLineCallback(string.Format("{0}{1}{2} VARCHAR,", Database.SiteTable, TableName,
                Database.IdColumn));
            AppendLineCallback(string.Format("{0}{1}{2} VARCHAR,", Database.SiteTable, TableName,
                Database.TitleColumn));
            AppendLineCallback(string.Format("{0} VARCHAR,", Database.ParentIdColumn));
            AppendLineCallback(string.Format("{0} INTEGER,", Database.LevelColumn));
            AppendLineCallback(string.Format("PRIMARY KEY({0}{2},{0}{1}{2}));", Database.SiteTable,
                TableName,
                Database.IdColumn));
            AppendLineCallback("BEGIN;");

            string insertOrReplaceString =
                string.Format(
                    "INSERT OR REPLACE INTO {0}{1}({0}{2},{0}{1}{2},{0}{1}{3},{4},{5}) VALUES ({{{{{0}{2}}}}},'{{{{{0}{1}{2}}}}}','{{{{{0}{1}{3}}}}}','{{{{{4}}}}}',{{{{{5}}}}});",
                    Database.SiteTable, TableName, Database.IdColumn, Database.TitleColumn,
                    Database.ParentIdColumn, Database.LevelColumn);

            string siteIdPattern =
                Regex.Escape(string.Format("{{{{{0}{1}}}}}", Database.SiteTable, Database.IdColumn));
            string siteTableIdPattern =
                Regex.Escape(string.Format("{{{{{0}{1}{2}}}}}", Database.SiteTable, TableName, Database.IdColumn));
            string siteTableTitlePattern =
                Regex.Escape(string.Format("{{{{{0}{1}{2}}}}}", Database.SiteTable, TableName, Database.TitleColumn));
            string parentIdPattern = Regex.Escape(string.Format("{{{{{0}}}}}", Database.ParentIdColumn));
            string levelPattern = Regex.Escape(string.Format("{{{{{0}}}}}", Database.LevelColumn));

            SiteProperties properties = Database.GetSiteProperties(SiteId);
            BuilderInfos infos = Database.GetBuilderInfos(SiteId);
            BuilderInfo info = infos[TableName.ToString()];

            var baseBuilder = new UriBuilder(properties.Url.ToString())
            {
                UserName = properties.UserName.ToString(),
                Password = properties.Password.ToString(),
            };

            MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(info.IdTemplate.ToString(),
                Transformation.FieldPattern);


            var queue = new Queue<Values>();
            queue.Enqueue(new Values {{_optionKey, Empty}});
            Total++;

            while (queue.Any())
            {
                Values parentValues = queue.Dequeue();

                int maxCount = parentValues.MaxCount;
                Match option = matches[parentValues.Count - 1];
                parentValues[_optionKey] =
                    Enumerable.Repeat(option.Groups[Transformation.NameGroup].Value, maxCount).ToList();

                List<string> urls = Transformation.ParseTemplate(info.UrlTemplate.ToString(), parentValues);

                var returnFieldInfos = new ReturnFieldInfos();
                foreach (var pair in new Dictionary<object, object>
                {
                    {option.Groups[Transformation.NameGroup].Value, info.KeySelectTemplate},
                    {ReturnTitle, info.TitleSelectTemplate}
                })
                {
                    returnFieldInfos.Add(
                        new ReturnFieldInfo
                        {
                            SiteId = SiteId,
                            ReturnFieldId = pair.Key,
                            ReturnFieldXpathTemplate = info.XPathTemplate,
                            ReturnFieldResultTemplate = pair.Value,
                            ReturnFieldRegexPattern = @".*",
                            ReturnFieldRegexReplacement = @"$&",
                            ReturnFieldRegexMatchPattern = @".*",
                        });
                }
                List<string> parentIds = Transformation.ParseTemplate(info.IdTemplate.ToString(), parentValues);
                for (int i = 0; i < maxCount; i++)
                {
                    Values slice = parentValues.Slice(i);
                    var builder = new UriBuilder(baseBuilder + urls[i]);
                    HtmlDocument[] documents =
                        await
                            Parser.WebRequestHtmlDocument(builder.Uri, properties.Method.ToString(),
                                properties.Encoding.ToString());

                    HtmlNode[] nodes = documents.Select(document => document.DocumentNode).ToArray();
                    ReturnFields returnFields = Parser.BuildReturnFields(nodes,
                        slice, returnFieldInfos);
                    var returnValues = new Values(returnFields);
                    foreach (var pair in slice)
                        returnValues.Add(pair.Key, Enumerable.Repeat(pair.Value[0], returnFields.MaxCount).ToList());
                    List<string> ids = Transformation.ParseTemplate(info.IdTemplate.ToString(),
                        returnValues);
                    List<string> titles = returnFields[ReturnTitle];
                    var items = new Values
                    {
                        {
                            siteIdPattern,
                            Enumerable.Repeat(SiteId.ToString(),
                                returnFields.MaxCount).ToList()
                        },
                        {siteTableIdPattern, ids},
                        {siteTableTitlePattern, titles},
                        {parentIdPattern, Enumerable.Repeat(parentIds[i], returnFields.MaxCount).ToList()},
                        {
                            levelPattern,
                            Enumerable.Repeat(parentValues.Count.ToString(CultureInfo.InvariantCulture),
                                returnFields.MaxCount).ToList()
                        },
                    };

                    List<string> commandTexts = Transformation.ParseTemplate(insertOrReplaceString, items);
                    Total += commandTexts.Count;
                    foreach (string commandText in commandTexts)
                    {
                        AppendLineCallback(commandText);
                        if (ProgressCallback != null) ProgressCallback(++Current, Total);
                    }

                    if (parentValues.Count < matches.Count)
                    {
                        returnValues.Remove(Regex.Escape(string.Format("{{{{{0}}}}}", ReturnTitle)));
                        queue.Enqueue(returnValues);
                        Total += returnValues.MaxCount;
                    }
                    if (ProgressCallback != null) ProgressCallback(++Current, Total);
                }
            }

            AppendLineCallback("COMMIT;");
            CompliteCallback = (CompliteCallback) stack.Pop();


            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        public static void ThreadProc(object obj)
        {
            var This = obj as Builder;
            object[] objects = {};
            if (This != null) This.MethodInfo.Invoke(This, objects);
        }

        public class GridItem
        {
            public KeyValuePair<object, string> Key { get; set; }
            public KeyValuePair<object, string> Value { get; set; }
        }
    }
}