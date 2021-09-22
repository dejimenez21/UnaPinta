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

        public HomePage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult HomePageView()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return View();
        }
    }

}
