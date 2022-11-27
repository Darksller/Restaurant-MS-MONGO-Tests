using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Restaurant.DAL.Repositories.MsServerRepository
{
    public class DishIngredientRepositoryMS : IDishIngredientRepository
    {
        private readonly string _connectionString;

        public DishIngredientRepositoryMS(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Create(DishIngredient entity)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO dishIngredients (idIngredient,idDish,count,price) VALUES (@idIngredient,@idDish,@count,@price)";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@idIngredient", SqlDbType.Int).Value = entity.idIngredient;
                    cmd.Parameters.Add("@idDish", SqlDbType.Int).Value = entity.idDish;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = entity.Price;
                    cmd.Parameters.Add("@count", SqlDbType.Int).Value = entity.Count;
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
                string query = $"DELETE FROM dishIngredients WHERE _id = @id";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public DishIngredient Get(int id)
        {
            var dishIngredient = new DishIngredient();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM dishIngredients WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dishIngredient.Id = (int)reader["_id"];
                            dishIngredient.idDish = (int)reader["idDish"];
                            dishIngredient.idIngredient = (int)reader["idIngredient"];
                            dishIngredient.Count = (int)reader["count"];
                            dishIngredient.Price = (int)reader["price"];
                        }

                    }
                }
            }
            return dishIngredient;
        }

        public IEnumerable<DishIngredient> GetAll()
        {
            var dishIngredients = new List<DishIngredient>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM dishIngredients";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dishIngredients.Add(new DishIngredient
                            {
                                Id = (int)reader["_id"],
                                idDish = (int)reader["idDish"],
                                idIngredient = (int)reader["idIngredient"],
                                Count = (int)reader["count"],
                                Price = (int)reader["price"]
                            });
                        }

                    }
                }
            }
            return dishIngredients;
        }

        public bool Update(DishIngredient entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"UPDATE dishIngredients SET idIngredient = @idIngredient, idDish = @idDish WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@idIngredient", SqlDbType.Int).Value = entity.idIngredient;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    cmd.Parameters.Add("@idDish", SqlDbType.Int).Value = entity.idDish;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = entity.Price;
                    cmd.Parameters.Add("@count", SqlDbType.Int).Value = entity.Count;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
