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
        [Key]
        [StringLength(255)]
        public string Login { get; set; }
    }
}
