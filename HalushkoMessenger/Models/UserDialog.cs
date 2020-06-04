using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.Models
{
    public class UserDialog
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int DialogId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Companion full name")]
        public string CompanionFullName { get; set; }

        [Required]
        [StringLength(255)]
        public string UesrName { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("DialogId")]
        public Dialog Dialog { get; set; }
    }
}
