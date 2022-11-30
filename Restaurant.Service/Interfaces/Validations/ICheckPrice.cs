using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Interfaces.Validations
{
    public interface ICheckPrice
    {
        public bool isValidPrice(int price);
    }
}
