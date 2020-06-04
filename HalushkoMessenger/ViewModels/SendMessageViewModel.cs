using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.ViewModels
{
    public class SendMessageViewModel
    {
        [Required]
        public int DialogId { get; set; }

        [Required]
        public string SenderUserId { get; set; }

        [Required]
        public string RecipientUserId { get; set; }

        [StringLength(3000)]
        [DataType(DataType.MultilineText)]
        public string MessegeText { get; set; }
    }
}
