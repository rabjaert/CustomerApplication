using CustomerApplication.Model.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication.Model.DataTransferObject
{
    public class UserDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string LastName { get; set; }
        [RegularExpression(@"^[A-Za-z0-9]{1,15}$")]
        public string Username { get; set; }
        [RegularExpression("^[0-9]+$")]
        public int TelephoneNumber { get; set; }
        [RegularExpression(@"[A-Za-z0-9@.-]+${1,15}")]
        public string Email { get; set; }
        [RegularExpression(@"^[A-Za-z0-9]{1,15}$")]
        public string Password { get; set; }
        public virtual int? CompanyId { get; set; }
    }
}