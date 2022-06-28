using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace DataAccessLayer
{
    public class Repository<T> : IRepository<T> where T : IEntityBase
    {

        protected SQLiteConnection connection;
        public Repository()
        {
            DbConnection();
        }

        private void DbConnection()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Database", "Shape_db.db").Replace(@"\\", @"\").Replace(@"\TwoDimensionShapeApp\", @"\").Replace(@"\bin\Debug\", @"\");
            connection = new SQLiteConnection($@"URI=file:{filePath}");
            connection.Open();
        }

        private string ConvertPathToUri(string path)
        {
            var uri = new Uri(path);
            return uri.ToString();
        }
        /// <summary>
        /// Save Entities Method
        /// </summary>
        /// <param name="query">sql INSERT Statement</param>
        /// <param name="values">Fields To Be Saved In key value pair</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Update Entities Method
        /// </summary>
        /// <param name="query">Update Query</param>
        /// <param name="values">Fields to Update In key value pair</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Delete Entities Method
        /// </summary>
        /// <param name="query">Delete Query</param>
        /// <param name="values">Fields To Be Delete in key value pair</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
