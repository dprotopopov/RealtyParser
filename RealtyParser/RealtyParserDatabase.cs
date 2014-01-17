using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RealtyParser
{
    public class RealtyParserDatabase
    {
        private const String ConnectionString = @"data source=RealtyParser.db";

        public RealtyParserDatabase()
        {
            Connection = new SQLiteConnection(ConnectionString);
        }

        protected T ConvertTo<T>(object obj)
        {
            return (T)TypeDescriptor.GetConverter(obj).ConvertTo(obj, typeof(T));
        }
        public SQLiteConnection Connection { get; set; }

        public Dictionary<T, string> GetDictionary<T>(string tableName)
        {
            Dictionary<T, string> dictionary = new Dictionary<T, string>();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM " + tableName + "";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    T key = ConvertTo<T>(reader["" + tableName + "Id"]);
                    string value = reader["" + tableName + "Title"].ToString();
                    if (!dictionary.ContainsKey(key)) dictionary.Add(key, value);
                }
                Connection.Close();
            }
            return dictionary;
        }
        public Dictionary<T, string> GetDictionary<T>(string tableName, string keyColumnName, string valueColumnName, long siteId)
        {
            Dictionary<T, string> dictionary = new Dictionary<T, string>();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM " + tableName + " WHERE SiteId=@SiteId";
                command.Parameters.Add(new SQLiteParameter("@SiteId", DbType.Int32) { Value = siteId });
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    T key = ConvertTo<T>(reader[keyColumnName]);
                    string value = reader[valueColumnName].ToString();
                    if (!dictionary.ContainsKey(key)) dictionary.Add(key, value);
                }
                Connection.Close();
            }
            return dictionary;
        }

        public TR GetScalar<TR, T>(T id, string columnName, string tableName)
        {
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT " + columnName + " FROM " + tableName + " WHERE " + tableName + "Id=@Id";
                command.Parameters.Add(new SQLiteParameter("@Id", (typeof(T) == typeof(string)) ? DbType.String : DbType.Int32) { Value = id });
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TR value = ConvertTo<TR>(reader[columnName]);
                    Connection.Close();
                    return value;
                }
                Connection.Close();
                return ConvertTo<TR>(0);
            }
        }
        public TR GetScalar<TR, T>(T id, string columnName, string tableName, long siteId)
        {
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT " + columnName + " FROM Site" + tableName + " WHERE Site" + tableName + "Id=@Id AND SiteId=@SiteId";
                command.Parameters.Add(new SQLiteParameter("@SiteId", DbType.Int32) { Value = siteId });
                command.Parameters.Add(new SQLiteParameter("@Id", (typeof(T) == typeof(string)) ? DbType.String : DbType.Int32) { Value = id });
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TR value = ConvertTo<TR>(reader[columnName]);
                    Connection.Close();
                    return value;
                }
                Connection.Close();
                return ConvertTo<TR>(0);
            }
        }
        public SiteProperties GetSiteProperties(long siteId)
        {
            SiteProperties properties = new SiteProperties();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Site WHERE SiteId=@SiteId";
                command.Parameters.Add(new SQLiteParameter("@SiteId", DbType.Int32) { Value = siteId });
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string key = reader.GetName(i);
                    string value = reader[key].ToString();
                    properties.Add(key, value);
                }
                Connection.Close();
            }
            properties.ReturnFieldInfos = GetReturnFieldInfos(siteId);
            Mapping dictionary = new Mapping();
            foreach (var mappedTable in GetList<string>("Mapping", "TableName"))
                dictionary.Add(mappedTable, GetDictionary<long>("Site" + mappedTable + "Mapping", "" + mappedTable + "Id", "Site" + mappedTable + "Id", siteId));
            properties.Mapping = dictionary;

            return properties;
        }

        public void SetSiteProperties(SiteProperties properties)
        {
            Connection.Open();
            using (SQLiteCommand command1 = Connection.CreateCommand())
            {
                command1.CommandText = @"SELECT * FROM Site";
                SQLiteDataReader reader = command1.ExecuteReader();
                using (SQLiteCommand command2 = Connection.CreateCommand())
                {
                    command2.CommandText = @"INSERT OR REPLACE Site(" + properties.Keys.Aggregate((i, j) => i + "," + j) +
                                          ") VALUES (@" + properties.Keys.Aggregate((i, j) => i + ",@" + j) + ")";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        command2.Parameters.Add(new SQLiteParameter("@" + reader.GetName(i), properties[reader.GetName(i)]));
                    }
                    reader.Close();
                    command2.ExecuteNonQuery();
                    Connection.Close();
                }
            }
            Mapping dictionary = properties.Mapping;
            foreach (var item in dictionary)
                SetDictionary<long>(item.Value, "Site" + item.Key + "Mapping", "" + item.Key + "Id", "Site" + item.Key + "Id", Convert.ToInt64(properties.SiteId));
        }
        public void SetDictionary<T>(Dictionary<T, string> dictionary, string tableName, string keyColumnName, string valueColumnName, long siteId)
        {
            Connection.Open();
            foreach (var item in dictionary)
                using (SQLiteCommand command = Connection.CreateCommand())
                {
                    command.CommandText = @"INSERT OR REPLACE " + tableName + "(SiteId," + keyColumnName + "," + valueColumnName + ") VALUES (@SiteId,@" + keyColumnName + ",@" + valueColumnName + ")";
                    command.Parameters.Add(new SQLiteParameter("@SiteId", siteId));
                    command.Parameters.Add(new SQLiteParameter("@" + keyColumnName + "", item.Key));
                    command.Parameters.Add(new SQLiteParameter("@" + valueColumnName + "", item.Value));
                    command.ExecuteNonQuery();
                }
            Connection.Close();
        }
        public List<T> GetList<T>(string tableName, string columnName)
        {
            List<T> values = new List<T>();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM " + tableName + "";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    T value = ConvertTo<T>(reader[columnName]);
                    values.Add(value);
                }
                Connection.Close();
            }
            return values;
        }
        public static string GenerateSql(long siteId, Dictionary<long, string> collection, string mappedTableName)
        {
            Debug.Assert(collection.Any());
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CREATE TABLE IF NOT EXISTS Site" + mappedTableName + "Mapping(");
            sb.AppendLine("SiteId INTEGER,");
            sb.AppendLine("" + mappedTableName + "Id INTEGER,");
            sb.AppendLine("Site" + mappedTableName + "Id VARCHAR,");
            sb.AppendLine("PRIMARY KEY(SiteId," + mappedTableName + "Id));");
            foreach (var item in collection)
            {
                sb.AppendLine("INSERT OR REPLACE INTO Site" + mappedTableName + "Mapping(SiteId," + mappedTableName + "Id,Site" + mappedTableName + "Id) VALUES (" + siteId + "," + item.Key + ",'" + item.Value + "');");
            }
            return sb.ToString();
        }
        public static string GenerateSql(long siteId, Dictionary<string, string> collection, string mappedTableName)
        {
            Debug.Assert(collection.Count > 0);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CREATE TABLE IF NOT EXISTS Site" + mappedTableName + "(");
            sb.AppendLine("SiteId INTEGER,");
            sb.AppendLine("Site" + mappedTableName + "Id VARCHAR,");
            sb.AppendLine("Site" + mappedTableName + "Title VARCHAR,");
            sb.AppendLine("PRIMARY KEY(SiteId,Site" + mappedTableName + "Id));");
            foreach (var item in collection)
            {
                sb.AppendLine("INSERT OR REPLACE INTO Site" + mappedTableName + "(SiteId,Site" + mappedTableName + "Id,Site" + mappedTableName + "Title) VALUES (" + siteId + ",'" + item.Key + "','" + item.Value + "');");
            }
            return sb.ToString();
        }
        public static string GenerateSql(long siteId, HierarhialItemCollection collection, string mappedTableName)
        {
            Debug.Assert(collection.Any());
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CREATE TABLE IF NOT EXISTS Site" + mappedTableName + "(");
            sb.AppendLine("SiteId INTEGER,");
            sb.AppendLine("Site" + mappedTableName + "Id VARCHAR,");
            sb.AppendLine("Site" + mappedTableName + "Title VARCHAR,");
            sb.AppendLine("ParentId VARCHAR,");
            sb.AppendLine("Level INTEGER,");
            sb.AppendLine("PRIMARY KEY(SiteId,Site" + mappedTableName + "Id));");
            foreach (var item in collection.Values)
            {
                sb.AppendLine("INSERT OR REPLACE INTO Site" + mappedTableName + "(SiteId,Site" + mappedTableName + "Id,Site" + mappedTableName + "Title,ParentId,Level) VALUES (" + siteId + ",'" + item.Key + "','" + item.Value + "','" + item.ParentId + "'," + item.Level + ");");
            }
            return sb.ToString();
        }

        public List<ReturnFieldInfo> GetReturnFieldInfos(long siteId)
        {
            List<ReturnFieldInfo> returnFieldInfos = new List<ReturnFieldInfo>();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM SiteReturnFieldMapping JOIN ReturnField USING (ReturnFieldId) WHERE SiteReturnFieldMapping.SiteId=@SiteId";
                command.Parameters.Add(new SQLiteParameter("@SiteId", DbType.Int32) { Value = siteId });
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReturnFieldInfo info = new ReturnFieldInfo();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string key = reader.GetName(i);
                        string value = reader[key].ToString();
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
