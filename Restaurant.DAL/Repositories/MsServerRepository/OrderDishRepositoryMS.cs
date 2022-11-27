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
    public class OrderDishRepositoryMS : IOrderDishRepository
    {
        private readonly string _connectionString;

        public OrderDishRepositoryMS(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Create(OrderDish entity)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO orderList (idOrder,idDish,count,price) VALUES (@idOrder,@idDish,@count,@price)";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@idOrder", SqlDbType.Int).Value = entity.idOrder;
                    cmd.Parameters.Add("@idDish", SqlDbType.Int).Value = entity.Price;
                    cmd.Parameters.Add("@count", SqlDbType.Int).Value = entity.Count;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = entity.Price;
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
                string query = $"DELETE FROM orderList WHERE _id = @id";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public OrderDish Get(int id)
        {
            var orderDish = new OrderDish();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM orderList WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            orderDish.Id = (int)reader["_id"];
                            orderDish.idDish = (int)reader["idDish"];
                            orderDish.idOrder = (int)reader["idOrder"];
                            orderDish.Price = (decimal)reader["price"];
                            orderDish.Count = (int)reader["count"];
                        }

                    }
                }
            }
            return orderDish;
        }

        public IEnumerable<OrderDish> GetAll()
        {
            var orderList = new List<OrderDish>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM orderList";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderList.Add(new OrderDish
                            {
                                Id = (int)reader["_id"],
                                idDish = (int)reader["idDish"],
                                idOrder = (int)reader["idOrder"],
                                Price = (decimal)reader["price"],
                                Count = (int)reader["count"],
                            });
                        }

                    }
                }
            }
            return orderList;
        }

        public bool Update(OrderDish entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"UPDATE orderList SET idOrder = @idOrder, idDish = @idDish, count = @count, price = @price WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@idOrder", SqlDbType.Int).Value = entity.idOrder;
                    cmd.Parameters.Add("@idDish", SqlDbType.Int).Value = entity.Price;
                    cmd.Parameters.Add("@count", SqlDbType.Int).Value = entity.Count;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = entity.Price;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
