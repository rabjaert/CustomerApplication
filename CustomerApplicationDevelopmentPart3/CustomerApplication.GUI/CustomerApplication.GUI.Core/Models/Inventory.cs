using System;
using System.Runtime.Serialization;

namespace CustomerApplication.GUI.Core.Models
{
    /// <summary>Inventory class</summary>
    [DataContract]
    public class Inventory
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [DataMember]
        public int Id { get; set; }
        /// <summary>Gets or sets the name of the item.</summary>
        /// <value>The name of the item.</value>
        [DataMember]
        public string ItemName { get; set; }
        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [DataMember]
        public string Description { get; set; }
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        [DataMember]
        public int Quantity { get; set; }
        /// <summary>Gets or sets the add date.</summary>
        /// <value>The add date.</value>
        [DataMember]
        public DateTime AddDate { get; set; }
        /// <summary>Gets or sets the company identifier.</summary>
        /// <value>The company identifier.</value>
        [DataMember]
        public int CompanyId { get; set; }
        /// <summary>Gets or sets the company.</summary>
        /// <value>The company.</value>
        [DataMember]
        public Company Company { get; set; }

    }
}
