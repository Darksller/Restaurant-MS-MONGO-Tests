using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant.DAL.Interfaces;
using Restaurant.DAL.Repositories.MsServerRepository;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories.MongoDBRepository
{
    public class DishIngredientRepositoryMO : IDishIngredientRepository
    {
        private IMongoCollection<BsonDocument> _mongoCollection;

        private IIngredientRepository _ingredientRepositoryMO;

        public DishIngredientRepositoryMO(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase("res");
            _mongoCollection = mongoDatabase.GetCollection<BsonDocument>("dishes");
            _ingredientRepositoryMO = new IngredientRepositoryMO(connectionString);
        }

        public bool Create(DishIngredient entity)
        {
            var pipeline = new BsonDocument
            {
                {"$unwind", "$Ingredients"}
            };

            var pipeline2 = new BsonDocument
            {
                { "$project", new BsonDocument
                  {
                    { "_id", "$_id"},
                    { "IngId", "$Ingredients._id"}
                  }
                }
            };

            BsonDocument[] pipelines = new BsonDocument[] { pipeline, pipeline2 };
            List<BsonDocument> results = _mongoCollection.Aggregate<BsonDocument>(pipelines).ToList();

            List<int> arr = new List<int>();
            foreach (var item in results)
                arr.Add(item.GetValue("IngId").ToInt32());

            if (arr.Count == 0) entity.Id = 1;
            else entity.Id = arr.Max() + 1;

            var filter = Builders<BsonDocument>.Filter.Eq("_id", entity.idDish);
            var ing = _ingredientRepositoryMO.Get(entity.idIngredient);

            BsonDocument bsonElements = new BsonDocument()
            {
                {"_id", entity.Id},
                {"idIngredient", ing.Id},
                {"Name", ing.Name},
                {"Price",ing.Price },
                {"Count",entity.Count }
            };

            var arrayUpdate = Builders<BsonDocument>.Update.Push("Ingredients", bsonElements);

            _mongoCollection.UpdateOneAsync(filter, arrayUpdate);

            return true;
        }

        public bool Delete(int id)
        {
            var data = Get(id);

            var filter = new BsonDocument("_id", data.idDish);
            var update = Builders<BsonDocument>.Update.Pull("Ingredients", new BsonDocument() { { "_id", id } });
            _mongoCollection.FindOneAndUpdate(filter, update);

            return true;
        }

        public DishIngredient Get(int id)
        {
            var pipeline = new BsonDocument
            {
                {"$unwind", "$Ingredients"}
            };

            var pipeline2 = new BsonDocument
            {
                { "$match", new BsonDocument
                   {
                      {"Ingredients._id", id }
                   }
                }
            };

            var pipeline3 = new BsonDocument
            {
                { "$project", new BsonDocument
                   {
                      { "_id", "$_id"},
                      { "idIng", "$Ingredients.idIngredient"},
                      { "idDishIng", "$Ingredients._id"},
                      { "Count", "$Ingredients.Count"},
                      { "Price", "$Ingredients.Price"},
                   }
                }
            };
            BsonDocument[] pipelines = new BsonDocument[] { pipeline, pipeline2, pipeline3 };
            List<BsonDocument> results = _mongoCollection.Aggregate<BsonDocument>(pipelines).ToList();

            var dishIngredient = new DishIngredient();

            var item = results[0];

            dishIngredient.idDish = item.GetValue("_id").ToInt32();
            dishIngredient.Id = item.GetValue("idDishIng").ToInt32();
            dishIngredient.idIngredient = item.GetValue("idIng").ToInt32();
            dishIngredient.Count = item.GetValue("Count").ToInt32();
            dishIngredient.Price = item.GetValue("Price").ToInt32();

            return dishIngredient;
        }

        public IEnumerable<DishIngredient> GetAll()
        {
            var pipeline = new BsonDocument
            {
                {"$unwind", "$Ingredients"}
            };

            var pipeline3 = new BsonDocument
            {
                { "$project", new BsonDocument
                   {
                      { "_id", "$_id"},
                      { "idIng", "$Ingredients.idIngredient"},
                      { "idDishIng", "$Ingredients._id"},
                      { "Count", "$Ingredients.Count"},
                      { "Price", "$Ingredients.Price"},
                   }
                }
            };
            BsonDocument[] pipelines = new BsonDocument[] { pipeline, pipeline3 };
            List<BsonDocument> results = _mongoCollection.Aggregate<BsonDocument>(pipelines).ToList();

            var dishIngredient = new List<DishIngredient>();

            foreach (BsonDocument item in results)
            {
                dishIngredient.Add(new DishIngredient
                {
                    idDish = item.GetValue("_id").ToInt32(),
                    Id = item.GetValue("idDishIng").ToInt32(),
                    idIngredient = item.GetValue("idIng").ToInt32(),
                    Count = item.GetValue("Count").ToInt32(),
                    Price = item.GetValue("Price").ToDecimal()
                });
            }

            return dishIngredient;
        }

        public bool Update(DishIngredient entity)
        {
            var ing = _ingredientRepositoryMO.Get(entity.idIngredient);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", entity.idDish) & Builders<BsonDocument>.Filter.Eq("Ingredients._id", entity.Id);
            var update = Builders<BsonDocument>.Update.Set("Ingredients.$.Name", ing.Name)
                .Set("Ingredients.$.Price", ing.Price).Set("Ingredients.$.idIngredient", entity.idIngredient).Set("Ingredients.$.Price", entity.Price)
                .Set("Ingredients.$.Count", entity.Count);
            _mongoCollection.UpdateOne(filter, update);
            return true;
        }
    }
}
