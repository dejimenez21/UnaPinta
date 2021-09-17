using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Requests;
using Una_Pinta.Helpers.Utilities;
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
            return View();
        }

        [HttpPost]
        public IActionResult UserTapRegister(UserSignUp userSignUp)
        {
            var result = _userRepository.PostUser(userSignUp).Result;
            if (((int)result.StatusCode) == 201)
            {
                TempData["tokenval"] = Utilities.SetUserCookies(result);
            }
            return Json(new { code = (int)result.StatusCode, responseText = result.Content });
        }

        [HttpPost]
        public IActionResult UserTapLogin(UserSignUp userSignUp)
        {
            var result = _userRepository.GetUser(userSignUp).Result;
            if (((int)result.StatusCode) == 200)
            {
                TempData["tokenval"] = Utilities.SetUserCookies(result);
            }
            return Json(new { code = (int)result.StatusCode, responseText = result.Content });
        }

        public IActionResult GetProvinces()
        {
            var listProvinces = _provincesRepository.GetProvinces().Result;
            var selectList = new List<SelectListItem>();
            foreach (var item in listProvinces)
            {
                selectList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            var provinces = selectList.Select(elem => new { code = elem.Value, name = elem.Text });
            return Json(new { content = provinces });
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> EmailVerification(string id, string token)
        {
            var result = await _userRepository.ConfirmEmail(id, token);
            if (((int)result.StatusCode) == 200)
            {
                return RedirectToAction("ConfirmAccountProcess", "ConfirmAccount");
            }
            else
            {
                return RedirectToAction("ConfirmAccount", "ConfirmAccount");
            }
        }
    }
}
