using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Api.Helpers;
using UnaPinta.Core.Contracts.Case;
using UnaPinta.Dto.Models.Case;

namespace UnaPinta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CasesController : ControllerBase
    {
        private readonly ICaseService _caseService;
        private readonly ITokenParams _tokenParams;

        public CasesController(ICaseService caseService, ITokenParams tokenParams)
        {
            _caseService = caseService;
            _tokenParams = tokenParams;
        }

        [HttpPost()]
        [Authorize(Roles = "donante")]
        public async Task<ActionResult<CaseDetailsDto>> Create([FromBody]CreateCaseDto createCaseDto)
        {
            var userName = _tokenParams.UserName;
            var caseDetails = await _caseService.CreateCase(createCaseDto, userName);
            return Created($"api/cases/{caseDetails.Id}", caseDetails); 
        }
    }
}
