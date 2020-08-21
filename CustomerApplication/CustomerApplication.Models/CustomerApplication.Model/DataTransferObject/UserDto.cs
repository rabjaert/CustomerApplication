using CustomerApplication.Model.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication.Model.DataTransferObject
{
    /// <summary>DataTransferModel class</summary>
    public class UserDto
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string FirstName { get; set; }
        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        [RegularExpression(@"^[/a-zA-Z]+${1,30}")]
        public string LastName { get; set; }
        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        [RegularExpression(@"^[A-Za-z0-9]{1,15}$")]
        public string Username { get; set; }
        /// <summary>Gets or sets the telephone number.</summary>
        /// <value>The telephone number.</value>
        [RegularExpression("^[0-9]+$")]
        public int TelephoneNumber { get; set; }
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [RegularExpression(@"[A-Za-z0-9@.-]+${1,15}")]
        public string Email { get; set; }
        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [RegularExpression(@"^[A-Za-z0-9]{1,15}$")]
        public string Password { get; set; }
        /// <summary>Gets or sets the company identifier.</summary>
        /// <value>The company identifier.</value>
        public virtual int? CompanyId { get; set; }
    }
}