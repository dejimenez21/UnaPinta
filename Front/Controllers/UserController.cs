using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Models;
using Una_Pinta.Services;

namespace Una_Pinta.Controllers
{
    public class UserController : Controller
    {
        readonly IUserRepository _userRepository;
        const string registerpage = "UserRegisterPage";

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult UserLoginPage()
        {
            return View();
        }

        public IActionResult UserRegisterPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserTapRegister(UserSignUp userSignUp)
        {
            _userRepository.PostUser(userSignUp);
            return View(registerpage);
        }

        [HttpPost]
        public IActionResult UserTapLogin(UserSignUp userSignUp)
        {
            var result = _userRepository.GetUser(userSignUp).Result;
            if (result is null)
                return View(registerpage);
            else
                return RedirectToAction(actionName: "HomePageView", controllerName: "HomePage");
        }
    }
}
