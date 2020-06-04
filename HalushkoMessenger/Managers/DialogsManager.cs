using HalushkoMessenger.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HalushkoMessenger.Managers
{
    public class DialogsManager : IDisposable
    {
        private bool disposing;
        private readonly ApplicationDbContext _context;

        public DialogsManager(ApplicationDbContext context)
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

        public User GetUserById(string userId)
        {
            return _context.Users.Single(u => u.Id == userId);
        }

        public IEnumerable<UserDialog> GetAllUserDialogs(string userId)
        {
            return _context.UserDialogs.Where(ud => ud.UserId == userId);
        }

        public List<Message> GetAllDialogMessages(int dialogId)
        {
            return _context.Messages.Where(m => m.DialogId == dialogId).ToList();
        }

        public void SendMessage(int dialogId, string senderId, string recipientId, string messageText, DateTime dateTimeStamp)
        {
            Message message = new Message
            {
                DialogId = dialogId,
                SenderUserId = senderId,
                RecipientUserId = recipientId,
                DateTimeStamp = dateTimeStamp,
                MessegeText = messageText
            };

            _context.Messages.Add(message);

            _context.SaveChanges();
        }

        public bool DialogExists(string user1Id, string user2Id)
        {
            return _context.Dialogs.Any(d => (d.User1Id == user1Id && d.User2Id == user2Id) ||
                d.User1Id == user2Id && d.User2Id == user1Id);
        }
    }
}
