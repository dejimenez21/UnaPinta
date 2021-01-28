using Api.Entities;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> loginManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> loginManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult<User>> Register(Register obj)
        {
            User user = mapper.Map<User>(obj);
            if (ModelState.IsValid)
            {
                //User user = mapper.Map<User>(obj);
                IdentityResult result = await userManager.CreateAsync(user, obj.Password);

                if (result.Succeeded)
                {
                    //Esto nunca va a llegar
                    /*if (!roleManager.RoleExistsAsync("NormalUser").Result)
                    {
                        Role Role = mapper.Map<Role>(obj);
                        IdentityResult roleResult = roleManager.CreateAsync(Role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("","Error while creating role!");
                            return Created("Api/AccountController/", user);
                        }
                    }*/

                    await userManager.AddToRoleAsync(user, "Donante");
                    return RedirectToAction("Login", "Account");
                }
            }
            return Created("Api/AccountController/", user);
        }
    }
}
