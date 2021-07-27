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
        private readonly IAuthenticationManager _authManager;

        public AuthController(IAuthenticationManager authManager, UserManager<User> userManager, SignInManager<User> loginManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            _authManager = authManager;
        }


        [HttpPost("signup")]
        public async Task <ActionResult<User>> SignUp(UserSignUp obj)
        {
            if(ModelState.IsValid)
            {
                User user = mapper.Map<UserSignUp, User>(obj);
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
                       
                }
                else
                {
                    return Problem(userCreationResult.Errors.First().Description, null, 400);
                }

                return Created(Request.Path + $"/{user.UserName}", obj);

                
            }

            return BadRequest(ModelState);  
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Authenticate(UserLogin obj)
        {
            if (ModelState.IsValid)
            {
                
                if(!await _authManager.ValidateUser(obj))
                {
                    return Unauthorized();
                }

                return Ok(new { Token = await _authManager.CreateToken() });
            }

            return BadRequest(ModelState);
        }
    
        [HttpPost("roles")]
        public async Task<ActionResult<Role>> CreateRole(RoleCreate roleCreate)
        {
            if (ModelState.IsValid)
            {

                var roleName = roleCreate.RoleName;
                var newRole = new Role()
                {
                    Name = roleName,
                };

                var roleResult = await roleManager.CreateAsync(newRole);

                if (roleResult.Succeeded)
                    return Created(Request.Path + $"/{newRole.Name}", newRole);

                return Problem(roleResult.Errors.First().Description, null, 500);
            }
            else
            {
                return BadRequest(ModelState);
            }
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
    }
}
