using Microsoft.AspNetCore.Http;
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
            var result = _userRepository.PostUser(userSignUp).Result;
            return Json(new { code = ((int)result.StatusCode), responseText = result.Content });
        }

        [HttpPost]
        public IActionResult UserTapLogin(UserSignUp userSignUp)
        {
            var result = _userRepository.GetUser(userSignUp).Result;
            if (result == 200)
            {
                SetUserCookies(userSignUp);
            }
            return Json(result);            
        }

        public void SetUserCookies(UserSignUp userSignUp)
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(50);
            var username = "";

            if (userSignUp.UserName is not null && userSignUp.Password is not null)
            {
                HttpContext.Session.SetString(username, userSignUp.UserName);
                ViewBag.UserKeyName = HttpContext.Session.GetString(username);
                Response.Cookies.Append("id_user_key", $"{username}", option);
            }
        }
    }
}
