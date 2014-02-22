using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using RealtyParser.Collections;

namespace RealtyParser
{
    /// <summary>
    ///     Класс для работы с базой данных
    /// </summary>
    public class Database
    {
        public const string SiteTable = "Site";
        public const string MappingTable = "Mapping";
        public const string HierarchicalTable = "Hierarchical";
        public const string ReturnFieldTable = "ReturnField";
        public const string BuilderTable = "Builder";

        public const string IdColumn = "Id";
        public const string TitleColumn = "Title";
        public const string TableNameColumn = "TableName";
        public const string LevelColumn = "Level";
        public const string ParentIdColumn = "Parent" + IdColumn;
        public const string HasChildColumn = "HasChild";
        public const string ModuleNamespaceColumn = "ModuleNamespace";
        public readonly string SiteIdColumn = string.Format("{0}{1}", SiteTable, IdColumn);

        private readonly MethodInfo _stringFormatMethodInfo = typeof (string).GetMethod("Format",
            new[] {typeof (string), typeof (object[])});

        public Database()
        {
            ModuleNamespace = GetType().Namespace;
            ConnectionString = string.Format("data source={0}.db", ModuleNamespace);
            Connection = new SQLiteConnection(ConnectionString);
        }

        public string ModuleNamespace { get; set; }

        private string ConnectionString { get; set; }

        public ProgressCallback ProgressCallback { get; set; }
        public AppendLineCallback AppendLineCallback { get; set; }
        public CompliteCallback CompliteCallback { get; set; }

        /// <summary>
        ///     Коннектор к базе данных
        /// </summary>
        public SQLiteConnection Connection { get; set; }

        public static T ConvertTo<T>(object obj)
        {
            return (T) TypeDescriptor.GetConverter(obj).ConvertTo(obj, typeof (T));
        }

        /// <summary>
        ///     Загрузка из базы данных настроек указанного сайта
        /// </summary>
        public SiteProperties GetSiteProperties(object siteId)
        {
            Debug.Assert(siteId != null && !string.IsNullOrEmpty(siteId.ToString()));
            var properties = new SiteProperties();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = string.Format("SELECT * FROM {0} WHERE {0}{1}=@{0}{1}", SiteTable, IdColumn);
                command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn), siteId));
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                long current = 0;
                long total = reader.FieldCount;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string key = reader.GetName(i);
                    object value = reader[key];
                    properties.Add(key, value);
                    if (ProgressCallback != null) ProgressCallback(++current, total);
                }
                Connection.Close();
            }
            if (CompliteCallback != null) CompliteCallback();
            return properties;
        }

        public Mappings GetMappings(object siteId)
        {
            Debug.Assert(siteId != null && !string.IsNullOrEmpty(siteId.ToString()));
            var mappings = new Mappings();
            object[] mapping = GetList(MappingTable, TableNameColumn).ToArray();
            long current = 0;
            long total = mapping.Count();
            foreach (object table in mapping)
            {
                mappings.Add(table.ToString(),
                    GetMapping(string.Format("{0}{1}{2}", SiteTable, table, MappingTable),
                        string.Format("{0}{1}", table, IdColumn),
                        string.Format("{0}{1}{2}", SiteTable, table, IdColumn), siteId));
                if (ProgressCallback != null) ProgressCallback(++current, total);
            }
            if (CompliteCallback != null) CompliteCallback();
            return mappings;
        }

        /// <summary>
        ///     Сохранение из базу данных настроек сайта
        ///     Не используется
        /// </summary>
        public void SetSiteProperties(SiteProperties properties)
        {
            Connection.Open();
            using (SQLiteCommand command1 = Connection.CreateCommand())
            {
                command1.CommandText = string.Format("SELECT * FROM {0}", SiteTable);
                SQLiteDataReader reader = command1.ExecuteReader();
                using (SQLiteCommand command2 = Connection.CreateCommand())
                {
                    command2.CommandText = string.Format("INSERT OR REPLACE {0}({1}) VALUES (@{2})", SiteTable,
                        properties.Keys.Aggregate((i, j) => string.Format("{0},{1}", i, j)),
                        properties.Keys.Aggregate((i, j) => string.Format("{0},@{1}", i, j)));
                    long current = 0;
                    long total = reader.FieldCount;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        command2.Parameters.Add(new SQLiteParameter(string.Format("@{0}", reader.GetName(i)),
                            properties[reader.GetName(i)]));
                        if (ProgressCallback != null) ProgressCallback(++current, total);
                    }
                    reader.Close();
                    command2.ExecuteNonQuery();
                    Connection.Close();
                }
            }
            if (CompliteCallback != null) CompliteCallback();
        }

        /// <summary>
        ///     Сохранение из базу данных справочника для указанного сайта siteId
        ///     Не используется
        /// </summary>
        public void SetMapping(Mapping mapping, string tableName, string keyColumnName, string valueColumnName,
            object siteId)
        {
            Connection.Open();
            string insertOrReplaceString =
                string.Format("INSERT OR REPLACE {0}({3}{4},{1},{2}) VALUES (@{3}{4},@{1},@{2})", tableName,
                    keyColumnName, valueColumnName, SiteTable, IdColumn);
            long current = 0;
            long total = mapping.Count;
            foreach (var item in mapping)
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.CommandText = insertOrReplaceString;
                    command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn), siteId));
                    command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", keyColumnName), item.Key));
                    command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", valueColumnName),
                        item.Value));
                    command.ExecuteNonQuery();
                    if (ProgressCallback != null) ProgressCallback(++current, total);
                }
            Connection.Close();
            if (CompliteCallback != null) CompliteCallback();
        }

        /// <summary>
        ///     Загрузка из базы данных описай возвращаемых полей для указанного сайта
        ///     Используется только при загрузке свойств сайта
        /// </summary>
        public ReturnFieldInfos GetReturnFieldInfos(object siteId)
        {
            Debug.Assert(siteId != null && !string.IsNullOrEmpty(siteId.ToString()));
            var returnFieldInfos = new ReturnFieldInfos();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText =
                    string.Format("SELECT * FROM {0}{1}{2} JOIN {1} USING ({1}{3}) WHERE {0}{1}{2}.{0}{3}=@{0}{3}",
                        SiteTable, ReturnFieldTable, MappingTable, IdColumn);
                command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn), siteId));
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var info = new ReturnFieldInfo();
                    long current = 0;
                    long total = reader.FieldCount;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string key = reader.GetName(i);
                        object value = reader[key];
                        info.Add(key, value);
                        if (ProgressCallback != null) ProgressCallback(++current, total);
                    }
                    returnFieldInfos.Add(info);
                }
                Connection.Close();
            }
            if (CompliteCallback != null) CompliteCallback();
            return returnFieldInfos;
        }

        public BuilderInfos GetBuilderInfos(object siteId)
        {
            Debug.Assert(siteId != null && !string.IsNullOrEmpty(siteId.ToString()));
            var builderInfos = new BuilderInfos();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText =
                    string.Format("SELECT * FROM {0}{1}{2} JOIN {2} USING ({4}) WHERE {0}{1}{2}.{0}{3}=@{0}{3}",
                        SiteTable, BuilderTable, MappingTable, IdColumn, TableNameColumn);
                command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn), siteId));
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var info = new BuilderInfo();
                    long current = 0;
                    long total = reader.FieldCount;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string key = reader.GetName(i);
                        object value = reader[key];
                        info.Add(key, value);
                        if (ProgressCallback != null) ProgressCallback(++current, total);
                    }
                    builderInfos.Add(info);
                }
                Connection.Close();
            }
            if (CompliteCallback != null) CompliteCallback();
            return builderInfos;
        }

        #region

        private readonly Dictionary<Type[], string> _getListProfiles = new Dictionary<Type[], string>
        {
            {
                new[] {typeof (string)},
                string.Format(
                    "SELECT {{0}}{0} FROM {{0}}",
                    IdColumn)
            },
            {
                new[] {typeof (string), typeof (string)},
                string.Format(
                    "SELECT {{1}} FROM {{0}}")
            },
            {
                new[] {typeof (string), typeof (object)},
                string.Format(
                    "SELECT {{0}}{0} FROM {{0}} WHERE ({1}=@{1})",
                    IdColumn, ParentIdColumn)
            },
            {
                new[] {typeof (string), typeof (object), typeof (object)},
                string.Format(
                    "SELECT {0}{{0}}{1} FROM {0}{{0}} WHERE ({0}{1}=@{0}{1}) AND ({2}=@{2})",
                    SiteTable, IdColumn, ParentIdColumn)
            },
            {
                new[] {typeof (object), typeof (string), typeof (object)},
                string.Format(
                    "SELECT {0}{{1}}{2} FROM {0}{{1}}{1} WHERE ({{1}}{2}=@{2}) AND ({0}{2}=@{0}{2})",
                    SiteTable, MappingTable, IdColumn)
            },
            {
                new[] {typeof (string), typeof (long), typeof (long), typeof (object)},
                string.Format(
                    "SELECT {{0}}{0} FROM {{0}} WHERE ({1}=@{1}) AND ({2}>=@Min{2}) AND ({2}<=@Max{2})",
                    IdColumn, ParentIdColumn, LevelColumn)
            },
            {
                new[] {typeof (string), typeof (long), typeof (long), typeof (object), typeof (object)},
                string.Format(
                    "SELECT {0}{{0}}{1} FROM {0}{{0}} WHERE ({0}{1}=@{0}{1}) AND ({2}=@{2}) AND ({3}>=@Min{3}) AND ({3}<=@Max{3})",
                    SiteTable, IdColumn, ParentIdColumn, LevelColumn)
            },
        };

        /// <summary>
        ///     Загрузка из базы данных всех значений из указанной колонки указанной таблицы
        /// </summary>
        public IEnumerable<object> GetList(params object[] parameters)
        {
            Type[] types = parameters.Select(parameter => parameter.GetType()).ToArray();
            long current = 0;
            long total = 1;
            foreach (var pair in _getListProfiles.Where(pair => Types.Type.IsKindOf(types, pair.Key)))
            {
                var values = new List<object>();
                Connection.Open();
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.CommandText =
                        (string) _stringFormatMethodInfo.Invoke(null, new object[] {pair.Value, parameters});
                    Debug.WriteLine(command.CommandText);
                    switch (types.Length)
                    {
                        case 1:
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", ParentIdColumn),
                                parameters[parameters.Length - 1]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn),
                                parameters[parameters.Length - 1]));
                            break;
                        case 2:
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", ParentIdColumn),
                                parameters[parameters.Length - 1]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn),
                                parameters[parameters.Length - 1]));
                            break;
                        case 3:
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", IdColumn),
                                parameters[0]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", ParentIdColumn),
                                parameters[parameters.Length - 2]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn),
                                parameters[parameters.Length - 1]));
                            break;
                        case 4:
                            command.Parameters.Add(new SQLiteParameter(string.Format("@Min{0}", LevelColumn),
                                parameters[parameters.Length - 3]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@Max{0}", LevelColumn),
                                parameters[parameters.Length - 2]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", ParentIdColumn),
                                parameters[parameters.Length - 1]));
                            break;
                        case 5:
                            command.Parameters.Add(new SQLiteParameter(string.Format("@Min{0}", LevelColumn),
                                parameters[parameters.Length - 4]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@Max{0}", LevelColumn),
                                parameters[parameters.Length - 3]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", ParentIdColumn),
                                parameters[parameters.Length - 2]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn),
                                parameters[parameters.Length - 1]));
                            break;
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        object value = reader[0];
                        values.Add(value);
                        if (ProgressCallback != null) ProgressCallback(++current, ++total);
                    }
                    Connection.Close();
                }
                if (ProgressCallback != null) ProgressCallback(++current, total);
                if (CompliteCallback != null) CompliteCallback();
                return values;
            }
            throw new NotImplementedException();
        }

        #endregion

        #region

        private readonly Dictionary<Type[], string> _getMappingProfiles = new Dictionary<Type[], string>
        {
            {
                new[] {typeof (string)},
                string.Format("SELECT {{0}}{0},{{0}}{1} FROM {{0}}", IdColumn, TitleColumn)
            },
            {
                new[] {typeof (string), typeof (object)},
                string.Format("SELECT {0}{{0}}{1},{0}{{0}}{2} FROM {0}{{0}} WHERE {0}{1}=@{0}{1}", SiteTable,
                    IdColumn, TitleColumn)
            },
            {
                new[] {typeof (string), typeof (long), typeof (long)},
                string.Format("SELECT {{0}}{0},{{0}}{1} FROM {{0}} WHERE ({2}>=@Min{2}) AND ({2}<=@Max{2})",
                    IdColumn,
                    TitleColumn, LevelColumn)
            },
            {
                new[] {typeof (string), typeof (long), typeof (long), typeof (object)},
                string.Format(
                    "SELECT {0}{{0}}{1},{0}{{0}}{2} FROM {0}{{0}} WHERE ({0}{1}=@{0}{1}) AND ({3}>=@{0}Min{3}) AND ({3}<=@{0}Max{3})",
                    SiteTable,
                    IdColumn, TitleColumn, LevelColumn)
            },
            {
                new[] {typeof (string), typeof (string), typeof (string)},
                string.Format("SELECT {{1}},{{2}} FROM {{0}}")
            },
            {
                new[] {typeof (string), typeof (string), typeof (string), typeof (object)},
                string.Format("SELECT {{1}},{{2}} FROM {{0}} WHERE {0}{1}=@{0}{1}", SiteTable, IdColumn)
            },
            {
                new[] {typeof (string), typeof (string), typeof (string), typeof (long), typeof (long)},
                string.Format("SELECT {{1}},{{2}} FROM {{0}} WHERE ({0}>=@Min{0}) AND ({0}<=@Max{0})", LevelColumn)
            },
            {
                new[] {typeof (string), typeof (string), typeof (string), typeof (long), typeof (long), typeof (object)},
                string.Format(
                    "SELECT {{1}},{{2}} FROM {{0}} WHERE ({0}{1}=@{0}{1}) AND ({2}>=@{0}Min{2}) AND ({2}<=@{0}Max{2})",
                    SiteTable, IdColumn, LevelColumn)
            },
            {
                new[]
                {
                    typeof (string), typeof (long), typeof (long), typeof (long), typeof (long), typeof (object)
                },
                string.Format(
                    "SELECT {0}{{0}}{1}.{{0}}{2},{0}{{0}}{1}.{0}{{0}}{2} FROM {0}{{0}}{1} JOIN {{0}} USING ({{0}}{2}) JOIN {0}{{0}} USING ({0}{{0}}{2}) WHERE ({0}{{0}}{1}.{0}{2}=@{0}{2}) AND ({{0}}.{3}>=@Min{3}) AND ({{0}}.{3}<=@Max{3}) AND ({0}{{0}}.{3}>=@{0}Min{3}) AND ({0}{{0}}.{3}<=@{0}Max{3})",
                    SiteTable, MappingTable, IdColumn, LevelColumn)
            },
        };

        /// <summary>
        ///     Загрузка всех пар Ключ-Значение из таблицы базы данных для указанного siteId
        ///     Ключ в поле keyColumnName
        ///     Значение в поле valueColumnName
        /// </summary>
        public Mapping GetMapping(params object[] parameters)
        {
            Type[] types = parameters.Select(arg => arg.GetType()).ToArray();
            foreach (var pair in _getMappingProfiles.Where(pair => Types.Type.IsKindOf(types, pair.Key)))
            {
                var mapping = new Mapping();
                Connection.Open();
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.CommandText =
                        (string) _stringFormatMethodInfo.Invoke(null, new object[] {pair.Value, parameters});
                    Debug.WriteLine(command.CommandText);
                    switch (types.Length)
                    {
                        case 1:
                            break;
                        case 2:
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn),
                                parameters[parameters.Length - 1]));
                            break;
                        case 6:
                            command.Parameters.Add(new SQLiteParameter(string.Format("@Min{0}", LevelColumn),
                                parameters[parameters.Length - 5]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@Max{0}", LevelColumn),
                                parameters[parameters.Length - 4]));
                            command.Parameters.Add(
                                new SQLiteParameter(string.Format("@{0}Min{1}", SiteTable, LevelColumn),
                                    parameters[parameters.Length - 3]));
                            command.Parameters.Add(
                                new SQLiteParameter(string.Format("@{0}Max{1}", SiteTable, LevelColumn),
                                    parameters[parameters.Length - 2]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn),
                                parameters[parameters.Length - 1]));
                            break;
                        default:
                            command.Parameters.Add(new SQLiteParameter(string.Format("@Min{0}", LevelColumn),
                                parameters[parameters.Length - 2]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@Max{0}", LevelColumn),
                                parameters[parameters.Length - 1]));
                            command.Parameters.Add(
                                new SQLiteParameter(string.Format("@{0}Min{1}", SiteTable, LevelColumn),
                                    parameters[parameters.Length - 3]));
                            command.Parameters.Add(
                                new SQLiteParameter(string.Format("@{0}Max{1}", SiteTable, LevelColumn),
                                    parameters[parameters.Length - 2]));
                            command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn),
                                parameters[parameters.Length - 1]));
                            break;
                    }
                    Debug.WriteLine(command.CommandText);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        object key = reader[0];
                        object value = reader[1];
                        if (!mapping.ContainsKey(key)) mapping.Add(key, value);
                    }
                    Connection.Close();
                }
                if (CompliteCallback != null) CompliteCallback();
                return mapping;
            }
            throw new NotImplementedException();
        }

        #endregion

        #region

        private readonly string[] _getUserFieldsFormatStrings =
        {
            string.Format("SELECT * FROM {0}{{0}} WHERE {0}Id=@{0}{1} AND {0}{{0}}{1}=@{{0}}{1}", SiteTable,
                IdColumn),
            string.Format(
                "SELECT * FROM {0}{{0}}{1} WHERE {0}{2}=@{0}{2} AND {0}{{0}}{2}=@{{0}}{2} AND {{0}}{2}=@{2}",
                SiteTable, MappingTable, IdColumn),
            string.Format("SELECT * FROM {{0}} WHERE {{0}}{0}=@{0}", IdColumn)
        };

        public Collections.Properties GetUserFields(object id, object mappedId, string mappedTableName,
            object siteId)
        {
            Debug.Assert(id != null && !string.IsNullOrEmpty(id.ToString()));
            Debug.Assert(mappedId != null && !string.IsNullOrEmpty(mappedId.ToString()));
            Debug.Assert(mappedTableName != null && !string.IsNullOrEmpty(mappedTableName));
            Debug.Assert(siteId != null && !string.IsNullOrEmpty(siteId.ToString()));
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var userFields = new Collections.Properties();
            var internals = new List<string>
            {
                string.Format("{0}{1}", SiteTable, IdColumn),
                string.Format("{0}{1}", mappedTableName, IdColumn),
                string.Format("{0}{1}{2}", SiteTable, mappedTableName, IdColumn),
                ParentIdColumn,
                HasChildColumn,
                LevelColumn
            };
            Connection.Open();
            long current = 0;
            long total = _getUserFieldsFormatStrings.Length;
            foreach (string formatString in _getUserFieldsFormatStrings)
            {
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.CommandText = string.Format(formatString, mappedTableName);
                    command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn), siteId));
                    command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", mappedTableName, IdColumn),
                        mappedId));
                    command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", IdColumn), id));
                    Debug.WriteLine(command.CommandText);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        total += reader.FieldCount;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string key = reader.GetName(i);
                            object value = reader[i];
                            if (!internals.Contains(key) && !userFields.ContainsKey(key))
                            {
                                userFields.Add(key, value);
                                Debug.WriteLine("{0}->{1}", key, value);
                            }
                            if (ProgressCallback != null) ProgressCallback(++current, total);
                        }
                    }
                }
                if (ProgressCallback != null) ProgressCallback(++current, total);
            }
            Connection.Close();
            if (CompliteCallback != null) CompliteCallback();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return userFields;
        }

        #endregion

        #region

        private readonly Dictionary<Type[], string> _getScalarProfiles = new Dictionary<Type[], string>
        {
            {
                new[] {typeof (object), typeof (string)},
                string.Format("SELECT {{0}}{0} FROM {{0}} WHERE {{0}}{0}=@{0}", IdColumn)
            },
            {
                new[] {typeof (object), typeof (string), typeof (string)},
                string.Format("SELECT {{0}} FROM {{1}} WHERE {{1}}{0}=@{0}", IdColumn)
            },
            {
                new[] {typeof (object), typeof (string), typeof (string), typeof (string)},
                string.Format("SELECT {{0}} FROM {{2}} WHERE {{1}}=@{0}", IdColumn)
            },
            {
                new[] {typeof (object), typeof (string), typeof (object)},
                string.Format("SELECT {0}{{0}}{1} FROM {0}{{0}}{2} WHERE {{0}}{1}=@{1} AND {0}{1}=@{0}{1}",
                    SiteTable, IdColumn, MappingTable)
            },
            {
                new[] {typeof (object), typeof (string), typeof (string), typeof (object)},
                string.Format("SELECT {{0}} FROM {0}{{1}} WHERE {0}{{1}}{1}=@{1} AND {0}{1}=@{0}{1}", SiteTable,
                    IdColumn)
            }
        };

        /// <summary>
        ///     Выборка скалярного значения из колонки таблицы по ключу == id
        ///     Ключ в поле Название таблицы+"Id"
        ///     Значение в поле columnName
        /// </summary>
        public object GetScalar(params object[] parameters)
        {
            Type[] types = parameters.Select(arg => arg.GetType()).ToArray();
            foreach (var pair in _getScalarProfiles.Where(pair => Types.Type.IsKindOf(types, pair.Key)))
            {
                Connection.Open();
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.CommandText =
                        (string)
                            _stringFormatMethodInfo.Invoke(null,
                                new object[] {pair.Value, parameters.Where((val, idx) => idx != 0).ToArray()});
                    Debug.WriteLine(command.CommandText);
                    command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", IdColumn), parameters[0]));
                    command.Parameters.Add(new SQLiteParameter(string.Format("@{0}{1}", SiteTable, IdColumn),
                        parameters[parameters.Length - 1]));
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        object value = reader[0];
                        Connection.Close();
                        return value;
                    }
                    Connection.Close();
                    return null;
                }
            }
            throw new NotImplementedException();
        }

        #endregion
    }
}