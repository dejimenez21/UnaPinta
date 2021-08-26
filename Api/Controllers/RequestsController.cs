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

        [HttpPost("")]
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

    }
}