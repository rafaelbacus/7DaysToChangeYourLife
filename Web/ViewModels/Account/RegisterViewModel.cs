using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}