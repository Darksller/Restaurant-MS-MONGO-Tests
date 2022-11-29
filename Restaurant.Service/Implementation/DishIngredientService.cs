using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using Restaurant.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Implementation
{
    public class DishIngredientService : IDishIngredientService
    {
        public Decimal GetDishCostPrice(int dishId, IDishIngredientRepository dishIngredientRepository)
        {
            var dishesIngredients = dishIngredientRepository.GetAll();
            Decimal sum = 0;
            foreach (DishIngredient dishIng in dishesIngredients)
            {
                if (dishIng.idDish == dishId)
                    sum += dishIng.Price * dishIng.Count;
            }
            return sum;
        }
    }
}
