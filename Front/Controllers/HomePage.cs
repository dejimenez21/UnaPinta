using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Utilities;
using UnaPinta.Dto.Enums;

namespace Una_Pinta.Controllers
{
    public class HomePage : Controller
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly Utilities _utilities;
        public string Token { get; set; } = "";
        public RoleEnum RoleEnum;
        public bool EmailVerified = false;

        public HomePage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _utilities = new Utilities(_httpContextAccessor);
        }

        public IActionResult HomePageView()
        {
            return View();
        }

        public IActionResult NavigateToBloodRequestCollection()
        {
            Token = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            if (!string.IsNullOrEmpty(Token))
            {
                var tokenDecoded = _utilities.GetJwtToken(Token);
                EmailVerified = _utilities.VerifyEmail(tokenDecoded);
                RoleEnum = _utilities.VerifyRole(tokenDecoded);
            }
            return Json(new { verified = EmailVerified, roleUser = ((int)RoleEnum) });
        }

        public IActionResult NavigateToBloodRequest()
        {
            Token = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            if (!string.IsNullOrEmpty(Token))
            {
                var tokenDecoded = _utilities.GetJwtToken(Token);
                EmailVerified = _utilities.VerifyEmail(tokenDecoded);
                RoleEnum = _utilities.VerifyRole(tokenDecoded);
            }
            return Json(new { verified = EmailVerified, roleUser = ((int)RoleEnum) });
        }
    }

}
