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

namespace HalushkoMessenger.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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

                    // Creating user`s database
                    DbContextOptions<UserDbContext> options = new DbContextOptionsBuilder<UserDbContext>()
                        .UseSqlServer(String.Format(_configuration.GetConnectionString("UserConnection"), user.UserName))
                        .Options;

                    _ = new UserDbContext(options).Database.EnsureCreated();

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
        // POST: Accout/Profile
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Profile()
        {
            return View();
        }
}
}