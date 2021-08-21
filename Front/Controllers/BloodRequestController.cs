using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Services;

namespace Una_Pinta.Controllers
{
    public class BloodRequestController : Controller
    {
        readonly IBloodTypesRepository _bloodTypesRepository;
        List<SelectListItem> bloodTypes = new List<SelectListItem>();
        public BloodRequestController(IBloodTypesRepository bloodTypesRepository)
        {
            _bloodTypesRepository = bloodTypesRepository;
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
