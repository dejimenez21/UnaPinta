using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Controllers
{
    public class HomePage : Controller
    {
        public IActionResult HomePageView()
        {
            return View();
        }
    }
}
