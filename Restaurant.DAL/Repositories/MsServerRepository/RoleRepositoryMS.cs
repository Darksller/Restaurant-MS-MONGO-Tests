using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories.MsServerRepository
{
    public class RoleRepositoryMS : IRoleRepository
    {

        private readonly string _connectionString;

        public RoleRepositoryMS(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Create(Role entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO roles VALUES (@name)";
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
                string query = $"DELETE FROM roles WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public Role Get(int id)
        {
            Role role = new Role();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM roles WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            role.Id = (int)reader["_id"];
                            role.Name = (string)reader["name"];
                        }

                    }
                }
            }
            return role;
        }

        public IEnumerable<Role> GetAll()
        {
            List<Role> roles = new List<Role>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM roles";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Role
                            {
                                Id = (int)reader["_id"],
                                Name = (string)reader["name"]
                            });
                        }

                    }
                }
            }
            return roles;
        }

        public bool Update(Role entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"UPDATE roles SET name = @name WHERE _id = @id";
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
