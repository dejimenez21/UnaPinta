using Microsoft.AspNetCore.Http;
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
        readonly IHttpContextAccessor _httpContextAccessor;
        public ConfirmAccountController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
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
            var tokenUser = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var result = await _userRepository.ResendEmail(tokenUser);
            return Json(new { code = result.StatusCode, content = result.Content });
        }
    }
}
