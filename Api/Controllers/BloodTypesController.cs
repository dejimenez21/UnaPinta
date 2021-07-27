using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnaPinta.Data.Entities;
using UnaPinta.Core.Services;
using UnaPinta.Data.Contracts;
using UnaPinta.Dto.Helpers;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodTypesController : ControllerBase
    {
        private readonly IUnaPintaRepository _repo;

        public BloodTypesController(IUnaPintaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<BloodType>>> GetAllBloodTypes()
        {
            var bloodTypes = await _repo.GetAllBloodTypes();
            return Ok(bloodTypes);
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<TModel>> GetTModelById(int id)
        // {
        //     // TODO: Your code here
        //     await Task.Yield();

        //     return null;
        // }

        // [HttpPost("")]
        // public async Task<ActionResult<TModel>> PostTModel(TModel model)
        // {
        //     // TODO: Your code here
        //     await Task.Yield();

        //     return null;
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTModel(int id, TModel model)
        // {
        //     // TODO: Your code here
        //     await Task.Yield();

        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult<TModel>> DeleteTModelById(int id)
        // {
        //     // TODO: Your code here
        //     await Task.Yield();

        //     return null;
        // }
    }
}