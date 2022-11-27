using MongoDB.Bson;
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
    public class StatusRepositoryMO : IStatusRepository
    {
        private IMongoCollection<Status> mongoCollection;
        public StatusRepositoryMO(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDataBase = mongoClient.GetDatabase("res");
            mongoCollection = mongoDataBase.GetCollection<Status>("statuses");
        }
        public bool Create(Status entity)
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

        public Status Get(int id)
        {
            var status = mongoCollection.Find("{_id:" + id + "}").FirstOrDefault();
            return status;
        }

        public IEnumerable<Status> GetAll()
        {
            var statuses = mongoCollection.Find("{}").ToList();
            return statuses;
        }

        public bool Update(Status entity)
        {
            var filter = Builders<Status>.Filter.Eq("_id", entity.Id);
            var update = Builders<Status>.Update.Set("Name", entity.Name);

            mongoCollection.UpdateOne(filter, update);

            return true;
        }
    }
}
