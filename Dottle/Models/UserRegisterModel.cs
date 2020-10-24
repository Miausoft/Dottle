using System.ComponentModel.DataAnnotations;

namespace Support_Your_Locals.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter your birth date")]
        public string BirthDate { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [Key]
        public string Email { get; set; }

    }
}
