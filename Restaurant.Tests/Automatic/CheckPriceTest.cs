using Restaurant.Service.Implementation.Validations;
using Restaurant.Service.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests.Automatic
{
    public class CheckPriceTest
    {
        [Fact]
        public void isValidPrice_LessThenMin_IsFalse()
        {
            // Arrange
            ICheckPrice checkPrice = new CheckPrice();
            int price = -1000;

            // Act
            var result = checkPrice.isValidPrice(price);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void isValidPrice_MoreThenMax_IsFalse()
        {
            // Arrange
            ICheckPrice checkPrice = new CheckPrice();
            int price = 10000;

            // Act
            var result = checkPrice.isValidPrice(price);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void isValidPrice_Max_IsTrue()
        {
            // Arrange
            ICheckPrice checkPrice = new CheckPrice();
            int price = 1000;

            // Act
            var result = checkPrice.isValidPrice(price);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void isValidPrice_Min_IsTrue()
        {
            // Arrange
            ICheckPrice checkPrice = new CheckPrice();
            int price = 1;

            // Act
            var result = checkPrice.isValidPrice(price);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void isValidPrice_Mid_IsTrue()
        {
            // Arrange
            ICheckPrice checkPrice = new CheckPrice();
            int price = 500;

            // Act
            var result = checkPrice.isValidPrice(price);

            // Assert
            Assert.True(result);
        }
    }
}
