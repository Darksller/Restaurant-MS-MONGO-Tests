using Restaurant.Service.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Implementation.Validations
{
    public class CheckPrice : ICheckPrice
    {
        public bool isValidPrice(int price)
        {
            bool isValis = false;
            if (price > 0 && price <= 1000)
            {
                isValis = true;
            }
            return isValis;
        }
    }
}
