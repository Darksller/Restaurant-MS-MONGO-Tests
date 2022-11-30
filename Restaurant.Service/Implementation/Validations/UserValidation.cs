using Restaurant.Service.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Implementation.Validations
{
    public class UserValidation : IUserValidation
    {
        public bool isValidPhoneNumber(string phoneNumber)
        {
            bool isValid = false;
            string pattern = @".,@!?()_%$+=#%&*~<>";
            if (!string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length >= 7 && phoneNumber.Length <= 15 && !phoneNumber.Any(pN => pattern.Contains(pN)) &&
                !phoneNumber.Any(pN => Char.IsLetter(pN)))
                isValid = true;
            return isValid;
        }
    }
}
