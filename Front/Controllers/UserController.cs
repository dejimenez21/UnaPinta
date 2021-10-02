using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using UnaPinta.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
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
        readonly Utilities _utilities;
        public RoleEnum RoleEnum;
        public int BloodType;

        public UserController(IUserRepository userRepository, IProvincesRepository provincesRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _provincesRepository = provincesRepository;
            _utilities = new Utilities(httpContextAccessor);
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
        public async Task<IActionResult> UserTapRegister(UserSignUp userSignUp)
        {
            var result = await _userRepository.PostUser(userSignUp);
            return Json(new { code = (int)result.StatusCode, responseText = result.Content });
        }

        [HttpPost]
        public async Task<IActionResult> UserTapLogin(UserSignUp userSignUp)
        {
            var result = await _userRepository.GetUser(userSignUp);            
            RoleEnum = new RoleEnum();
            if (((int)result.StatusCode) == 200)
            {
                var tokenSession = _utilities.SetSession(result);
                var token = _utilities.GetJwtToken(tokenSession);
                BloodType = _utilities.VerifyBloodType(token);
                RoleEnum = _utilities.VerifyRole(token);
                _utilities.SetUserName(token);
            }
            return Json(new { code = (int)result.StatusCode, responseText = result.Content, roleUser = ((int)RoleEnum), blood = BloodType });
            
        }

        public async Task<IActionResult> GetProvinces()
        {
            var listProvinces = await _provincesRepository.GetProvinces();
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
                _utilities.SetSession(result);
                return RedirectToAction("ConfirmAccountProcess", "ConfirmAccount");
            }
            else
            {
                return RedirectToAction("ConfirmAccount", "ConfirmAccount");
            }
        }
    }
}
