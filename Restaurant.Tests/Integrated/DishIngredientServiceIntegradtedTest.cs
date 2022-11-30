using Restaurant.DAL.Interfaces;
using Restaurant.DAL.Repositories.MongoDBRepository;
using Restaurant.Domain.Models;
using Restaurant.Service.Implementation.Repositories;
using Restaurant.Service.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests.Integrated
{
    [TestCaseOrderer("Restaurant.Tests.Integrated.Configuration", "Restaurant.Tests.Integrated")]
    public class DishIngredientServiceIntegradtedTest
    {
        private readonly string _connectionString = "mongodb://localhost:27017";

        [Fact, TestPriority(0)]
        public void CreateTestData()
        {
            try
            {
                //Arange
                IDishIngredientRepository dishIngredientRepository = new DishIngredientRepositoryMO(_connectionString);
                IDishRepository dishRepository = new DishRepositoryMO(_connectionString);
                IIngredientRepository ingredientRepository = new IngredientRepositoryMO(_connectionString);
                int firstCount = 4;
                int firstPrice = 10;
                int secondCount = 1;
                int secondPrice = 12;
                var dishName = "Тестовое блюдо";
                var dish = new Dish()
                {
                    Id = 0,
                    Name = dishName,
                    Price = 100,
                    Recipe = "Тестовый рецепт"
                };
                var firstIngredientName = "Первый ингредиент";
                var firstIngredient = new Ingredient()
                {
                    Id = 0,
                    Name = firstIngredientName,
                    Price = firstPrice
                };
                var secondIngredientName = "Второй ингредиент";
                var secondIngredient = new Ingredient()
                {
                    Id = 0,
                    Name = secondIngredientName,
                    Price = secondPrice
                };
                var dishIngredients = new List<DishIngredient>();

                // Act
                dishRepository.Create(dish);
                ingredientRepository.Create(firstIngredient);
                ingredientRepository.Create(secondIngredient);
                dishIngredients.Add(new DishIngredient()
                {
                    idDish = dishRepository.GetDishByName(dishName).Id,
                    Count = firstCount,
                    Id = 0,
                    idIngredient = ingredientRepository.GetIngredientByName(firstIngredientName).Id,
                    Price = firstPrice
                });
                dishIngredients.Add(new DishIngredient()
                {
                    idDish = dishRepository.GetDishByName(dishName).Id,
                    Count = secondCount,
                    Id = 0,
                    idIngredient = ingredientRepository.GetIngredientByName(secondIngredientName).Id,
                    Price = secondPrice
                });
                foreach (DishIngredient dishIngredient in dishIngredients)
                    dishIngredientRepository.Create(dishIngredient);

                // Assert
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.True(false);
            }
        }

        [Fact, TestPriority(1)]
        public void CalculationTest()
        {
            // Arrange
            IDishIngredientService dishIngredientService = new DishIngredientService();
            IDishIngredientRepository dishIngredientRepository = new DishIngredientRepositoryMO(_connectionString);
            IDishRepository dishRepository = new DishRepositoryMO(_connectionString);
            var dishName = "Тестовое блюдо";
            var dishId = dishRepository.GetDishByName(dishName).Id;
            var dishIngredients = dishIngredientRepository.GetDishIngredientsByDishId(dishId);

            // Act
            var result = dishIngredientService.GetDishCostPrice(dishId, dishIngredients);

            // Assert
            Assert.Equal(GetResult(), result);
        }

        [Fact, TestPriority(2)]
        public void DeleteTestData()
        {
            try
            {
                //Arange
                IDishRepository dishRepository = new DishRepositoryMO(_connectionString);
                IIngredientRepository ingredientRepository = new IngredientRepositoryMO(_connectionString);
                var dishName = "Тестовое блюдо";
                var firstIngredientName = "Первый ингредиент";
                var secondIngredientName = "Второй ингредиент";



                // Act
                dishRepository.Delete(dishRepository.GetDishByName(dishName).Id);
                ingredientRepository.Delete(ingredientRepository.GetIngredientByName(firstIngredientName).Id);
                ingredientRepository.Delete(ingredientRepository.GetIngredientByName(secondIngredientName).Id);

                // Assert
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.True(false);
            }
        }

        public Decimal GetResult() => 4 * 10 + 1 * 12;
    }
}
