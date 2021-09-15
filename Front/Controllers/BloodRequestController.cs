using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Models;
using Una_Pinta.Services;
using UnaPinta.Dto.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Una_Pinta.Controllers
{
    public class BloodRequestController : Controller
    {
        readonly IBloodTypesRepository _bloodTypesRepository;
        readonly IBloodRequestRepository _bloodRequestRepository;
        List<SelectListItem> bloodTypes = new List<SelectListItem>();
        List<SelectListItem> bloodComponent = new List<SelectListItem>();
        public BloodRequestController(IBloodTypesRepository bloodTypesRepository, IBloodRequestRepository bloodRequestRepository)
        {
            _bloodTypesRepository = bloodTypesRepository;
            _bloodRequestRepository = bloodRequestRepository;
        }

        public IActionResult BloodRequestPage()
        {
            var tokenString = TempData.Peek("tokenval").ToString();

            var token = new JwtSecurityToken(jwtEncodedString: tokenString);

            var validateToken = VerifiedToken(token);

            if (validateToken == true)
            {
                LoadBloodComponents();
                LoadBloodTypes();
                return View();
            }
            else
            {
                return RedirectToAction("ConfirmAccount", "ConfirmAccount");
            }
        }

        public bool VerifiedToken(JwtSecurityToken token)
        {
            if(token.Claims.First(c => c.Type == "EmailConfirmed").Value.Contains("False"))
            {
                return false;
            }

            return true;
        }

        public void LoadBloodTypes()
        {
            bloodTypes.Add(new SelectListItem { Text = "A+", Value = "1" });
            bloodTypes.Add(new SelectListItem { Text = "A-", Value = "2" });
            bloodTypes.Add(new SelectListItem { Text = "B+", Value = "3" });
            bloodTypes.Add(new SelectListItem { Text = "B-", Value = "4" });
            bloodTypes.Add(new SelectListItem { Text = "AB+", Value = "5" });
            bloodTypes.Add(new SelectListItem { Text = "AB-", Value = "6" });
            bloodTypes.Add(new SelectListItem { Text = "O+", Value = "7" });
            bloodTypes.Add(new SelectListItem { Text = "O-", Value = "8" });
            ViewData["bloodTypesList"] = bloodTypes;
        }

        public void LoadBloodComponents()
        {
            bloodComponent.Add(new SelectListItem { Text = "Plasma", Value = "1" });
            bloodComponent.Add(new SelectListItem { Text = "Plaquetas", Value = "2" });
            bloodComponent.Add(new SelectListItem { Text = "Globulos Blancos", Value = "3" });
            bloodComponent.Add(new SelectListItem { Text = "Globulos Rojos", Value = "4" });
            ViewData["bloodComponentList"] = bloodComponent;
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
                var searchtype = bloodTypes.Find(elem => elem.Value == item.ToString());
                selectedTypes.Add(new SelectListItem { Text = searchtype.Text, Value = searchtype.Value});
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
