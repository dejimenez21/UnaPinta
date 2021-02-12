using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Entities;
using Api.Services;
using AutoMapper;
using Api.Helpers;
using Api.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IUnaPintaRepository _repo;
        private readonly IMapper _mapper;
        private readonly IRequestsService _service;

        public RequestsController(IUnaPintaRepository repo, IMapper mapper, IRequestsService service)
        {
            _repo = repo;
            _mapper = mapper;
            _service = service;
        }

        // [HttpGet("")]
        // public async Task<ActionResult<IEnumerable<TModel>>> GetTModels()
        // {
        //     await Task.Yield();

        //     return new List<TModel> { };
        // }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<TModel>> GetTModelById(int id)
        // {
        //     await Task.Yield();

        //     return null;
        // }

        [HttpPost(""), Authorize]
        public async Task<ActionResult<Request>> CreateRequest(RequestCreate requestCreate)
        {
            var request = _mapper.Map<Request>(requestCreate);
            _repo.CreateRequest(request);
            await _repo.SaveChangesAsync();

            Response.OnCompleted(async () => 
                await _service.SendRequestNotification(request)
            );

            return Created("api/requests", request);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTModel(int id, TModel model)
        // {
        //     await Task.Yield();

        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult<TModel>> DeleteTModelById(int id)
        // {
        //     await Task.Yield();

        //     return null;
        // }
    }
}