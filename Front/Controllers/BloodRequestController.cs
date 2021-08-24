using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Services;
using UnaPinta.Dto.Models;

namespace Una_Pinta.Controllers
{
    public class BloodRequestController : Controller
    {
        readonly IBloodTypesRepository _bloodTypesRepository;
        readonly IBloodRequestRepository _bloodRequestRepository;
        List<SelectListItem> bloodTypes = new List<SelectListItem>();
        public BloodRequestController(IBloodTypesRepository bloodTypesRepository, IBloodRequestRepository bloodRequestRepository)
        {
            _bloodTypesRepository = bloodTypesRepository;
            _bloodRequestRepository = bloodRequestRepository;
        }

        public IActionResult BloodRequestPage()
        {
            LoadBloodTypes();
            return View();
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

    }
}
