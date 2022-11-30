using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Interfaces.Validations
{
    public interface IUserValidation
    {
        public bool isValidPhoneNumber(string phoneNumber);
    }
}
