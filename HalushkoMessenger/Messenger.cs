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

        public User GetUserById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetAllUsersByUserNameSubstring(string userNameSubstr)
        {
            userNameSubstr = userNameSubstr.Normalize();
            return _context.Users.Where(u => u.NormalizedUserName.Contains(userNameSubstr)).ToList();
        }

        public Dialog GetDialogById(int dialogId)
        {
            return _context.Dialogs.Find(dialogId);
        }

        public Dialog GetDialogByUsers(Guid userId, Guid companionId)
        {
            return _context.UserDialogs.Include(ud => ud.Dialog).Single(ud => ud.UserId == userId && ud.CompanionId == companionId).Dialog;
        }

        public IEnumerable<UserDialog> GetAllUserDialogs(Guid userId)
        {
            return _context.UserDialogs.Where(ud => ud.UserId == userId);
        }

        public List<Message> GetAllDialogMessages(int dialogId)
        {
            return _context.Messages.Where(m => m.DialogId == dialogId).ToList();
        }

        public Message SendMessage(Dialog dialog, User sender, string text)
        {
            Message message = new Message
            {
                Dialog = dialog,
                Sender = sender,
                DateTimeStamp = DateTime.Now,
                MessegeText = text
            };
            _context.Messages.Add(message);

            return message;
        }

        public Dialog CreateDialog(User user1, User user2)
        {
            Dialog dialog = new Dialog { User1 = user1, User2 = user2 };
            _context.Dialogs.Add(dialog);

            return dialog;
        }

        public UserDialog CreateUserDialog(User user, User companion, Dialog dialog)
        {
            UserDialog ud = new UserDialog { User = user, Companion = companion, Dialog = dialog };
            _context.UserDialogs.Add(ud);

            return ud;
        }

        public bool DialogExists(Guid userId, Guid companionId)
        {
            return _context.UserDialogs.Any(ud => ud.UserId == userId && ud.CompanionId == companionId);
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
