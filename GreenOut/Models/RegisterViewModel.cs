using System.ComponentModel.DataAnnotations;

namespace GreenOut.Models
{
    public class RegisterViewModel
    {
        [Display(Name="Email Address")]
        [Required(ErrorMessage ="Email Address is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name="Confrim Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Passwords must match")]
        public string ConfirmPassword { get; set; }


        
    }
}
