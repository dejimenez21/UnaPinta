using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Utilities;
using Una_Pinta.Models;
using Una_Pinta.Services;

namespace Una_Pinta.Controllers
{
    public class DonorController : Controller
    {
        readonly IUserRepository _userRepository;
        readonly IProvincesRepository _provincesRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly Utilities _utilities;
        public DonorController(IUserRepository userRepository, IProvincesRepository provincesRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _provincesRepository = provincesRepository;
            _httpContextAccessor = httpContextAccessor;
            _utilities = new Utilities(httpContextAccessor);
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
        public async Task<IActionResult> DonorTapRegister(UserSignUp userSignUp)
        {
            var result = await _userRepository.PostUser(userSignUp);
            return Json(new { code = ((int)result.StatusCode), responseText = result.Content });
        }

        public async Task<IActionResult> GetProvinces()
        {
            var listProvinces =  await _provincesRepository.GetProvinces();
            var selectList = new List<SelectListItem>();
            foreach (var item in listProvinces)
            {
                selectList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            var provinces = selectList.Select(elem => new { code = elem.Value, name = elem.Text });
            return Json(new { content = provinces });
        }

        public IActionResult DonorQuestionsPage()
        {
            var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var token = _utilities.GetJwtToken(getToken);
            var validateToken = _utilities.VerifyEmail(token: token);

            if (validateToken == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ConfirmAccount", "ConfirmAccount");
            }
        }
    }
}
