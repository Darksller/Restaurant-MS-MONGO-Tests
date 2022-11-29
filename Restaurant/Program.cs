using Restaurant.DAL.Repositories.MongoDBRepository;
using Restaurant.DAL.Repositories.MsServerRepository;
using Restaurant.Service.Implementation;

namespace Restaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017";

            var d = new OrderDishRepositoryMS(connectionString);

            var dd = new OrderService();

            dd.MakeOrder(null, new OrderRepositoryMO(connectionString), new StatusRepositoryMO(connectionString));
        }
    }
}