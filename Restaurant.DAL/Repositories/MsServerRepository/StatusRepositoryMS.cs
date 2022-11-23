using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories.MsServerRepository
{
    public class StatusRepositoryMS : IStatusRepository
    {
        private readonly string _connectionString;
        public StatusRepositoryMS(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Create(Status entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO statuses VALUES (@name)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NChar).Value = entity.Name;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public bool Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"DELETE FROM statuses WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public Status Get(int id)
        {
            Status status = new Status();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM statuses WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            status.Id = (int)reader["_id"];
                            status.Name = (string)reader["name"];
                        }

                    }
                }
            }
            return status;
        }

        public IEnumerable<Status> GetAll()
        {
            List<Status> statuses = new List<Status>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM statuses";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statuses.Add(new Status
                            {
                                Id = (int)reader["_id"],
                                Name = (string)reader["name"]
                            });
                        }

                    }
                }
            }
            return statuses;
        }

        public bool Update(Status entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"UPDATE statuses SET name=@name WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    cmd.Parameters.Add("@name", SqlDbType.NChar).Value = entity.Name;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
