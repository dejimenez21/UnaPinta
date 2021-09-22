using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Una_Pinta.Services;
using UnaPinta.Dto.Models;
using Una_Pinta.Helpers.BloodComponentFill;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Utilities;
using Microsoft.AspNetCore.Http;

namespace Una_Pinta.Controllers
{
    public class BloodRequestController : Controller
    {
        readonly IBloodTypesRepository _bloodTypesRepository;
        readonly IBloodRequestRepository _bloodRequestRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly Utilities _utilities;
        public BloodRequestController(IBloodTypesRepository bloodTypesRepository, IBloodRequestRepository bloodRequestRepository, IHttpContextAccessor httpContextAccessor)
        {
            _bloodTypesRepository = bloodTypesRepository;
            _bloodRequestRepository = bloodRequestRepository;
            _httpContextAccessor = httpContextAccessor;
            _utilities = new Utilities(httpContextAccessor);
        }

        public IActionResult BloodRequestPage()
        {
            var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var token = _utilities.GetJwtToken(getToken);
            var validateToken = _utilities.VerifyEmail(token: token);

            if (validateToken == true)
            {
                ViewData["bloodTypesList"] = BloodComponentFill.LoadBloodTypes();
                ViewData["bloodComponentList"] = BloodComponentFill.LoadBloodComponent();
                return View();
            }
            else
            {
                return RedirectToAction("ConfirmAccount", "ConfirmAccount");
            }
        }

        [HttpPost]
        public async Task<IActionResult> TapBloodRequestCreate(RequestCreate requestCreate)
        {
            requestCreate.PrescriptionBase64 = "something";
            var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var result = await _bloodRequestRepository.PostBloodRequest(requestCreate, getToken);
            return Json(new { code = (int)result.StatusCode, responseText = result.Content });
        }

        [HttpPost]
        public async Task<IActionResult> GetBloodTypes(int id)
        {
            var selectedTypes = new List<SelectListItem>();
            var listBloodFromApi = await _bloodTypesRepository.GetBloodTypes(id);
            foreach (var item in listBloodFromApi)
            {
                var searchtype = BloodComponentFill.LoadBloodTypes().Find(elem => elem.Value == item.ToString());
                selectedTypes.Add(new SelectListItem { Text = searchtype.Text, Value = searchtype.Value });
            }
            var types = selectedTypes.Select(elem => new { id = elem.Value, text = elem.Text });
            return Json(new { content = types });
        }

        public async Task<IActionResult> BloodRequestDetail()
        {
            var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var result = await _bloodRequestRepository.GetRequestDetails(12, getToken);
            TempData["resultRequest"] = result;
            return View();
        }

        public async Task<IActionResult> BloodRequestDetailsCollection()
        {
            return View();
            //var token = _utilities.GetJwtToken(Token);
            //var validateToken = _utilities.VerifyEmail(token: token);

            //if (validateToken == true)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("ConfirmAccount", "ConfirmAccount");
            //}
        }

    }
}
