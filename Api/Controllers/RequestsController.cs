using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnaPinta.Dto.Models;
using UnaPinta.Data.Entities;
using AutoMapper;
using UnaPinta.Core.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace UnaPinta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "solicitante")]
    public class RequestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRequestsService _service;

        public RequestsController(IMapper mapper, IRequestsService service)
        {
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

        [HttpPost("")]
        public async Task<ActionResult<Request>> CreateRequest(RequestCreate requestCreate)
        {
            var request = _mapper.Map<Request>(requestCreate);

            try
            {
                await _service.CreateRequest(request, HttpContext.User.FindFirst("UserName").Value);
            }
            catch
            {
                return BadRequest();
            }


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