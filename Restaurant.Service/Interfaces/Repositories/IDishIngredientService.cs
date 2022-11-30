using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Interfaces.Repositories
{
    public interface IDishIngredientService
    {
        public decimal GetDishCostPrice(int dishId, List<DishIngredient> dishIngredients);
    }
}
