using System.ComponentModel.DataAnnotations;

namespace Dottle.Models
{
    public class UserRegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
