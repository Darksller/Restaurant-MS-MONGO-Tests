using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant.DAL.Interfaces;
using Restaurant.DAL.Repositories.MsServerRepository;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories.MongoDBRepository
{
    public class OrderDishRepositoryMO : IOrderDishRepository
    {
        private IMongoCollection<BsonDocument> _mongoCollection;

        private IDishRepository _dishRepositoryMO;

        public OrderDishRepositoryMO(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase("res");
            _mongoCollection = mongoDatabase.GetCollection<BsonDocument>("orders");
            _dishRepositoryMO = new DishRepositoryMO(connectionString);
        }

        public bool Create(OrderDish entity)
        {
            var pipeline = new BsonDocument
            {
                {"$unwind", "$Dishes"}
            };

            var pipeline2 = new BsonDocument
            {
                { "$project", new BsonDocument
                  {
                    { "_id", "$_id"},
                    { "dishId", "$Dishes._id"}
                  }
                }
            };

            BsonDocument[] pipelines = new BsonDocument[] { pipeline, pipeline2 };
            List<BsonDocument> results = _mongoCollection.Aggregate<BsonDocument>(pipelines).ToList();

            List<int> arr = new List<int>();
            foreach (var item in results)
                arr.Add(item.GetValue("dishId").ToInt32());

            if (arr.Count == 0) entity.Id = 1;
            else entity.Id = arr.Max() + 1;

            var filter = Builders<BsonDocument>.Filter.Eq("_id", entity.idOrder);
            var dish = _dishRepositoryMO.Get(entity.idDish);

            BsonDocument bsonElements = new BsonDocument()
            {
                {"_id", entity.Id},
                {"idDish", dish.Id},
                {"Name", dish.Name},
                {"Price",entity.Price},
                {"Count",entity.Count}
            };

            var arrayUpdate = Builders<BsonDocument>.Update.Push("Dishes", bsonElements);

            _mongoCollection.UpdateOneAsync(filter, arrayUpdate);

            return true;
        }

        public bool Delete(int id)
        {
            var data = Get(id);

            var filter = new BsonDocument("_id", data.idDish);
            var update = Builders<BsonDocument>.Update.Pull("Dishes", new BsonDocument() { { "_id", id } });
            _mongoCollection.FindOneAndUpdate(filter, update);

            return true;
        }

        public OrderDish Get(int id)
        {
            var pipeline = new BsonDocument
            {
                {"$unwind", "$Dishes"}
            };

            var pipeline2 = new BsonDocument
            {
                { "$match", new BsonDocument
                   {
                      {"Dishes._id", id }
                   }
                }
            };

            var pipeline3 = new BsonDocument
            {
                { "$project", new BsonDocument
                   {
                      { "_id", "$_id"},
                      { "idDish", "$Dishes.idDish"},
                      { "idDishOrder", "$Dishes._id"},
                      { "Count", "$Dishes.Count"},
                      { "Price", "$Dishes.Price"},
                   }
                }
            };
            BsonDocument[] pipelines = new BsonDocument[] { pipeline, pipeline2, pipeline3 };
            List<BsonDocument> results = _mongoCollection.Aggregate<BsonDocument>(pipelines).ToList();

            var orderDish = new OrderDish();

            var item = results[0];

            orderDish.idOrder = item.GetValue("_id").ToInt32();
            orderDish.Id = item.GetValue("idDishOrder").ToInt32();
            orderDish.idDish = item.GetValue("idDish").ToInt32();
            orderDish.Count = item.GetValue("Count").ToInt32();
            orderDish.Price = item.GetValue("Price").ToDecimal();

            return orderDish;
        }

        public IEnumerable<OrderDish> GetAll()
        {
            var pipeline = new BsonDocument
            {
                {"$unwind", "$Dishes"}
            };

            var pipeline3 = new BsonDocument
            {
                { "$project", new BsonDocument
                   {
                      { "_id", "$_id"},
                      { "idDish", "$Dishes.idDish"},
                      { "idDishOrder", "$Dishes._id"},
                      { "Count", "$Dishes.Count"},
                      { "Price", "$Dishes.Price"},
                   }
                }
            };
            BsonDocument[] pipelines = new BsonDocument[] { pipeline, pipeline3 };
            List<BsonDocument> results = _mongoCollection.Aggregate<BsonDocument>(pipelines).ToList();

            var orderDish = new List<OrderDish>();

            foreach (BsonDocument item in results)
            {
                orderDish.Add(new OrderDish
                {
                    idOrder = item.GetValue("_id").ToInt32(),
                    Id = item.GetValue("idDishOrder").ToInt32(),
                    idDish = item.GetValue("idDish").ToInt32(),
                    Count = item.GetValue("Count").ToInt32(),
                    Price = item.GetValue("Price").ToDecimal()
                });
            }

            return orderDish;
        }

        public bool Update(OrderDish entity)
        {
            var dish = _dishRepositoryMO.Get(entity.idDish);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", entity.idOrder) & Builders<BsonDocument>.Filter.Eq("Dishes._id", entity.Id);
            var update = Builders<BsonDocument>.Update.Set("Dishes.$.Name", dish.Name)
                .Set("Dishes.$.Price", dish.Price).Set("Dishes.$.idDish", entity.idDish).Set("Dishes.$.Count", entity.Count).Set("Dishes.$.Price", entity.Price);
            _mongoCollection.UpdateOne(filter, update);
            return true;
        }
    }

}
