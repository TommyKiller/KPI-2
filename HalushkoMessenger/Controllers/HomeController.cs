using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HalushkoMessenger.Models;
using HalushkoMessenger.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HalushkoMessenger.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly Messenger _messenger;

        public HomeController(UserManager<User> userManager, Messenger messenger)
        {
            _userManager = userManager;
            _messenger = messenger;
        }

        //
        // GET: Home/Dialogs
        [HttpGet]
        public IActionResult Dialogs()
        {
            UserDialogsViewModel model = new UserDialogsViewModel
            {
                UserDialogs = _messenger.GetAllUserDialogs(_userManager.GetUserId(User)).ToList()
            };

            return View(model);
        }

        //
        // GET: Home/Dialog/dialogID
        [HttpGet]
        public IActionResult Dialog(int dialogId)
        {
            DialogMessagesViewModel model = new DialogMessagesViewModel
            {
                Messages = _messenger.GetAllDialogMessages(dialogId)
            };

            return View(model);
        }

        //
        // POST: Home/Dialog/dialogID
        [HttpPost]
        public IActionResult Dialog(SendMessageViewModel model)
        {
            throw new NotImplementedException();
        }

        //
        // GET: Home/Search
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        //
        // GET: Home/Search/userNameSubstring
        [HttpGet]
        public IActionResult Search(string userNameSubstr)
        {
            SearchUserViewModel model = new SearchUserViewModel
            {
                Users = _messenger.GetAllUsersByUserNameSubstr(userNameSubstr)
            };

            return View("Search",model);
        }

        //
        // POST: Home/Search
        [HttpPost]
        public async Task<IActionResult> Search(User user2)
        {
            User user1 = await _userManager.GetUserAsync(User);

            Dialog dialog = _messenger.CreateDialog(user1, user2);
            UserDialog user1Dialog = _messenger.CreateUserDialog(user1, dialog, String.Format("{0} {1}", user2.Name, user2.Surname));
            UserDialog user2Dialog = _messenger.CreateUserDialog(user2, dialog, String.Format("{0} {1}", user1.Name, user1.Surname));
            _messenger.SaveChanges();

            return View("Dialog", dialog.Id);
        }

        //
        // GET: Home/Profile
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            User user = await _userManager.GetUserAsync(User);

            Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserProfileViewModel>()));

            UserProfileViewModel model = mapper.Map<User, UserProfileViewModel>(user);

            return View(model);
        }
    }
}
