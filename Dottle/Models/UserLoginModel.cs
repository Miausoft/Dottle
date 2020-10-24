using System.ComponentModel.DataAnnotations;

namespace Dottle.Models
{
    public class UserLoginModel
    {

        [Required(ErrorMessage = "Please enter your email address")]
        public string Email { get; set; }
    }
}
