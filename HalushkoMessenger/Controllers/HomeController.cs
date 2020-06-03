using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HalushkoMessenger.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: Home/Dialogs
        [HttpGet]
        public IActionResult Dialogs()
        {
            return View();
        }

        //// Home/Dialogs/DialogID
        //[HttpGet]
        //public IActionResult Dialogs(int id)
        //{
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult About()
        //{
        //    return View();
        //}

        //
        // GET: Home/Profile
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
