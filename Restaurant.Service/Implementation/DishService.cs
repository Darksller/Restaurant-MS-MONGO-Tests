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
    public class DishService : IDishService
    {
        public List<Dish> GetMenu(IDishRepository dishRepository) => (List<Dish>)dishRepository.GetAll();

        public string GetRecipe(int dishId, IDishRepository dishRepository) => dishRepository.Get(dishId).Recipe;
    }
}
