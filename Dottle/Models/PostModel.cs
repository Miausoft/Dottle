using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dottle.Models
{
    public class PostModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Add description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter business address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Select working hours")]
        public string TimeSheet { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}
