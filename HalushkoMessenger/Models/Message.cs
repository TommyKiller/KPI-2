using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DialogId { get; set; }

        [Required]
        public string SenderUserId { get; set; }

        [Required]
        public string RecipientUserId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }

        [StringLength(3000)]
        [DataType(DataType.MultilineText)]
        public string MessegeText { get; set; }

        [ForeignKey("DialogId")]
        public Dialog Dialog { get; set; }

        [ForeignKey("SenderUserId")]
        public User SenderUser { get; set; }

        [ForeignKey("RecipientUserId")]
        public User RecipientUser { get; set; }
    }
}
