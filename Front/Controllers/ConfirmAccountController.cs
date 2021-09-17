using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Services;

namespace Una_Pinta.Controllers
{
    public class ConfirmAccountController : Controller
    {
        readonly IUserRepository _userRepository;
        public ConfirmAccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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

        [HttpGet]
        public async Task<IActionResult> SendEmailVerification()
        {
            var tokenString = TempData.Peek("tokenval").ToString();
            var reuslt = await _userRepository.ResendEmail(tokenString);
            return View();
        }
    }
}
