using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class Repository<T> : IRepository<T> where T : IEntityBase
    {

        protected SqlConnection connection;
        readonly string connectionStr = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wori Hearty Alapher\shapeFileDB.mdf;Integrated Security=True";
        public Repository()
        {
            //string connectionStr = Path.GetFullPath("Shape_db.accdb");
            connection = new SqlConnection(connectionStr);
            //connection.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={connectionStr}";
            connection.Open();

        }

        public bool Add(string query, Dictionary<string, object> values)
        {
            bool Status = false;
            try
            {
                using (var cmd = new SqlCommand(query, connection))
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
                using (var cmd = new SqlCommand(query, connection))
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
        public bool Delete(T shapeId)
        {
            bool Status = false;
            try
            {
                using (var reader = new SqlDataReader())
                {

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
    }
}
