using MongoDB.Driver;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories.MongoDBRepository
{
    public class UserRepositoryMO : IUserRepository
    {
        private IMongoCollection<User> mongoCollection;

        public UserRepositoryMO(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDataBase = mongoClient.GetDatabase("res");
            mongoCollection = mongoDataBase.GetCollection<User>("users");
        }
        public bool Create(User entity)
        {
            var obj = mongoCollection.Find("{}").Sort("{_id:-1}").Limit(1).ToList();
            if (obj.Count == 0) entity.Id = 1;
            else entity.Id = obj[0].Id + 1;
            mongoCollection.InsertOne(entity);
            return true;
        }

        public bool Delete(int id)
        {
            mongoCollection.DeleteOne("{_id:" + id + "}");
            return true;
        }

        public User Get(int id)
        {
            var obj = mongoCollection.Find("{_id:" + id + "}").FirstOrDefault();
            return obj;
        }

        public IEnumerable<User> GetAll()
        {
            var objs = mongoCollection.Find("{}").ToList();
            return objs;
        }

        public bool Update(User entity)
        {
            var filter = Builders<User>.Filter.Eq("_id", entity.Id);
            var update = Builders<User>.Update.Set("Name", entity.Name);

            mongoCollection.UpdateOne(filter, update);

            return true;
        }
    }
}
