using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.Managers
{
    public class DialogResult
    {
        public bool Succeeded { get; set; }

        public IEnumerable<DialogError> Errors { get; set; }

        DialogResult(bool succeeded, params DialogError[] errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
    }

    public class DialogError
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
