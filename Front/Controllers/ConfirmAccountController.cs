using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Controllers
{
    public class ConfirmAccountController : Controller
    {
        public IActionResult ConfirmAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmButton()
        {
            return View();
        }

        public IActionResult ConfirmAccountProcess()
        {
            return View();
        }
    }
}
