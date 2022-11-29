using Restaurant.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Interfaces
{
    internal interface IDishIngredientService
    {
        public Decimal GetDishCostPrice(int dishId, IDishIngredientRepository dishIngredientRepository);
    }
}
