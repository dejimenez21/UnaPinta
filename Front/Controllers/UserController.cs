using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserIndexPage()
        {
            return View();
        }

        public IActionResult UserRegisterPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserTapRegister()
        {
            return View();
        }
    }
}
