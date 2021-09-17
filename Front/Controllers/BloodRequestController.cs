using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Una_Pinta.Services;
using Una_Pinta.Helpers.Validations;
using UnaPinta.Dto.Models;
using Una_Pinta.Helpers.BloodComponentFill;
using System.Threading.Tasks;

namespace Una_Pinta.Controllers
{
    public class BloodRequestController : Controller
    {
        readonly IBloodTypesRepository _bloodTypesRepository;
        readonly IBloodRequestRepository _bloodRequestRepository;
        public BloodRequestController(IBloodTypesRepository bloodTypesRepository, IBloodRequestRepository bloodRequestRepository)
        {
            _bloodTypesRepository = bloodTypesRepository;
            _bloodRequestRepository = bloodRequestRepository;
        }

        public IActionResult BloodRequestPage()
        {
            //var tokenString = TempData.Peek("tokenval").ToString();

            //var token = new JwtSecurityToken(jwtEncodedString: tokenString);

            //var validateToken = ValidateToken.VerifiedToken(token);

            //if (validateToken == true)
            //{
            //    LoadBloodComponents();
            //    LoadBloodTypes();
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("ConfirmAccount", "ConfirmAccount");
            //}
            ViewData["bloodTypesList"] = BloodComponentFill.LoadBloodTypes();
            ViewData["bloodComponentList"] = BloodComponentFill.LoadBloodComponent();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TapBloodRequestCreate(RequestCreate requestCreate)
        {
            requestCreate.PrescriptionBase64 = "something";
            var cookie = TempData.Peek("tokenval");
            var result = await _bloodRequestRepository.PostBloodRequest(requestCreate, cookie.ToString());
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
            var cookie = TempData.Peek("tokenval");
            var result = await _bloodRequestRepository.GetRequestDetails(12, cookie.ToString());
            TempData["resultRequest"] = result;
            return View();
        }

    }
}
