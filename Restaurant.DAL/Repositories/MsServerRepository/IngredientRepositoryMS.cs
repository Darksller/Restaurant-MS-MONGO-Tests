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
    public class IngredientRepositoryMS : IIngredientRepository
    {

        private readonly string _connectionString;

        public IngredientRepositoryMS(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Create(Ingredient entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO ingredients VALUES (@name,@price)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NChar).Value = entity.Name;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = entity.Price;
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
                string query = $"DELETE FROM ingredients WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public Ingredient Get(int id)
        {
            Ingredient ingredient = new Ingredient();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM ingredients WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ingredient.Id = (int)reader["_id"];
                            ingredient.Name = (string)reader["name"];
                            ingredient.Price = (decimal)reader["price"];
                        }

                    }
                }
            }
            return ingredient;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM ingredients";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ingredients.Add(new Ingredient
                            {
                                Id = (int)reader["_id"],
                                Name = (string)reader["name"],
                                Price = (decimal)reader["price"]
                            });
                        }

                    }
                }
            }
            return ingredients;
        }

        public Ingredient GetIngredientByName(string name)
        {
            Ingredient ingredient = new Ingredient();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM ingredients WHERE name = @name";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NChar).Value = name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ingredient.Id = (int)reader["_id"];
                            ingredient.Name = (string)reader["name"];
                            ingredient.Price = (decimal)reader["price"];
                        }

                    }
                }
            }
            return ingredient;
        }

        public bool Update(Ingredient entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"UPDATE ingredients SET name = @name, price = @price WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    cmd.Parameters.Add("@name", SqlDbType.NChar).Value = entity.Name;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = entity.Price;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
