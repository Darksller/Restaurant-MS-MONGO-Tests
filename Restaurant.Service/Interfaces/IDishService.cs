using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Interfaces
{
    internal interface IDishService
    {
        public List<Dish> GetMenu(IDishRepository dishRepository);
        public String GetRecipe(int dishId, IDishRepository dishRepository);

    }
}
