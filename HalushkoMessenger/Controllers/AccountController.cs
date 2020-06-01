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

namespace HalushkoMessenger.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        //
        // GET: /Accont/Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Accont/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterUserViewModel model)
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
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View(model);
        }

        ////
        //// GET: /Accont/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        ////
        //// POST: /Accont/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<LoginUserViewModel, User>()));

                User user = mapper.Map<LoginUserViewModel, User>(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);

                    return RedirectToAction("Dialogs", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View(model);
        }
    }
}
