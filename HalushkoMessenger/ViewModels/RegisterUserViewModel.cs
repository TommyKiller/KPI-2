using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [StringLength(256)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "First name")]
        public string Name { get; set; }

        [StringLength(256)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords should match!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
