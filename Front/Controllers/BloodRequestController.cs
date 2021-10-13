﻿using Microsoft.AspNetCore.Mvc;
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
using Una_Pinta.Models;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Una_Pinta.Controllers
{
    public class BloodRequestController : Controller
    {
        readonly IBloodTypesRepository _bloodTypesRepository;
        readonly IBloodRequestRepository _bloodRequestRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IProvincesRepository _provincesRepository;
        readonly Utilities _utilities;
        public List<RequestSummary> RequestSummaries;
        public BloodRequestController(IBloodTypesRepository bloodTypesRepository, IBloodRequestRepository bloodRequestRepository, IHttpContextAccessor httpContextAccessor, IProvincesRepository provincesRepository, IHostingEnvironment hostingEnvironment)
        {
            _bloodTypesRepository = bloodTypesRepository;
            _bloodRequestRepository = bloodRequestRepository;
            _httpContextAccessor = httpContextAccessor;
            _provincesRepository = provincesRepository;
            _utilities = new Utilities(httpContextAccessor);
            RequestSummaries = new List<RequestSummary>();
        }

        public IActionResult BloodRequestPage()
        {
            var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var token = _utilities.GetJwtToken(getToken);
            var validateToken = _utilities.VerifyEmail(token: token);

            if (validateToken == true)
            {
                TempData["token"] = getToken;
                return View();
            }
            else
            {
                return RedirectToAction("ConfirmAccount", "ConfirmAccount");
            }
        }

        [HttpPost]
        public async Task<IActionResult> TapBloodRequestCreate([FromForm]RequestCreateDto requestCreate)
        {

            if (requestCreate.ForMe)
            {
                var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
                var token = _utilities.GetJwtToken(getToken);                
                requestCreate.Name = _utilities.GetUserInfo(token).name;
                requestCreate.BloodTypeId = _utilities.GetUserInfo(token).bloodType;
                requestCreate.BirthDate = _utilities.GetUserInfo(token).birthDate;
                
                var result = await _bloodRequestRepository.PostBloodRequest(requestCreate, getToken);
                return Json(new { code = (int)result.StatusCode, responseText = result.Content });
            }
            else
            {
                var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
                var result = await _bloodRequestRepository.PostBloodRequest(requestCreate, getToken);
                
                return Json(new { code = (int)result.StatusCode, responseText = result.Content });
            }
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
            //var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            //var result = await _bloodRequestRepository.GetRequestDetails(id, getToken);
            var request = _httpContextAccessor.HttpContext.Session.GetString("requestDetails");
            var result = JsonConvert.DeserializeObject<RequestDetails>(request);
            TempData["resultRequest"] = result;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetProvinces()
        {
            var listProvinces = await _provincesRepository.GetProvinces();
            var selectList = new List<SelectListItem>();
            foreach (var item in listProvinces)
            {
                selectList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            var provinces = selectList.Select(elem => new { code = elem.Value, name = elem.Text });
            return Json(new { content = provinces });
        }

        public async Task<IActionResult> BloodRequestDetailsCollection()
        {
            var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var token = _utilities.GetJwtToken(getToken);
            var validateToken = _utilities.VerifyEmail(token: token);

            if (validateToken == true)
            {
                var requestSummary = await _bloodRequestRepository.GetRequestSummary(getToken);
                RequestSummaries = requestSummary;
                TempData["requestSummary"] = requestSummary;
                return View();
            }
            else
            {
                return RedirectToAction("ConfirmAccount", "ConfirmAccount");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStringDates()
        {
            var stringDates = await _bloodRequestRepository.GetStringDates();
            var selectList = new List<SelectListItem>();
            foreach (var item in stringDates)
            {
                selectList.Add(new SelectListItem { Text = item.String, Value = item.Id.ToString() });
            }
            var dates = selectList.Select(elem => new { code = elem.Value, name = elem.Text });
            return Json(new { content = dates });
        }

        [HttpPost]
        public async Task<IActionResult> PostCases(int id)
        {
            //TODO: Improve this code
            Cases cases = new Cases();
            cases.RequestId = id;

            var getToken = _httpContextAccessor.HttpContext.Session.GetString("userToken");
            var createCase = await _bloodRequestRepository.PostCase(cases, getToken);

            var jsonContentResult = createCase.Content;
            var jo = JObject.Parse(jsonContentResult);
            var resultContent = jo["request"];
            var resultParsed = resultContent.ToObject<RequestDetails>();
            _httpContextAccessor.HttpContext.Session.SetString("requestDetails", JsonConvert.SerializeObject(resultParsed));
            return Json(new { code = (int)createCase.StatusCode, responseText = createCase.Content});
        }

        public async Task<IActionResult> BloodRequestList()
        {
            return View();
        }
    }
}
