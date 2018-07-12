using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class LoginViewModel 
    {
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Username or Email")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}