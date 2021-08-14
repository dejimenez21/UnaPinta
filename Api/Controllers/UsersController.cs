using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Dto.Models;
using UnaPinta.Core.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UnaPinta.Data.Contracts;
using UnaPinta.Core.Contracts;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;

namespace UnaPinta.Api.Controllers
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
        public async Task<ActionResult<ConfirmationResponse>> ConfirmUser(long id, CodeSubmit code)
        {
            var userToConfirm = await _repo.GetUserById(id);
            if(userToConfirm == null)
                return NotFound("The user you are trying to confirm don't even exist");

            var response = await _services.ConfirmEmail(userToConfirm, code.Code);
            return Ok(response);
        }


    }
}