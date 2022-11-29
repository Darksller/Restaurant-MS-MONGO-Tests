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
    public class OrderService : IOrderService
    {
        public Decimal GetOrderInvoice(int idOrder, IOrderDishRepository orderDishRepository)
        {
            var orderDishes = orderDishRepository.GetAll();
            Decimal sum = 0;
            foreach (OrderDish orderDish in orderDishes)
            {
                if (orderDish.idOrder == idOrder)
                    sum += orderDish.Price * orderDish.Count;
            }
            return sum;

        }
        public int MakeOrder(User user, IOrderRepository orderRepository, IStatusRepository statusRepository)
        {
            if (user == null)
                return -1;
            orderRepository.Create(new Order()
            {
                Id = 0,
                OrderDate = DateTime.Now,
                Status = statusRepository.Get(1),
                User = user
            });
            return orderRepository.GetNextOrderId();
        }
        public bool FillOrder(List<OrderDish> orderDishes, IOrderDishRepository orderDishRepository)
        {
            foreach (OrderDish orderDish in orderDishes)
                orderDishRepository.Create(orderDish);
            return true;
        }
        public List<Order> OrdersForThePeriod(DateTime leftSide, DateTime rightSide, IOrderRepository orderRepository)
        {
            var orders = (List<Order>)orderRepository.GetAll();
            var ordersForThePeriod = new List<Order>();
            foreach (Order order in orders)
                if (order.OrderDate >= leftSide && order.OrderDate <= rightSide)
                    ordersForThePeriod.Add(order);
            return ordersForThePeriod.OrderBy(order => order.OrderDate).ToList();
        }
    }
}
