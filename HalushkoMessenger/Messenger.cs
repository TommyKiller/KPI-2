using HalushkoMessenger.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HalushkoMessenger
{
    public class Messenger : IDisposable
    {
        private bool disposing;
        private readonly ApplicationDbContext _context;

        public Messenger(ApplicationDbContext context)
        {
            disposing = false;
            _context = context;
        }

        public void Dispose()
        {
            disposing = true;

            Dispose(disposing);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public IEnumerable<User> GetAllUsersByUserNameSubstr(string userNameSubstr)
        {
            return _context.Users.Where(u => u.UserName == userNameSubstr);
        }

        public IEnumerable<UserDialog> GetAllUserDialogs(string userId)
        {
            return _context.UserDialogs.Where(ud => ud.UserId == userId);
        }

        public List<Message> GetAllDialogMessages(int dialogId)
        {
            return _context.Messages.Where(m => m.DialogId == dialogId).ToList();
        }

        public Dialog CreateDialog(User user1, User user2)
        {
            return new Dialog();
        }

        public UserDialog CreateUserDialog(User user, Dialog dialog, string companionFulName)
        {
            return new UserDialog();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
