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
    internal class OrderRepositoryMS : IOrderRepository
    {

        private readonly string _connectionString;

        public OrderRepositoryMS(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Create(Order entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO orders VALUES (@idPerson, @orderDate,@idStatus)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@idPerson", SqlDbType.Int).Value = entity.CustomerId;
                    cmd.Parameters.Add("@orderDate", SqlDbType.Date).Value = entity.OrderDate;
                    cmd.Parameters.Add("@idStatus", SqlDbType.Int).Value = entity.Status.Id;
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
                string query = $"DELETE FROM orders WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public Order Get(int id) // ССПРОСИТЬ МОЖНА ЛИ ЗАПРОСЫВ ЗАПРОСАХ 
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Order entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = $"UPDATE orders SET idPerson = @idPerson, orderDate = @price, idStatus = @idStatus WHERE _id = @id";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    cmd.Parameters.Add("@idPerson", SqlDbType.Int).Value = entity.CustomerId;
                    cmd.Parameters.Add("@orderDate", SqlDbType.Date).Value = entity.OrderDate;
                    cmd.Parameters.Add("@idStatus", SqlDbType.Int).Value = entity.Status.Id;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
