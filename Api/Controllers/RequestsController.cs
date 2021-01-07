using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Entities;
using Api.Services;
using AutoMapper;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IUnaPintaRepository _repo;
        private readonly IMapper _mapper;

        public RequestsController(IUnaPintaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // [HttpGet("")]
        // public async Task<ActionResult<IEnumerable<TModel>>> GetTModels()
        // {
        //     // TODO: Your code here
        //     await Task.Yield();

        //     return new List<TModel> { };
        // }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<TModel>> GetTModelById(int id)
        // {
        //     // TODO: Your code here
        //     await Task.Yield();

        //     return null;
        // }

        [HttpPost("")]
        public async Task<ActionResult<Request>> CreateRequest(RequestCreate requestCreate)
        {
            var request = _mapper.Map<Request>(requestCreate);
            return Created("api/requests", request);
        }

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