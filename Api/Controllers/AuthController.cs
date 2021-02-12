using Api.Entities;
using Api.Models;
using Api.Models.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> loginManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;

        public AuthController(UserManager<User> userManager, SignInManager<User> loginManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
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
                        return Problem(userRoleResult.Errors.First().Description, null, 500);
                    }
                       
                }
                else
                {
                    return Problem(userCreationResult.Errors.First().Description, null, 500);
                }

                return Created(Request.Path + $"/{user.UserName}", obj);

                
            }

            return BadRequest(ModelState);  
        }

        [HttpPost]
        public async Task<ActionResult<User>> SignIn(UserLogin obj)
        {
            if (ModelState.IsValid)
            {
                
                var user = await userManager.Users.SingleOrDefaultAsync(u => u.UserName == obj.UserName);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var userSingInResult = await userManager.CheckPasswordAsync(user, obj.Password);

                if(userSingInResult)
                {
                    return Ok();
                }

                return BadRequest("Email or password incorrect.");
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
