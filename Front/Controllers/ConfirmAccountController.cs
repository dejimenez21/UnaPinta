using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Utilities;
using Una_Pinta.Services;
using UnaPinta.Dto.Enums;

namespace Una_Pinta.Controllers
{
    public class ConfirmAccountController : Controller
    {
        readonly IUserRepository _userRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly Utilities _utilities;
        public RoleEnum RoleEnum;
        public ConfirmAccountController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _utilities = new Utilities(httpContextAccessor);
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

        [HttpPost]
        public IActionResult ConfirmProcess()
        {
            var tokenUser = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var tokenDecoded = _utilities.GetJwtToken(tokenUser);
            RoleEnum = _utilities.VerifyRole(tokenDecoded);
            var emailVerify = _utilities.VerifyEmail(tokenDecoded);
            return Json(new { roleUser = ((int)RoleEnum), emailUser = emailVerify });
        }

        [HttpGet]
        public async Task<IActionResult> SendEmailVerification()
        {
            var tokenUser = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var result = await _userRepository.ResendEmail(tokenUser);
            return Json(new { code = result.StatusCode, content = result.Content });
        }
    }
}
