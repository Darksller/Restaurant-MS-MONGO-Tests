using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Restaurant.DAL.Repositories.MongoDBRepository
{
    public class DishRepositoryMO : IDishRepository
    {
        private IMongoCollection<BsonDocument> mongoCollection;
        public DishRepositoryMO(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDataBase = mongoClient.GetDatabase("res");
            mongoCollection = mongoDataBase.GetCollection<BsonDocument>("dishes");
        }
        public bool Create(Dish entity)
        {
            var obj = mongoCollection.Find("{}").Sort("{_id:-1}").Limit(1).ToList();
            if (obj.Count == 0) entity.Id = 1;
            else entity.Id = obj[0].GetValue("_id").ToInt32() + 1;

            var document = new BsonDocument
            {
                { "_id", entity.Id },
                {"Name",entity.Name },
                {"Price",entity.Price },
                {"Recipe",entity.Recipe }
            };

            mongoCollection.InsertOne(document);
            return true;
        }

        public bool Delete(int id)
        {
            mongoCollection.DeleteOne("{_id:" + id + "}");
            return true;
        }

        public Dish Get(int id)
        {
            var obj = mongoCollection.Find("{_id:" + id + "}").FirstOrDefault();
            return new Dish()
            {
                Id = obj.GetValue("_id").ToInt32(),
                Name = obj.GetValue("Name").ToString(),
                Price = obj.GetValue("Price").ToDecimal(),
                Recipe = obj.GetValue("Recipe").ToString()
            };
        }

        public IEnumerable<Dish> GetAll()
        {
            var dishes = new List<Dish>();

            var exDishes = mongoCollection.Find("{}").ToList();

            foreach (BsonDocument dish in exDishes)
            {
                dishes.Add(new Dish()
                {
                    Id = dish.GetValue("_id").ToInt32(),
                    Name = dish.GetValue("Name").ToString(),
                    Price = dish.GetValue("Price").ToDecimal(),
                    Recipe = dish.GetValue("Recipe").ToString()
                });
            }
            return dishes;
        }

        public bool Update(Dish entity)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", entity.Id);
            var update = Builders<BsonDocument>.Update.Set("Name", entity.Name).Set("Price", entity.Price).Set("Recipe", entity.Recipe);

            mongoCollection.UpdateOne(filter, update);

            return true;
        }
    }
}
