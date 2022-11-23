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
    public class DishRepositoryMS : IDishRepository
    {

        private readonly string _connectionString;

        public DishRepositoryMS(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Create(Dish entity)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO dishes VALUES (@name,@price,@recipe)";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NChar).Value = entity.Name;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = entity.Price;
                    cmd.Parameters.Add("@recipe", SqlDbType.NChar).Value = entity.Recipe;
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
                string query = $"DELETE FROM dishes WHERE _id = @id";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public Dish Get(int id)
        {
            Dish dish = new Dish();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM dishes WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dish.Id = (int)reader["_id"];
                            dish.Name = (string)reader["name"];
                            dish.Price = (decimal)reader["price"];
                            dish.Recipe = (string)reader["recipe"];
                        }

                    }
                }
            }
            return dish;
        }

        public IEnumerable<Dish> GetAll()
        {
            List<Dish> dishes = new List<Dish>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM dishes";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dishes.Add(new Dish
                            {
                                Id = (int)reader["_id"],
                                Name = (string)reader["name"],
                                Price = (decimal)reader["price"],
                                Recipe = (string)reader["recipe"]
                            });
                        }

                    }
                }
            }
            return dishes;
        }

        public bool Update(Dish entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"UPDATE dishes SET name = @name, price = @price, recipe = @recipe WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    cmd.Parameters.Add("@name", SqlDbType.NChar).Value = entity.Name;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = entity.Price;
                    cmd.Parameters.Add("@recipe", SqlDbType.NChar).Value = entity.Recipe;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
