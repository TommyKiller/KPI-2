﻿using HalushkoMessenger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.ViewModels
{
    public class DialogMessagesViewModel
    {
        public int DialogId { get; set; }

        public List<Message> Messages { get; set; }
    }
}
