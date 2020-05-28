using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerApplication.Model.Models
{
    /// <summary>Company class</summary>
    public class Company
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>Gets or sets the name of the company.</summary>
        /// <value>The name of the company.</value>
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string CompanyName { get; set; }
        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string Description { get; set; }
        /// <summary>Gets or sets the employees.</summary>
        /// <value>The employees.</value>
        public ICollection<Employee> Employees { get; set; }
        /// <summary>Gets or sets the inventories.</summary>
        /// <value>The inventories.</value>
        public ICollection<Inventory> Inventories { get; set; }

    }
}
