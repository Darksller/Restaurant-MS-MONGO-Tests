using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using Restaurant.Service.Interfaces;
using Restaurant.Service.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Implementation.Repositories
{
    public class DishIngredientService : IDishIngredientService
    {
        public decimal GetDishCostPrice(int dishId, List<DishIngredient> dishIngredients)
        {
            if (dishIngredients == null || dishIngredients.Count < 1 || dishId < 1) return -1;
            decimal sum = 0;
            foreach (DishIngredient dishIng in dishIngredients)
            {
                if (dishIng.idDish == dishId)
                    sum += dishIng.Price * dishIng.Count;
            }
            return sum;
        }
    }
}
