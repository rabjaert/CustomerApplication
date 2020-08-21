using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CustomerApplication.GUI.Core.Models
{
    /// <summary>Company class</summary>
    [DataContract]
    public class Company
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [DataMember]
        public int Id { get; set; }
        /// <summary>Gets or sets the name of the company.</summary>
        /// <value>The name of the company.</value>
        [DataMember]
        public string CompanyName { get; set; }
        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [DataMember]
        public string Description { get; set; }
        /// <summary>Gets or sets the employees.</summary>
        /// <value>The employees.</value>
        [DataMember]
        public ICollection<Employee> Employees { get; set; }

    }
}
