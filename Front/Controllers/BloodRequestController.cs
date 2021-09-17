using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Una_Pinta.Services;
using Una_Pinta.Helpers.Validations;
using UnaPinta.Dto.Models;
using Una_Pinta.Helpers.BloodComponentFill;

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
            LoadBloodComponents();
            LoadBloodTypes();
            return View();
        }

        public void LoadBloodTypes()
        {
            ViewData["bloodTypesList"] = BloodComponentFill.LoadBloodTypes();
        }

        public void LoadBloodComponents()
        {
            ViewData["bloodComponentList"] = BloodComponentFill.LoadBloodComponent();
        }

        [HttpPost]
        public IActionResult TapBloodRequestCreate(RequestCreate requestCreate)
        {
            requestCreate.PrescriptionBase64 = "something";
            var cookie = TempData.Peek("tokenval");
            var result = _bloodRequestRepository.PostBloodRequest(requestCreate, cookie.ToString()).Result;
            return Json(new { code = (int)result.StatusCode, responseText = result.Content });
        }

        [HttpPost]
        public IActionResult GetBloodTypes(int id)
        {
            LoadBloodTypes();
            var selectedTypes = new List<SelectListItem>();
            var listBloodFromApi = _bloodTypesRepository.GetBloodTypes(id).Result;
            foreach (var item in listBloodFromApi)
            {
                var searchtype = BloodComponentFill.LoadBloodTypes().Find(elem => elem.Value == item.ToString());
                selectedTypes.Add(new SelectListItem { Text = searchtype.Text, Value = searchtype.Value });
            }
            var types = selectedTypes.Select(elem => new { id = elem.Value, text = elem.Text });
            return Json(new { content = types });
        }

        public IActionResult BloodRequestDetail()
        {
            var cookie = TempData.Peek("tokenval");
            var result = _bloodRequestRepository.GetRequestDetails(12, cookie.ToString()).Result;
            TempData["resultRequest"] = result;
            return View();
        }

    }
}
