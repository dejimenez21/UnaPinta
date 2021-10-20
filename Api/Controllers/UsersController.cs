using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Api.Helpers;
using UnaPinta.Core.Contracts.Users;
using UnaPinta.Core.Services;
using UnaPinta.Dto.Models.User;

namespace UnaPinta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenParams _tokenParams;

        public UsersController(IUserService userService, ITokenParams tokenParams)
        {
            _userService = userService;
            _tokenParams = tokenParams;
        }

        [HttpGet("myprofile")]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> GetMyProfile()
        {
            var username = _tokenParams.UserName;
            var profile = await _userService.RetrieveUserProfile(username);
            return Ok(profile);
        }
    }
}
