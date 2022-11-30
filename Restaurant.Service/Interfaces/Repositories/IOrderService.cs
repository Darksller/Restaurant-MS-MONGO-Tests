using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Interfaces.Repositories
{
    public interface IOrderService
    {
        public decimal GetOrderInvoice(int idOrder, IOrderDishRepository orderDishRepository);
        public int MakeOrder(User user, IOrderRepository orderRepository, IStatusRepository statusRepository);
        public bool FillOrder(List<OrderDish> orderDishes, IOrderDishRepository orderDishRepository);
        public List<Order> OrdersForThePeriod(DateTime leftSide, DateTime rightSide, IOrderRepository orderRepository);

    }
}
