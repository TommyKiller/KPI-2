using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HalushkoMessenger.Managers;
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
        private readonly DialogsManager _dialogsManager;

        public HomeController(UserManager<User> userManager, DialogsManager dialogsManager)
        {
            _userManager = userManager;
            _dialogsManager = dialogsManager;
        }

        //
        // GET: Home/Dialogs
        [HttpGet]
        public IActionResult Dialogs()
        {
            UserDialogsViewModel model = new UserDialogsViewModel
            {
                UserDialogs = _dialogsManager.GetAllUserDialogs(_userManager.GetUserId(User)).ToList()
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
                Messages = _dialogsManager.GetAllDialogMessages(dialogId)
            };

            return View(model);
        }

        //
        // POST: Home/Dialog/dialogID
        [HttpPost]
        public IActionResult Dialog(Message message)
        {
            return View();
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
