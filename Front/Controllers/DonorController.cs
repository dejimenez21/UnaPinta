using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Models;
using Una_Pinta.Services;

namespace Una_Pinta.Controllers
{
    public class DonorController : Controller
    {
        readonly IUserRepository _userRepository;

        public DonorController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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
            var result = _userRepository.PostUser(userSignUp).Result;
            return Json(new { code = ((int)result.StatusCode), responseText = result.Content });
        }

        public IActionResult DonorQuestionsPage()
        {
            return View();
        }
    }
}
