using CustomerApplication.GUI.Core.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CustomerApplication.GUI.Core.Models
{
   [DataContract]
    public class Company
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public ICollection<Employee> Employees { get; set; }

    }
}
