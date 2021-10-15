using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnaPinta.Dto.Models;
using UnaPinta.Data.Entities;
using AutoMapper;
using UnaPinta.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using UnaPinta.Dto.Models.Request;
using UnaPinta.Api.Helpers;

namespace UnaPinta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RequestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRequestsService _service;
        private readonly ITokenParams _tokenParams;

        public RequestsController(IMapper mapper, IRequestsService service, ITokenParams tokenParams)
        {
            _mapper = mapper;
            _service = service;
            _tokenParams = tokenParams;
        }

        [HttpPost("")]
        [Authorize(Roles = "solicitante")]
        public async Task<ActionResult<RequestCreateDto>> CreateRequest([FromForm]RequestCreateDto requestCreate)
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
        [Authorize(Roles = "donante, solicitante")]
        public async Task<ActionResult<RequestDetailsDto>> GetRequestDetails(int id)
        {
            var details = await _service.RetrieveRequestDetailsById(id);
            return Ok(details);
        }

        [HttpGet("summary")]
        [Authorize(Roles = "donante")]
        public async Task<ActionResult<IEnumerable<RequestSummaryDto>>> GetRequestsSummary()
        {
            var username = HttpContext.User.FindFirst("UserName").Value;
            var requestsSummary = await _service.RetrieveRequestsSummaryByDonor(username);
            return Ok(requestsSummary);
        }

        [HttpGet("stringDates")]
        public async Task<ActionResult<IEnumerable<StringDate>>> GetStringDates() =>
            Ok(await _service.RetrieveAllStringDates());

        [HttpGet("datatable")]
        [Authorize(Roles ="solicitante")]
        public async Task<ActionResult<IEnumerable<RequestSummaryDto>>> GetRequestsForDatatable([FromQuery]string search = null)
        {
            var username = _tokenParams.UserName;
            var requests = await _service.RetrieveRequestsSummaryByRequester(username, search);
            return Ok(requests);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "solicitante")]
        public async Task<ActionResult> DeleteRequest(long id)
        {
            await _service.DeleteRequestById(id, _tokenParams.UserName);
            return Ok();
        }

        [HttpGet("withCases/{id}")]
        [Authorize(Roles = "solicitante")]
        public async Task<ActionResult<RequestCasesDto>> GetRequestDetailsForRequester(long id)
        {
            var username = _tokenParams.UserName;
            var requestCases = await _service.RetrieveRequestWithCases(id, username);
            return Ok(requestCases);
        }

        [HttpPut("markAsCompleted/{id}")]
        [Authorize(Roles = "solicitante")]
        public async Task<ActionResult> MarkAsCompleted(long id)
        {
            var username = _tokenParams.UserName;
            await _service.MarkRequestAsCompleted(id, username);
            return Ok();
        }
    }
}