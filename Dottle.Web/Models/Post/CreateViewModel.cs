using System.ComponentModel.DataAnnotations;

namespace Dottle.Web.Models.Post
{
    public class CreateViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [MaxLength(254)]
        public string Email { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        [MaxLength(30)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
