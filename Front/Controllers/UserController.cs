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
using UnaPinta.Dto.Models.Auth;
using UnaPinta.Dto.Models.User;

namespace Una_Pinta.Controllers
{
    public class UserController : Controller
    {
        readonly IUserRepository _userRepository;
        readonly IProvincesRepository _provincesRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly Utilities _utilities;
        public RoleEnum RoleEnum;
        public int BloodType;

        public UserController(IUserRepository userRepository, IProvincesRepository provincesRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _provincesRepository = provincesRepository;
            _utilities = new Utilities(httpContextAccessor);
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult UserLoginPage()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
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
            var tokenSession = "";
            var name = "";
            DateTime date = new DateTime();
            var result = await _userRepository.GetUser(userSignUp);            
            RoleEnum = new RoleEnum();
            if (((int)result.StatusCode) == 200)
            {
                tokenSession = _utilities.SetSession(result);
                var token = _utilities.GetJwtToken(tokenSession);
                BloodType = _utilities.VerifyBloodType(token);
                RoleEnum = _utilities.VerifyRole(token);
                _utilities.SetUserName(token);
                name = _utilities.GetUserInfo(token).name;
                date = _utilities.GetUserInfo(token).birthDate;
            }
            return Json(new { code = (int)result.StatusCode, 
                responseText = result.Content, 
                roleUser = ((int)RoleEnum), 
                blood = BloodType, 
                token = tokenSession,
                nameOfUser = name,
                birthDate = date
            });
            
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

        [HttpGet("resetPassword")]
        public async Task<IActionResult> UserNewPassword(string userName, string token)
        {
            _httpContextAccessor.HttpContext.Session.SetString("userToken", token);
            _httpContextAccessor.HttpContext.Session.SetString("userName", userName);
            return RedirectToAction("ResetCredentials", "User");
        }

        public async Task<IActionResult> MailCredentials()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SendEmail(string email)
        {
            var resultContent = await _userRepository.SendEmail(email);
            return Json(new { content = resultContent.Content, statusCode = resultContent.StatusCode });
        }

        public async Task<IActionResult> ResetCredentials()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostNewCredentials(string password)
        {
            PasswordResetDto passwordResetDto = new PasswordResetDto();
            passwordResetDto.NewPassword = password;
            passwordResetDto.UserName = _httpContextAccessor.HttpContext.Session.GetString("userName");
            passwordResetDto.Token = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var resultContent = await _userRepository.ResetPassword(passwordResetDto);
            return Json(new {content = resultContent.Content, StatusCode = resultContent.StatusCode});
        }

        public async Task<IActionResult> UserProfile()
        {
            var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var resultContent = await _userRepository.GetUserProfile(getToken);
            TempData["userProfile"] = resultContent;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ModifyFields(string province, string phone)
        {
            var updateUserProfileDto = new UpdateUserProfileDto();
            var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            updateUserProfileDto.ProvinceCode = province;
            updateUserProfileDto.PhoneNumber = phone;
            var resultContent = await _userRepository.UpdateUserProfile(getToken, updateUserProfileDto);
            return Json(new { code = ((int)resultContent.StatusCode), content = resultContent.Content});
        }
    }
}
