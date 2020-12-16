using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dottle.Models
{
    public class UserModel
    {
        [Required]
        [Key]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string PasswordSalt { get; set; }

        public int Score { get; set; }
    }
}
