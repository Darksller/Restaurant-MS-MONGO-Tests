using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
