using CustomerApplication.GUI.Core.Models;
using System.Runtime.Serialization;

namespace CustomerApplication.GUI.Core.DataTransferObject
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public int TelephoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int CompanyId { get; set; }
    }
}