using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Restaurant.DAL.Repositories.MsServerRepository
{
    public class OrderRepositoryMS : IOrderRepository
    {

        private readonly string _connectionString;

        public OrderRepositoryMS(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Create(Order entity)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO orders VALUES (@idPerson, @orderDate,@idStatus)";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@idPerson", SqlDbType.Int).Value = entity.User.Id;
                    cmd.Parameters.Add("@orderDate", SqlDbType.Date).Value = entity.OrderDate;
                    cmd.Parameters.Add("@idStatus", SqlDbType.Int).Value = entity.Status.Id;
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
                string query = $"DELETE FROM orders WHERE _id = @id";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public Order Get(int id)
        {
            Order order = new Order();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM orders WHERE _id = @id";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            order.Id = (int)reader["_id"];
                            order.OrderDate = (DateTime)reader["orderDate"];
                            order.Status = new StatusRepositoryMS(_connectionString).Get((int)reader["idStatus"]);
                            order.User = new UserRepositoryMS(_connectionString).Get((int)reader["idUser"]);
                        }

                    }
                }
            }
            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM ingredients";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                Id = (int)reader["_id"],
                                OrderDate = (DateTime)reader["orderDate"],
                                Status = new StatusRepositoryMS(_connectionString).Get((int)reader["idStatus"]),
                                User = new UserRepositoryMS(_connectionString).Get((int)reader["idUser"])
                            });
                        }

                    }
                }
            }
            return orders;
        }

        public bool Update(Order entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"UPDATE orders SET idPerson = @idPerson, orderDate = @price, idStatus = @idStatus WHERE _id = @id";
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    cmd.Parameters.Add("@idPerson", SqlDbType.Int).Value = entity.User.Id;
                    cmd.Parameters.Add("@orderDate", SqlDbType.Date).Value = entity.OrderDate;
                    cmd.Parameters.Add("@idStatus", SqlDbType.Int).Value = entity.Status.Id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
