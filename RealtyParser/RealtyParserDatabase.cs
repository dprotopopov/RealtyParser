using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

        public SQLiteConnection Connection { get; set; }

        public Dictionary<long, string> GetTable(string tableName)
        {
            Dictionary<long, string> table = new Dictionary<long, string>();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM " + tableName + "";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    long key = (long)reader["" + tableName + "Id"];
                    string value = (string)reader["" + tableName + "Title"];
                    table.Add(key, value);
                }
                Connection.Close();
            }
            return table;
        }

        public string Mapping(long siteId, long tableId, string tableName)
        {
            string mappingId = null;
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                SQLiteParameter p1 = new SQLiteParameter("@SiteId", System.Data.DbType.Int32) { Value = siteId };
                SQLiteParameter p2 = new SQLiteParameter("@tableId", System.Data.DbType.Int32) { Value = tableId };
                command.CommandText = @"SELECT * FROM Site" + tableName + @"Mapping WHERE SiteId=@SiteId AND " + tableName + @"Id=@tableId";
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mappingId = (string)reader["Site" + tableName + "Id"];
                }
                Connection.Close();
            }
            return mappingId;
        }

        public SiteProperties GetProperties(long siteId)
        {
            SiteProperties properties = new SiteProperties();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                SQLiteParameter p = new SQLiteParameter("@SiteId", System.Data.DbType.Int32) { Value = siteId };
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
            return properties;
        }

        public int SaveProperties(SiteProperties properties)
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
                    SQLiteParameter p = new SQLiteParameter("@" + property.Key, System.Data.DbType.String) { Value = property.Value };
                    command.Parameters.Add(p);
                }
                command.CommandText = @"REPLACE Site(" + fields.ToString().Substring(1) + ") VALUES (" + values.ToString().Substring(1) + ")";
                int retval= command.ExecuteNonQuery();
                Connection.Close();
                return retval;
            }
        }
        public List<string> GetEnum(string tableName, string columnName)
        {
            List<string> values = new List<string>();
            Connection.Open();
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM " + tableName + "";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string value = (string)reader[columnName];
                    values.Add(value);
                }
                Connection.Close();
            }
            return values;
        }
    }
}
