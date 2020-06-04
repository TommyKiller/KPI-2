using HalushkoMessenger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.ViewModels
{
    public class SearchUserViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}
