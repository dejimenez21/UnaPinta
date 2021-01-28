using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Entities;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
//using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnaPintaRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUsersServices _services;

        public UsersController(IUnaPintaRepository repo, IMapper mapper, IUsersServices services)
        {
            _repo = repo;
            _mapper = mapper;
            _services = services;
        }


        [HttpPost("")]
        public async Task<ActionResult<User>> RegisterUser(Register register)
        {
            if(await _repo.GetUserByEmail(register.Email))
            {
                return Ok("Este correo ya esta en uso");
            }

            User user = _mapper.Map<User>(register);
            //user.CanDonate = user.RoleId == (int)RoleEnum.Donante;
            _repo.AddUser(user);
            await _repo.SaveChangesAsync();

            Response.OnCompleted(async () => await _services.SendConfirmationCode(user.Id));
            

            return Created("api/users", user);
        }

        [HttpPost("confirm/{id}")]
        public async Task<ActionResult<ConfirmationResponse>> ConfirmUser(int id, CodeSubmit code)
        {
            var userToConfirm = await _repo.GetUserById(id);
            if(userToConfirm == null)
                return NotFound("The user you are trying to confirm don't even exist");

            var response = await _services.ConfirmEmail(userToConfirm, code.Code);
            return Ok(response);
        }


    }
}