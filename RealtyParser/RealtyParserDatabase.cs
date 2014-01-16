using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
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
                SQLiteParameter p = new SQLiteParameter("@SiteId", DbType.Int32) { Value = siteId };
                command.CommandText = @"SELECT * FROM " + tableName + " WHERE SiteId=@SiteId";
                command.Parameters.Add(p);
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

        public TR GetScalar<TR,T>(T id, string columnName, string tableName)
        {
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                SQLiteParameter p = new SQLiteParameter("@Id", (typeof(T) == typeof(string)) ? DbType.String : DbType.Int32) { Value = id };
                command.CommandText = @"SELECT " + columnName + " FROM " + tableName + " WHERE " + tableName + "Id=@Id";
                command.Parameters.Add(p);
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
                SQLiteParameter p1 = new SQLiteParameter("@SiteId", DbType.Int32) { Value = siteId };
                SQLiteParameter p2 = new SQLiteParameter("@Id", (typeof(T) == typeof(string)) ? DbType.String : DbType.Int32) { Value = id };
                command.CommandText = @"SELECT " + columnName + " FROM Site" + tableName + " WHERE Site" + tableName + "Id=@Id AND SiteId=@SiteId";
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
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
                SQLiteParameter p = new SQLiteParameter("@SiteId", DbType.Int32) { Value = siteId };
                command.CommandText = @"SELECT * FROM Site WHERE SiteId=@SiteId";
                command.Parameters.Add(p);
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
            foreach (var s in GetList<string>("Mapping", "TableName"))
                dictionary.Add(s, GetDictionary<long>("Site" + s + "Mapping", "" + s + "Id", "Site" + s + "Id", siteId));
            properties.Mapping = dictionary;

            return properties;
        }

        public int SaveSiteProperties(SiteProperties properties)
        {
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                StringBuilder fields = new StringBuilder();
                StringBuilder values = new StringBuilder();
                foreach (var property in properties)
                {
                    fields.Append("," + property.Key);
                    values.Append(",@" + property.Key);
                    SQLiteParameter p = new SQLiteParameter("@" + property.Key, DbType.String) { Value = property.Value };
                    command.Parameters.Add(p);
                }
                command.CommandText = @"REPLACE Site(" + fields.ToString().Substring(1) + ") VALUES (" + values.ToString().Substring(1) + ")";
                int retval = command.ExecuteNonQuery();
                Connection.Close();
                return retval;
            }
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
            Debug.Assert(collection.Count > 0);
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
            Debug.Assert(collection.Count > 0);
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
                SQLiteParameter p = new SQLiteParameter("@SiteId", DbType.Int32) { Value = siteId };
                command.Parameters.Add(p);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    returnFieldInfos.Add(new ReturnFieldInfo
                    {
                        SiteId = ConvertTo<string>(reader["SiteId"]),
                        ReturnFieldId = ConvertTo<string>(reader["ReturnFieldId"]),
                        UnoReturnFieldXpathTemplate = reader["UnoReturnFieldXpathTemplate"].ToString(),
                        UnoReturnFieldResultTemplate = reader["UnoReturnFieldResultTemplate"].ToString(),
                        UnoReturnFieldRegexPattern = reader["UnoReturnFieldRegexPattern"].ToString(),
                        UnoReturnFieldRegexReplacement = reader["UnoReturnFieldRegexReplacement"].ToString()
                    });
                }
                Connection.Close();
            }
            return returnFieldInfos;
        }
    }
}
