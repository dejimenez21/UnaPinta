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
    
    public class RequestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRequestsService _service;

        public RequestsController(IMapper mapper, IRequestsService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("")]
        [Authorize(Roles = "solicitante")]
        public async Task<ActionResult<RequestCreate>> CreateRequest(RequestCreate requestCreate)
        {

            try
            {
                var callback = await _service.CreateRequest(requestCreate, HttpContext.User.FindFirst("UserName").Value);
                Response.OnCompleted(callback);

                return Created("api/requests", requestCreate);
            }
            catch
            {
                return BadRequest();
            }
            
        }


        [HttpGet("{id}/details")]
        [Authorize(Roles = "donante")]
        public async Task<ActionResult<RequestDetailsDto>> GetRequestDetails(int id)
        {
            var details = await _service.RetrieveRequestDetailsById(id);
            return Ok(details);
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