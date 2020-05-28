using System.Runtime.Serialization;

namespace CustomerApplication.GUI.Core.Models
{
    /// <summary>Employees class</summary>
    [DataContract]
    public class Employee
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [DataMember]
        public int Id { get; set; }
        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        [DataMember]
        public string FirstName { get; set; }
        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        [DataMember]
        public string LastName { get; set; }
        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        [DataMember]
        public string Username { get; set; }
        /// <summary>Gets or sets the telephone number.</summary>
        /// <value>The telephone number.</value>
        [DataMember]
        public int TelephoneNumber { get; set; }
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [DataMember]
        public string Email { get; set; }
        /// <summary>Gets or sets the password hash.</summary>
        /// <value>The password hash.</value>
        [DataMember]
        public byte[] PasswordHash { get; set; }
        /// <summary>Gets or sets the password salt.</summary>
        /// <value>The password salt.</value>
        [DataMember]
        public byte[] PasswordSalt { get; set; }

        /// <summary>Gets or sets the company identifier.</summary>
        /// <value>The company identifier.</value>
        [DataMember]
        public int CompanyId { get; set; }
    }
}