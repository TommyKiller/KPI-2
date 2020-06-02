using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.Models
{
    public class AccountsDbContext : IdentityDbContext<User>
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options)
            : base(options)
        {

        }
    }
}
