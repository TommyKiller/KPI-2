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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        //
        // GET: Home/Dialogs
        [HttpGet]
        public async Task<IActionResult> Dialogs()
        {
            User user = await _userManager.GetUserAsync(User);
            UserDialogsViewModel model = new UserDialogsViewModel
            {
                UserDialogs = _messenger.GetAllUserDialogs(user.Id).ToList()
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
                DialogId = dialogId,
                Messages = _messenger.GetAllDialogMessages(dialogId)
            };

            return View(model);
        }

        //
        // POST: Home/Dialog/dialogID
        [HttpPost]
        public async Task<IActionResult> Dialog(SendMessageViewModel model)
        {
            Dialog dialog = _messenger.GetDialogById(model.DialogId);
            User user = await _userManager.GetUserAsync(User);

            _messenger.SendMessage(dialog, user, model.MessegeText);

            return RedirectToAction("Dialog", dialog.Id);
        }

        //
        // GET: Home/Search/userNameSubstring
        [HttpGet]
        public IActionResult Search(string userNameSubstr)
        {
            SearchUserViewModel model = new SearchUserViewModel
            {
                Users = userNameSubstr != String.Empty ? _messenger.GetAllUsersByUserNameSubstring(userNameSubstr) : new List<User>()
            };

            return View("Search", model);
        }

        //
        // POST: Home/Search
        [HttpPost]
        public async Task<IActionResult> Search(Guid user2Id)
        {
            User user1 = await _userManager.GetUserAsync(User);
            User user2 = _messenger.GetUserById(user2Id);
            Dialog dialog;

            if (!_messenger.DialogExists(user1.Id, user2Id))
            {
                dialog = _messenger.CreateDialog(user1, user2);
                _messenger.CreateUserDialog(user1, user2, dialog);
                _messenger.CreateUserDialog(user2, user1, dialog);
                _messenger.SaveChanges();
            }
            else
            {
                dialog = _messenger.GetDialogByUsers(user1.Id, user2Id);
            }

            return RedirectToAction("Dialog", "Home", dialog.Id);
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
