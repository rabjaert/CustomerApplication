using System.Runtime.Serialization;

namespace App1.Core.Models
{
    [DataContract]
    public class User
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
        public byte[] PasswordHash { get; set; }
        [DataMember]
        public byte[] PasswordSalt { get; set; }
    }
}