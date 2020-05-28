using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerApplication.Model.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string ItemName { get; set; }
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime AddDate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
