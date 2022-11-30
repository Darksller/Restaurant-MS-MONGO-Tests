using Moq;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests.Moq
{
    public class IngredientRepositoryTest
    {
        [Fact]
        public void CreateNewIngredient()
        {
            // Arrange 
            var newIngredient = new Ingredient { Name = "Банан", Price = 599 };
            var repositoryMoq = new Mock<IIngredientRepository>();
            repositoryMoq.Setup(rep => rep.Create(newIngredient)).Returns(true);
            IIngredientRepository repository = repositoryMoq.Object;

            //Act
            var result = repository.Create(newIngredient);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteIngredient()
        {
            
        }
    }
}
