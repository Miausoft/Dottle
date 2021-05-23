using System.ComponentModel.DataAnnotations;

namespace Dottle.Web.Models.User
{
    public class CreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First name must be 50 characters or less.")]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "First name may only contain alphanumeric characters.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last name must be 50 characters or less.")]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "LastName may only contain alphanumeric characters.")]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [MaxLength(254, ErrorMessage = "Email address must be 254 characters or less.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field is required"), DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long.")]
        [MaxLength(25, ErrorMessage = "Password must be 25 characters or less.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]+).+$", ErrorMessage = "Password must contain a number, lowercase and capital letter.")]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
