using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using RealtyParser.Collections;

namespace RealtyParser
{
    /// <summary>
    ///     Класс для работы с базой данных
    /// </summary>
    public class Database
    {
        /// <summary>
        ///     Инициализация
        /// </summary>
        public Database()
        {
            //Assembly assembly = GetType().Assembly;
            //var resourceManager = new ResourceManager("Resources.Strings", assembly);
            //string connectionString = resourceManager.GetString("ConnectionString");
            const string connectionString = "data source=RealtyParser.db";
            Connection = new SQLiteConnection(connectionString);
        }


        /// <summary>
        ///     Коннектор к базе данных
        /// </summary>
        private SQLiteConnection Connection { get; set; }

        public static T ConvertTo<T>(object obj)
        {
            return (T) TypeDescriptor.GetConverter(obj).ConvertTo(obj, typeof (T));
        }

        /// <summary>
        ///     Загрузка всех пар Ключ-Значение из таблицы базы данных для указанного siteId
        ///     Ключ в поле keyColumnName
        ///     Значение в поле valueColumnName
        /// </summary>
        public Dictionary<object, object> GetDictionary(params object[] args)
        {
            Type[] argTypes = args.Select(arg => arg.GetType()).ToArray();
            var profiles = new Dictionary<Type[], string>
            {
                {
                    new[] {typeof (string)},
                    @"SELECT {0}Id,{0}Title FROM {0}"
                },
                {
                    new[] {typeof (string), typeof (string), typeof (string), typeof (object)},
                    @"SELECT {1},{2} FROM {0} WHERE SiteId=@SiteId"
                }
            };
            foreach (var pair in profiles.Where(pair => IsKindOf(argTypes, pair.Key)))
            {
                var dictionary = new Dictionary<object, object>();
                Connection.Open();
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.CommandText = (args.Length == 1)
                        ? System.String.Format(pair.Value, args[0])
                        : System.String.Format(pair.Value, args[0], args[1], args[2]);
                    command.Parameters.Add(new SQLiteParameter("@SiteId") {Value = args[args.Length - 1]});
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        object key = reader[0];
                        object value = reader[1];
                        if (!dictionary.ContainsKey(key)) dictionary.Add(key, value);
                    }
                    Connection.Close();
                }
                return dictionary;
            }
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetUserFields(object id, object mappedId, string mappedTableName,
            object siteId)
        {
            Debug.Assert(id != null && !System.String.IsNullOrEmpty(id.ToString()));
            Debug.Assert(mappedId != null && !System.String.IsNullOrEmpty(mappedId.ToString()));
            Debug.Assert(mappedTableName != null && !System.String.IsNullOrEmpty(mappedTableName));
            Debug.Assert(siteId != null && !System.String.IsNullOrEmpty(siteId.ToString()));
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var dictionary = new Dictionary<string, object>();
            var internals = new List<string>
            {
                "SiteId",
                System.String.Format("{0}Id", mappedTableName),
                System.String.Format("Site{0}Id", mappedTableName),
                "ParentId",
                "HasChild",
                "Level"
            };
            Connection.Open();
            foreach (string formatString in new[]
            {
                @"SELECT * FROM Site{0} WHERE SiteId=@SiteId AND Site{0}Id=@MappedId",
                @"SELECT * FROM Site{0}Mapping WHERE SiteId=@SiteId AND Site{0}Id=@MappedId AND {0}Id=@Id",
                @"SELECT * FROM {0} WHERE {0}Id=@Id"
            })
            {
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.CommandText = string.Format(formatString, mappedTableName);
                    command.Parameters.Add(new SQLiteParameter("@SiteId") {Value = siteId});
                    command.Parameters.Add(new SQLiteParameter("@MappedId") {Value = mappedId});
                    command.Parameters.Add(new SQLiteParameter("@Id") {Value = id});
                    Debug.WriteLine(command.CommandText);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string key = reader.GetName(i);
                            object value = reader[i];
                            if (!internals.Contains(key) && !dictionary.ContainsKey(key))
                            {
                                dictionary.Add(key, value);
                                Debug.WriteLine("{0}->{1}", key, value);
                            }
                        }
                    }
                }
            }
            Connection.Close();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            return dictionary;
        }


        /// <summary>
        ///     Выборка скалярного значения из колонки таблицы по ключу == id
        ///     Ключ в поле Название таблицы+"Id"
        ///     Значение в поле columnName
        /// </summary>
        public object GetScalar(params object[] args)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Type[] argTypes = args.Select(arg => arg.GetType()).ToArray();
            Debug.WriteLine("args " + System.String.Join(",", args.Select(item => item.ToString()).ToArray()));
            Debug.WriteLine("argTypes " + System.String.Join(",", argTypes.Select(item => item.Name).ToArray()));
            var profiles = new Dictionary<Type[], string>
            {
                {
                    new[] {typeof (object), typeof (string)},
                    @"SELECT {0}Id FROM {0} WHERE {0}Id=@Id"
                },
                {
                    new[] {typeof (object), typeof (string), typeof (string)},
                    @"SELECT {0} FROM {1} WHERE {1}Id=@Id"
                },
                {
                    new[] {typeof (object), typeof (string), typeof (object)},
                    @"SELECT Site{0}Id FROM Site{0}Mapping WHERE {0}Id=@Id AND SiteId=@SiteId"
                },
                {
                    new[] {typeof (object), typeof (string), typeof (string), typeof (object)},
                    @"SELECT {0} FROM Site{1} WHERE Site{1}Id=@Id AND SiteId=@SiteId"
                }
            };
            foreach (var pair in profiles.Where(pair => IsKindOf(argTypes, pair.Key)))
            {
                Connection.Open();
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.Parameters.Add(new SQLiteParameter("@Id") {Value = args[0]});
                    command.CommandText = (args.Length == 2)
                        ? System.String.Format(pair.Value, args[1])
                        : System.String.Format(pair.Value, args[1], args[2]);
                    command.Parameters.Add(new SQLiteParameter("@SiteId") {Value = args[args.Length - 1]});
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        object value = reader[0];
                        Connection.Close();
                        Debug.WriteLine("value {0}", value);
                        Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
                        return value;
                    }
                    Connection.Close();
                    Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
                    return null;
                }
            }
            throw new NotImplementedException();
        }

        private static bool IsKindOf(Type[] args, Type[] profiles)
        {
            if (args.Length == profiles.Length)
            {
                for (int i = 0; i < args.Length && i < profiles.Length; i++)
                    if (!profiles[i].IsAssignableFrom(args[i]))
                        return false;
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Загрузка из базы данных настроек указанного сайта
        /// </summary>
        public SiteProperties GetSiteProperties(object siteId)
        {
            Debug.Assert(siteId != null && !System.String.IsNullOrEmpty(siteId.ToString()));
            var properties = new SiteProperties();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Site WHERE SiteId=@SiteId";
                command.Parameters.Add(new SQLiteParameter("@SiteId") {Value = siteId});
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string key = reader.GetName(i);
                    object value = reader[key];
                    properties.Add(key, value);
                }
                Connection.Close();
            }
            return properties;
        }

        public Mapping GetMapping(object siteId)
        {
            Debug.Assert(siteId != null && !System.String.IsNullOrEmpty(siteId.ToString()));
            var mapping = new Mapping();
            foreach (object mappedTable in GetList("Mapping", "TableName"))
            {
                mapping.Add(mappedTable.ToString(),
                    GetDictionary(System.String.Format("Site{0}Mapping", mappedTable),
                        System.String.Format("{0}Id", mappedTable),
                        System.String.Format("Site{0}Id", mappedTable), siteId));
            }
            return mapping;
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
                command1.CommandText = @"SELECT * FROM Site";
                SQLiteDataReader reader = command1.ExecuteReader();
                using (SQLiteCommand command2 = Connection.CreateCommand())
                {
                    command2.CommandText = string.Format(@"INSERT OR REPLACE Site({0}) VALUES (@{1})",
                        properties.Keys.Aggregate((i, j) => System.String.Format("{0},{1}", i, j)),
                        properties.Keys.Aggregate((i, j) => System.String.Format("{0},@{1}", i, j)));
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        command2.Parameters.Add(new SQLiteParameter("@" + reader.GetName(i),
                            properties[reader.GetName(i)]));
                    }
                    reader.Close();
                    command2.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        /// <summary>
        ///     Сохранение из базу данных справочника для указанного сайта siteId
        ///     Не используется
        /// </summary>
        public void SetDictionary(Dictionary<object, object> dictionary, string tableName, string keyColumnName,
            string valueColumnName, object siteId)
        {
            Connection.Open();
            foreach (var item in dictionary)
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.CommandText =
                        string.Format(@"INSERT OR REPLACE {0}(SiteId,{1},{2}) VALUES (@SiteId,@{1},@{2})", tableName,
                            keyColumnName, valueColumnName);
                    command.Parameters.Add(new SQLiteParameter("@SiteId", siteId));
                    command.Parameters.Add(new SQLiteParameter(System.String.Format("@{0}", keyColumnName), item.Key));
                    command.Parameters.Add(new SQLiteParameter(System.String.Format("@{0}", valueColumnName),
                        item.Value));
                    command.ExecuteNonQuery();
                }
            Connection.Close();
        }

        /// <summary>
        ///     Загрузка из базы данных всех значений из указанной колонки указанной таблицы
        /// </summary>
        public IEnumerable<object> GetList(string tableName, string columnName)
        {
            var values = new List<object>();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = System.String.Format(@"SELECT * FROM {0}", tableName);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object value = reader[columnName];
                    values.Add(value);
                }
                Connection.Close();
            }
            return values;
        }

        /// <summary>
        ///     Генератор SQL-кода
        ///     Не входит в техническое задание
        /// </summary>
        public static string GenerateSql(long siteId, Dictionary<long, string> collection, string mappedTableName)
        {
            Debug.Assert(!System.String.IsNullOrEmpty(siteId.ToString()));
            Debug.Assert(collection.Any());
            var sb = new StringBuilder();
            sb.AppendLine(System.String.Format("CREATE TABLE IF NOT EXISTS Site{0}Mapping(", mappedTableName));
            sb.AppendLine("SiteId INTEGER,");
            sb.AppendLine(System.String.Format("{0}Id INTEGER,", mappedTableName));
            sb.AppendLine(System.String.Format("Site{0}Id VARCHAR,", mappedTableName));
            sb.AppendLine(System.String.Format("PRIMARY KEY(SiteId,{0}Id));", mappedTableName));
            foreach (var item in collection)
            {
                sb.AppendLine(
                    string.Format(
                        "INSERT OR REPLACE INTO Site{0}Mapping(SiteId,{0}Id,Site{0}Id) VALUES ({1},{2},'{3}');",
                        mappedTableName, siteId, item.Key, item.Value));
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Генератор SQL-кода
        ///     Не входит в техническое задание
        /// </summary>
        public static string GenerateSql(long siteId, Dictionary<string, string> collection, string mappedTableName)
        {
            Debug.Assert(siteId != null && !System.String.IsNullOrEmpty(siteId.ToString()));
            Debug.Assert(collection.Count > 0);
            var sb = new StringBuilder();
            sb.AppendLine(System.String.Format("CREATE TABLE IF NOT EXISTS Site{0}(", mappedTableName));
            sb.AppendLine("SiteId INTEGER,");
            sb.AppendLine(System.String.Format("Site{0}Id VARCHAR,", mappedTableName));
            sb.AppendLine(System.String.Format("Site{0}Title VARCHAR,", mappedTableName));
            sb.AppendLine(System.String.Format("PRIMARY KEY(SiteId,Site{0}Id));", mappedTableName));
            foreach (var item in collection)
            {
                sb.AppendLine(
                    string.Format(
                        "INSERT OR REPLACE INTO Site{0}(SiteId,Site{0}Id,Site{0}Title) VALUES ({1},'{2}','{3}');",
                        mappedTableName, siteId, item.Key, item.Value));
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Генератор SQL-кода
        ///     Не входит в техническое задание
        /// </summary>
        public static string GenerateSql(long siteId, HierarchicalItemCollection collection, string mappedTableName)
        {
            Debug.Assert(siteId != null && !System.String.IsNullOrEmpty(siteId.ToString()));
            Debug.Assert(collection.Any());
            var sb = new StringBuilder();
            sb.AppendLine(System.String.Format("CREATE TABLE IF NOT EXISTS Site{0}(", mappedTableName));
            sb.AppendLine("SiteId INTEGER,");
            sb.AppendLine(System.String.Format("Site{0}Id VARCHAR,", mappedTableName));
            sb.AppendLine(System.String.Format("Site{0}Title VARCHAR,", mappedTableName));
            sb.AppendLine("ParentId VARCHAR,");
            sb.AppendLine("Level INTEGER,");
            sb.AppendLine(System.String.Format("PRIMARY KEY(SiteId,Site{0}Id));", mappedTableName));
            foreach (HierarchicalItem item in collection.Values)
            {
                sb.AppendLine(
                    string.Format(
                        "INSERT OR REPLACE INTO Site{0}(SiteId,Site{0}Id,Site{0}Title,ParentId,Level) VALUES ({1},'{2}','{3}','{4}',{5});",
                        mappedTableName, siteId, item.Key, item.Value, item.ParentId, item.Level));
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Загрузка из базы данных описай возвращаемых полей для указанного сайта
        ///     Используется только при загрузке свойств сайта
        /// </summary>
        public ReturnFieldInfos GetReturnFieldInfos(object siteId)
        {
            Debug.Assert(siteId != null && !System.String.IsNullOrEmpty(siteId.ToString()));
            var returnFieldInfos = new ReturnFieldInfos();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT * FROM SiteReturnFieldMapping JOIN ReturnField USING (ReturnFieldId) WHERE SiteReturnFieldMapping.SiteId=@SiteId";
                command.Parameters.Add(new SQLiteParameter("@SiteId") {Value = siteId});
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var info = new ReturnFieldInfo();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string key = reader.GetName(i);
                        object value = reader[key];
                        info.Add(key, value);
                    }
                    returnFieldInfos.Add(info);
                }
                Connection.Close();
            }
            return returnFieldInfos;
        }
    }
}