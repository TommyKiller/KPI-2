using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.Models
{
    public class Dialog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid User1Id { get; set; }

        [Required]
        public Guid User2Id { get; set; }

        [ForeignKey("User1Id")]
        public User User1 { get; set; }

        [ForeignKey("User2Id")]
        public User User2 { get; set; }
    }
}
