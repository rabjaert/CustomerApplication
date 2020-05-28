using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerApplication.Model.Models
{
   public class Company
    {
        public int Id { get; set; }
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string CompanyName { get; set; }
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Inventory> Inventories { get; set; }

    }
}
