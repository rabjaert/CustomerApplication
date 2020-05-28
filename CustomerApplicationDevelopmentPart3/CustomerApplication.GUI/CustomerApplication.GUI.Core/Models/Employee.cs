using System.Runtime.Serialization;

namespace CustomerApplication.GUI.Core.Models
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public int TelephoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public byte[] PasswordHash { get; set; }
        [DataMember]
        public byte[] PasswordSalt { get; set; }
       
        [DataMember]
        public int CompanyId { get; set; }
    }
}