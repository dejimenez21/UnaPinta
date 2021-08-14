using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Requests;
using Una_Pinta.Models;
using Una_Pinta.Services;

namespace Una_Pinta.Controllers
{
    public class UserController : Controller
    {
        readonly IUserRepository _userRepository;
        readonly IProvincesRepository _provincesRepository;

        public UserController(IUserRepository userRepository, IProvincesRepository provincesRepository)
        {
            _userRepository = userRepository;
            _provincesRepository = provincesRepository;
        }

        public IActionResult UserLoginPage()
        {
            return View();
        }

        public IActionResult UserRegisterPage()
        {
            GetProvinces();
            return View();
        }

        [HttpPost]
        public IActionResult UserTapRegister(UserSignUp userSignUp)
        {
            var result = _userRepository.PostUser(userSignUp).Result;
            return Json(new { code = (int)result.StatusCode, responseText = result.Content });
        }

        [HttpPost]
        public IActionResult UserTapLogin(UserSignUp userSignUp)
        {
            var result = _userRepository.GetUser(userSignUp).Result;
            if (((int)result.StatusCode) == 200)
            {
                SetUserCookies(userSignUp);
            }
            return Json(new { code = (int)result.StatusCode, responseText = result.Content });
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

        public List<Provinces> GetProvinces()
        {
            var listProvinces = _provincesRepository.GetProvinces().Result;
            ViewBag.Provinces = new SelectList(listProvinces, "state_name", "state_name");
            return listProvinces;
        }
    }
}
