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
        public Guid UserId { get; set; }

        [Required]
        public Guid CompanionId { get; set; }

        [Required]
        public int DialogId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        
        [ForeignKey("CompanionId")]
        public User Companion { get; set; }

        [ForeignKey("DialogId")]
        public Dialog Dialog { get; set; }
    }
}
