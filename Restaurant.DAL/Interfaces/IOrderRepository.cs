using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public int GetNextOrderId();
    }
}
