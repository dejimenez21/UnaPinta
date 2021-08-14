using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Core.Contracts;
using UnaPinta.Data.Entities;

namespace UnaPinta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IProvinceService _provinceService;

        public ProvincesController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        public async Task<ActionResult<IEnumerable<Province>>> GetAllProvinces()
        {
            var provinces = await _provinceService.GetAllProvinces();
            return Ok(provinces);
        }

    }
}
