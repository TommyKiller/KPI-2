using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<UserDialog> UserDialogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //
            // User
            modelBuilder.Entity<User>(u =>
            {
                u.HasIndex(u => u.UserName).IsUnique();
                u.Property(u => u.UserName).HasMaxLength(256);
                u.Property(u => u.Name).HasMaxLength(256);
                u.Property(u => u.Surname).HasMaxLength(256);
            });
            //
            // UserDialog
            modelBuilder.Entity<UserDialog>(ud =>
            {
                ud.HasKey(ud => new { ud.UserId, ud.DialogId });
                ud.HasOne(ud => ud.User).WithMany().HasForeignKey(ud => ud.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                ud.HasOne(ud => ud.Companion).WithMany().HasForeignKey(ud => ud.CompanionId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                ud.HasOne(ud => ud.Dialog).WithMany().HasForeignKey(ud => ud.DialogId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            //
            // Dialog
            modelBuilder.Entity<Dialog>(d =>
            {
                d.HasKey(d => d.Id);
                d.HasOne(d => d.User1).WithMany().HasForeignKey(d => d.User1Id).OnDelete(DeleteBehavior.NoAction).IsRequired();
                d.HasOne(d => d.User2).WithMany().HasForeignKey(d => d.User2Id).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });
            //
            // Message
            modelBuilder.Entity<Message>(m =>
            {
                m.HasKey(m => m.Id);
                m.HasOne(m => m.Sender).WithMany().HasForeignKey(m => m.SenderId).IsRequired();
                m.HasOne(m => m.Dialog).WithMany().HasForeignKey(m => m.DialogId).IsRequired();
                m.Property(m => m.MessegeText).HasMaxLength(3000).IsRequired();
                m.Property(m => m.DateTimeStamp).IsRequired();
            });
        }
    }
}
