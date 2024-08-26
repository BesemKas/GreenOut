using System.ComponentModel.DataAnnotations;

namespace GreenOut.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is Required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
