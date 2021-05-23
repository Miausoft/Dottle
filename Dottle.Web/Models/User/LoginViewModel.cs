using System.ComponentModel.DataAnnotations;

namespace Dottle.Web.Models.User
{
    public class LoginViewModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field is required"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
