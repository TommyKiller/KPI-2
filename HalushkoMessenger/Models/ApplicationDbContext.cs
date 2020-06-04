using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalushkoMessenger.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
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
            //
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            //
            // UserDialog
            //
            modelBuilder.Entity<UserDialog>()
                .HasKey(ud => new { ud.UserId, ud.DialogId });
            modelBuilder.Entity<UserDialog>()
                .HasOne(ud => ud.User)
                .WithMany()
                .HasForeignKey(ud => ud.UserId);
            modelBuilder.Entity<UserDialog>()
                .HasOne(ud => ud.Dialog)
                .WithMany()
                .HasForeignKey(ud => ud.DialogId);
            //
            // Dialog
            //
            modelBuilder.Entity<Dialog>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<Dialog>()
                .HasOne(d => d.User1)
                .WithMany()
                .HasForeignKey(d => d.User1Id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Dialog>()
                .HasOne(d => d.User2)
                .WithMany()
                .HasForeignKey(d => d.User2Id)
                .OnDelete(DeleteBehavior.NoAction);
            //
            // Message
            //
            modelBuilder.Entity<Message>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<Message>()
                .HasOne(m => m.SenderUser)
                .WithMany()
                .HasForeignKey(m => m.SenderUserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Message>()
                .HasOne(m => m.RecipientUser)
                .WithMany()
                .HasForeignKey(m => m.RecipientUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
