using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Models;

namespace Una_Pinta.Controllers
{
    public class DonorController : Controller
    {
        public IActionResult DonorIndexPage()
        {
            return View();
        }

        public IActionResult DonorRegisterPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DonorTapRegister(UserSignUp userSignUp)
        {
            return View();
        }

        public IActionResult DonorQuestionsPage()
        {
            return View();
        }
    }
}
