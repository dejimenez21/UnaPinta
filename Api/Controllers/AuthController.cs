using Api.Entities;
using Api.Models;
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


        [HttpPost]
        public async Task <ActionResult<User>> SingUp(UserSignUp obj)
        {
            if(ModelState.IsValid)
            {
                User user = mapper.Map<UserSignUp, User>(obj);
                var userCreationResult = await userManager.CreateAsync(user, obj.Password);
                
                if (userCreationResult.Succeeded)
                {
                    if(obj.Role != null)
                    {
                        var userCreaated = await userManager.FindByNameAsync(user.UserName);
                        await userManager.AddToRoleAsync(userCreaated, obj.Role);
                    }

                        
                }
                else
                {
                    return Problem(userCreationResult.Errors.First().Description, null, 500);
                }

                return Created($"Api/AccountController/{user.Id}", user);

                
            }

            return BadRequest(ModelState);  
        }

        [HttpPost]
        public async Task<ActionResult<User>> SingIn(UserLogin obj)
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
    }
}
