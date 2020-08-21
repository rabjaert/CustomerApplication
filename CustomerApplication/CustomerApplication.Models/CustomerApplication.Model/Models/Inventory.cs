using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerApplication.Model.Models
{
    public class Inventory
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>Gets or sets the name of the item.</summary>
        /// <value>The name of the item.</value>
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string ItemName { get; set; }
        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string Description { get; set; }
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }
        /// <summary>Gets or sets the add date.</summary>
        /// <value>The add date.</value>
        public DateTime AddDate { get; set; }
        /// <summary>Gets or sets the company identifier.</summary>
        /// <value>The company identifier.</value>
        public int CompanyId { get; set; }
        /// <summary>Gets or sets the company.</summary>
        /// <value>The company.</value>
        public Company Company { get; set; }

    }
}
