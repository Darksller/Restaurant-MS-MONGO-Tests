using Restaurant.Service.Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests.Automatic
{
    public class UserValidationTest
    {
        [Theory]
        [InlineData("dasfaad")]
        [InlineData("145d")]
        [InlineData(null)]
        [InlineData("1374141471442223")]
        [InlineData("13")]
        public void isValidPhoneNumber_IncorrectData_IsFalse(string phoneNumber)
        {
            // Arrange
            var userValidation = new UserValidation();

            // Act
            var result = userValidation.isValidPhoneNumber(phoneNumber);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("375291127690")]
        [InlineData("1667774")]
        [InlineData("125556778")]
        public void isValidPhoneNumber_CorrectData_IsTrue(string phoneNumber)
        {
            // Arrange
            var userValidation = new UserValidation();

            // Act
            var result = userValidation.isValidPhoneNumber(phoneNumber);

            // Assert
            Assert.True(result);
        }
    }
}
