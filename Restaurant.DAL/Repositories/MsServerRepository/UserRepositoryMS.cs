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
    public class UserRepositoryMS : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepositoryMS(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Create(User entity)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO users VALUES (@name,@phoneNumber,@address,@idRole,@login,@password)";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NChar).Value = entity.Name;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.Int).Value = entity.PhoneNumber;
                    cmd.Parameters.Add("@address", SqlDbType.NChar).Value = entity.Address;
                    cmd.Parameters.Add("@idRole", SqlDbType.Int).Value = entity.Role.Id;
                    cmd.Parameters.Add("@login", SqlDbType.NChar).Value = entity.Login;
                    cmd.Parameters.Add("@password", SqlDbType.NChar).Value = entity.Password;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public bool Delete(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"DELETE FROM users WHERE _id = @id";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public User Get(int id)
        {
            User user = new User();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM users WHERE _id = @id";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.Id = (int)reader["_id"];
                            user.Name = (string)reader["name"];
                            user.PhoneNumber = (int)reader["phoneNumber"];
                            user.Address = (string)reader["address"];
                            user.Role = new RoleRepositoryMS(_connectionString).Get((int)reader["idRole"]);
                            user.Login = (string)reader["login"];
                            user.Password = (string)reader["password"];
                        }

                    }
                }
            }
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM users";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = (int)reader["_id"],
                                Name = (string)reader["name"],
                                PhoneNumber = (int)reader["phoneNumber"],
                                Address = (string)reader["address"],
                                Role = new RoleRepositoryMS(_connectionString).Get((int)reader["idRole"]),
                                Login = (string)reader["login"],
                                Password = (string)reader["password"]
                            });
                        }

                    }
                }
            }
            return users;
        }

        public bool Update(User entity)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"UPDATE users SET name = @name, phoneNumber = @phoneNumber, address = @address, idRole = @idRole, login = @login," +
                    $" password = @password WHERE _id = @id";

                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NChar).Value = entity.Name;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.Int).Value = entity.PhoneNumber;
                    cmd.Parameters.Add("@address", SqlDbType.NChar).Value = entity.Address;
                    cmd.Parameters.Add("@idRole", SqlDbType.Int).Value = entity.Role.Id;
                    cmd.Parameters.Add("@login", SqlDbType.NChar).Value = entity.Login;
                    cmd.Parameters.Add("@password", SqlDbType.NChar).Value = entity.Password;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
