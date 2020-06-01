using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string SenderId { get; set; }

        [Required]
        [StringLength(100)]
        public string RecipientId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }

        [StringLength(3000)]
        [DataType(DataType.MultilineText)]
        public string MessegeText { get; set; }
    }
}
