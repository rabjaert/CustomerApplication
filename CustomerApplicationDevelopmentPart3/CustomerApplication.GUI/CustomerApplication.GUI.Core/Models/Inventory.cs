using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CustomerApplication.GUI.Core.Models
{
    [DataContract]
    public class Inventory
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public DateTime AddDate { get; set; }
        [DataMember]
        public int CompanyId { get; set; }
        [DataMember]
        public Company Company { get; set; }

    }
}
