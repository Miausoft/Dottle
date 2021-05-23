using System.ComponentModel.DataAnnotations;

namespace Dottle.ViewModels
{
    public class Inquiry
    {
        [Required]
        public string From { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
