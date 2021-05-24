using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dottle.Web.Models
{
    public class CreatePostViewModel
    {
        //[Required]
        //[MaxLength(50)]
        public string Title { get; set; }

        //[Required]
        //[MaxLength(500)]
        public string Description { get; set; }

        //[Required, DataType(DataType.EmailAddress)]
        //[MaxLength(254)]
        public string Email { get; set; }

        //[Required, DataType(DataType.PhoneNumber)]
        //[MaxLength(30)]
        public string Phone { get; set; }

        //[Required]
        //[MaxLength(100)]
        public string Address { get; set; }

        public List<TimeSheetViewModel> TimeSheets { get; set; } = new List<TimeSheetViewModel>();
    }
}
