using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace DataAccessLayer
{
    public class Repository<T> : IRepository<T> where T : IEntityBase
    {



        protected SQLiteConnection connection;
        readonly string connectionStr = @"URI=file:C:\Users\alapher.woriayibapri\Desktop\SEPAL Projects\alahearty\DragAndDropTwoDimensionalShapes\Database\Shape_db.db";
        public Repository()
        {
            //string connectionStr = Path.GetFullPath("Shape_db.accdb");
            connection = new SQLiteConnection(connectionStr);
            //connection.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={connectionStr}";
            connection.Open();

        }

        public bool Add(string query, Dictionary<string, object> values)
        {
            bool Status = false;
            try
            {
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    foreach (var value in values)
                    {
                        cmd.Parameters.AddWithValue($"@{value.Key}", value.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
                Status = true;
            }
            catch (Exception ex)
            {
                Status = false;
                throw new Exception("Repository.Add :  {0}", ex);
            }
            return Status;
        }

        public bool Update(string query, Dictionary<string, object> values)
        {
            bool Status = false;
            try
            {
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    foreach (var value in values)
                    {
                        cmd.Parameters.AddWithValue($"@{value.Key}", value.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
                Status = true;
            }
            catch (Exception ex)
            {
                Status = false;
                throw new Exception("Repository.Updated {0}", ex);
            }
            return Status;
        }
        public bool Delete(string query, Dictionary<string, object> values)
        {
            bool Status = false;
            try
            {
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    foreach (var value in values)
                    {
                        cmd.Parameters.AddWithValue($"@{value.Key}", value.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
                Status = true;
            }
            catch (Exception ex)
            {
                Status = false;
                throw new Exception("Repository.Delete {0}", ex);
            }
            return Status;
        }
    }
}
