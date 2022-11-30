using Restaurant.DAL.Interfaces;
using Restaurant.DAL.Repositories.MongoDBRepository;
using Restaurant.Domain.Models;
using Restaurant.Service.Implementation.Repositories;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests.Automatic
{
    public class DishIngredientServiceTest
    {
        private readonly string _connectionString = "mongodb://localhost:27017";
        [Fact]
        public void GetDishCostPrice_NullData_IsFalse()
        {
            // Arrange
            List<DishIngredient> dishIngredients = null;
            var dishIngredientService = new DishIngredientService();
            var dishId = 1;
            // Act
            var result = dishIngredientService.GetDishCostPrice(dishId, dishIngredients);

            // Assert
            Assert.Equal(result, -1);
        }

        [Fact]
        public void GetDishCostPrice_NoData_IsFalse()
        {
            // Arrange
            List<DishIngredient> dishIngredients = new List<DishIngredient>();
            var dishIngredientService = new DishIngredientService();
            var dishId = 1;
            // Act
            var result = dishIngredientService.GetDishCostPrice(dishId, dishIngredients);

            // Assert
            Assert.Equal(result, -1);
        }

        [Fact]
        public void GetDishCostPrice_IncorrectId_IsFalse()
        {
            // Arrange
            var dishIngredientRepository = new DishIngredientRepositoryMO(_connectionString);
            var dishIngredients = (List<DishIngredient>)dishIngredientRepository.GetAll();
            var dishIngredientService = new DishIngredientService();
            var dishId = -1;
            // Act
            var result = dishIngredientService.GetDishCostPrice(dishId, dishIngredients);

            // Assert
            Assert.Equal(result, -1);
        }

        [Fact]
        public void GetDishCostPrice_CorrectData_IsTrue()
        {
            // Arrange
            var dishIngredientRepository = new DishIngredientRepositoryMO(_connectionString);
            var dishIngredients = (List<DishIngredient>)dishIngredientRepository.GetAll();
            var dishIngredientService = new DishIngredientService();
            var dishId = 1;
            // Act
            var result = dishIngredientService.GetDishCostPrice(dishId, dishIngredients);

            // Assert
            Assert.NotEqual(result, -1);
        }
    }
}
