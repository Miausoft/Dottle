using System.ComponentModel.DataAnnotations;

namespace Support_Your_Locals.Models
{
    public class BusinessRegisterModel
    {

        [Required(ErrorMessage = "Please enter the product or activity")]
        public string Product {get; set;}
        [Required(ErrorMessage = "Please add description")]
        public string Description {get; set;}
        [Required(ErrorMessage = "Please enter the business phone number")]
        public string PhoneNumber {get; set;}
        [Required(ErrorMessage = "Please enter header")]
        public string Header {get; set;}
    }
}