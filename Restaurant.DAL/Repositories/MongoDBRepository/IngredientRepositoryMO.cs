using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories.MongoDBRepository
{
    public class IngredientRepositoryMO : IIngredientRepository
    {
        public bool Create(Ingredient entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Ingredient Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ingredient> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
