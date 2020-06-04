using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HalushkoMessenger.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "User name")]
        public override string UserName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "First name")]
        public string Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
    }
}
