﻿using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Interfaces
{
    public interface IDishRepository : IBaseRepository<Dish>
    {
        public Dish GetDishByName(string name);
    }
}
