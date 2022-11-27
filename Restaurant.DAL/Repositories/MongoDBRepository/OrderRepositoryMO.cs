using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories.MongoDBRepository
{
    public class OrderRepositoryMO : IOrderRepository
    {
        private IMongoCollection<BsonDocument> mongoCollection;

        public OrderRepositoryMO(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDataBase = mongoClient.GetDatabase("res");
            mongoCollection = mongoDataBase.GetCollection<BsonDocument>("orders");
        }

        public bool Create(Order entity)
        {
            var obj = mongoCollection.Find("{}").Sort("{_id:-1}").Limit(1).ToList();
            if (obj.Count == 0) entity.Id = 1;
            else entity.Id = obj[0].GetValue("_id").ToInt32() + 1;

            var document = new BsonDocument
            {
                { "_id", entity.Id },
                {"OrderDate",entity.OrderDate },
                {"Status", new BsonDocument()
                    {
                        {"_id", entity.Status.Id },
                        {"Name",entity.Status.Name }
                    }},
                {"User", new BsonDocument()
                        {
                        {"_id", entity.User.Id },
                        {"Name",entity.User.Name },
                        {"PhoneNumber", entity.User.PhoneNumber},
                        {"Address",entity.User.Address },
                        {"Login", entity.User.Login},
                        {"Password", entity.User.Password},
                        {"Role", new BsonDocument()
                            {
                                {"_id", entity.User.Role.Id },
                                {"Name",entity.User.Role.Name } } },
                            }}

            };

            mongoCollection.InsertOne(document);
            return true;
        }

        public bool Delete(int id)
        {
            mongoCollection.DeleteOne("{_id:" + id + "}");
            return true;
        }

        public Order Get(int id)
        {
            var obj = mongoCollection.Find("{_id:" + id + "}").FirstOrDefault();
            return new Order()
            {
                Id = obj.GetValue("_id").ToInt32(),
                OrderDate = obj.GetValue("OrderDate").ToLocalTime(),
                Status = new Status
                {
                    Id = obj.GetValue("Status")["_id"].ToInt32(),
                    Name = obj.GetValue("Status")["Name"].ToString()
                },
                User = new User
                {
                    Id = obj.GetValue("User")["_id"].ToInt32(),
                    Name = obj.GetValue("User")["Name"].ToString(),
                    Address = obj.GetValue("User")["Address"].ToString(),
                    Login = obj.GetValue("User")["Login"].ToString(),
                    Password = obj.GetValue("User")["Password"].ToString(),
                    PhoneNumber = obj.GetValue("User")["PhoneNumber"].ToInt32(),
                    Role = new Role
                    {
                        Id = obj.GetValue("User")["Role"]["_id"].ToInt32(),
                        Name = obj.GetValue("User")["Role"]["Name"].ToString(),
                    }
                }
            };
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = new List<Order>();

            var exOrders = mongoCollection.Find("{}").ToList();

            foreach (BsonDocument obj in exOrders)
            {
                orders.Add(new Order()
                {
                    Id = obj.GetValue("_id").ToInt32(),
                    OrderDate = obj.GetValue("OrderDate").ToLocalTime(),
                    Status = new Status
                    {
                        Id = obj.GetValue("Status")["_id"].ToInt32(),
                        Name = obj.GetValue("Status")["Name"].ToString()
                    },
                    User = new User
                    {
                        Id = obj.GetValue("User")["_id"].ToInt32(),
                        Name = obj.GetValue("User")["Name"].ToString(),
                        Address = obj.GetValue("User")["Address"].ToString(),
                        Login = obj.GetValue("User")["Login"].ToString(),
                        Password = obj.GetValue("User")["Password"].ToString(),
                        PhoneNumber = obj.GetValue("User")["PhoneNumber"].ToInt32(),
                        Role = new Role
                        {
                            Id = obj.GetValue("User")["Role"]["_id"].ToInt32(),
                            Name = obj.GetValue("User")["Role"]["Name"].ToString(),
                        }
                    }
                });
            }
            return orders;
        }

        public bool Update(Order entity)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", entity.Id);
            var update = Builders<BsonDocument>.Update.Set("OrderDate", entity.OrderDate).Set("Status", entity.Status).Set("User", entity.User);

            mongoCollection.UpdateOne(filter, update);

            return true;
        }
    }
}
