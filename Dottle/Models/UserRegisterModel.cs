using System.ComponentModel.DataAnnotations;

namespace Dottle.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

    }
}
