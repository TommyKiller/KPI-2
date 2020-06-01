using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.Models
{
    public class UserDbContext : DbContext
    {
        public DbSet<Messege> Messeges { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
