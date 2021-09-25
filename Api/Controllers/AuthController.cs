using UnaPinta.Core.Contracts;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Core.Exceptions.Role;
using UnaPinta.Core.Exceptions;
using UnaPinta.Api.Filters;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnaPinta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> loginManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;
        private readonly IAuthenticationService _authService;
        private readonly IProvinceService _provinceService;

        public AuthController(IAuthenticationService authManager, UserManager<User> userManager, 
            SignInManager<User> loginManager, RoleManager<Role> roleManager, IProvinceService provinceService, 
            IMapper mapper)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            _authService = authManager;
            _provinceService = provinceService;
        }


        [HttpPost("signup")]
        public async Task <ActionResult<User>> SignUp(UserSignUp obj)
        {
            
            User user = mapper.Map<UserSignUp, User>(obj);
            var province = await _provinceService.RetrieveProvinceByCode(obj.ProvinceCode);

            if (province == null)
                return BadRequest("La provincia especificada no existe");

            user.ProvinceId = province.Id;
            
            var userCreationResult = await userManager.CreateAsync(user, obj.Password);
                
            if (userCreationResult.Succeeded)
            {

                IdentityResult userRoleResult = new IdentityResult();
                try
                {
                    userRoleResult = await userManager.AddToRoleAsync(user, obj.Role);
                }
                catch(Exception e)
                {
                    await userManager.DeleteAsync(user);
                    return BadRequest(e.Message);
                }
                    
                if(!userRoleResult.Succeeded)
                {
                    await userManager.DeleteAsync(user);
                    return Problem(userRoleResult.Errors.First().Description, null, 400);
                }

                //var link = string.Format("{0}://{1}{2}", Request.Scheme, HttpContext.Request.Host, Url.Action("ConfirmEmail"));
                var link = string.Format("{0}://{1}{2}", Request.Scheme, "localhost:44308", "/ConfirmEmail");

                Response.OnCompleted(async () => await _authService.SendEmailConfirmationAsync(user, link));
            }
            else
            {
                return Problem(userCreationResult.Errors.First().Description, null, 400);
            }

            return Created(Request.Path + $"/{user.UserName}", obj);

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Authenticate(UserLogin obj) =>
            Ok(new { Token = await _authService.AuthenticateAsync(obj) });


        [HttpPost("roles")]
        public async Task<ActionResult<Role>> CreateRole(RoleCreate roleCreate)
        {
            var newRole = await _authService.CreateRole(roleCreate);
            return Created(Request.Path + $"/{newRole.Name}", newRole);
        }

        [HttpPost("{username}/roles")]
        public async Task<ActionResult> AddUserToRole(string userName, RoleCreate roleName)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(userName);
                if (user == null)
                    return NotFound("The user specified doesn't exist");

                IdentityResult userRoleResult = new IdentityResult();
                try
                {
                    userRoleResult = await userManager.AddToRoleAsync(user, roleName.RoleName);
                }
                catch (Exception e)
                {
                    await userManager.DeleteAsync(user);
                    return BadRequest(e.Message);
                }

                if (!userRoleResult.Succeeded)
                {
                    return Problem(userRoleResult.Errors.First().Description, null, 500);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpGet("confirmemail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string token)
        {
            var newToken = await _authService.ConfirmEmailAsync(userId, token);
            return Ok(new { Token = newToken });
        }

        [Authorize]
        [HttpGet("sendemailverification")]
        public async Task<ActionResult> SendEmailVerification()
        {
            var link = string.Format("{0}://{1}{2}", Request.Scheme, "localhost:44308", "/ConfirmEmail");
            var userName = User.Claims.First(x => x.Type == "UserName").Value;
            var user = await userManager.FindByNameAsync(userName);

            await _authService.SendEmailConfirmationAsync(user, link);

            return Ok();
        }
    }
}
