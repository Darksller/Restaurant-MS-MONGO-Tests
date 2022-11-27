using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models
{
    public class DishIngredient
    {
        public int Id { get; set; }

        public int idDish { get; set; }

        public int idIngredient { get; set; }

        public int Count { get; set; }

        public Decimal Price { get; set; }

    }
}
