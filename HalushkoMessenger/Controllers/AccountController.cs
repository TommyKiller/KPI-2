using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HalushkoMessenger.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HalushkoMessenger.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AutoMapper;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HalushkoMessenger.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !(_userManager is null))
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        private void WriteErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }

        //
        // GET: /Accont/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        //
        // POST: /Accont/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<RegisterUserViewModel, User>()));

                User user = mapper.Map<RegisterUserViewModel, User>(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);

                    return RedirectToAction("Dialogs", "Home");
                }
                else
                {
                    WriteErrors(result);
                }
            }

            return View(model);
        }

        //
        // GET: /Accont/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        //
        // POST: /Accont/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {   
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(ViewBag.ReturnUrl) && Url.IsLocalUrl(ViewBag.ReturnUrl))
                    {
                        return Redirect(ViewBag.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Dialogs", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("signIn", "Wrong login or password");
                }
            }

            return View(model);
        }

        //
        // GET: Accout/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: Account/Edit
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            User user = await _userManager.GetUserAsync(User);

            Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, EditUserViewModel>()));

            EditUserViewModel model = mapper.Map<User, EditUserViewModel>(user);

            return View(model);
        }

        //
        // PUT: Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(User);

                Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<EditUserViewModel, User>()));

                user = mapper.Map<EditUserViewModel, User>(model);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    WriteErrors(result);
                }
            }

            return View(model);
        }

        //
        // GET: Accout/Profile
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