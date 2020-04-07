using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApplication.Models
{
    public class Person
    {

        protected int PersonID { get; set; }
        protected string FirstName { get; set; }
        protected string LastName { get; set; }
        protected int Age { get; set; }

        [EmailAddress]
        protected string Email { get; set;}

        protected int PhoneNumber { get; set; }

        protected Role RoleCompany { get; set; }

        protected Company Company { get; set; }

    }
}
