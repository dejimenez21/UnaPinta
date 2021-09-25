using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Api.Models;
using UnaPinta.Data.Entities;
using UnaPinta.Core.Services;
using UnaPinta.Dto.Models;
using AutoMapper;
using UnaPinta.Core.Contracts;
using UnaPinta.Data.Contracts;
using UnaPinta.Dto.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace UnaPinta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitListController : ControllerBase
    {
        private readonly IWaitListServices _services;
        private readonly IMapper _mapper;
        private readonly IUnaPintaRepository _repo;
        private readonly UserManager<User> _userManager;

        public WaitListController(IWaitListServices services, IMapper mapper, IUnaPintaRepository repo, UserManager<User> userManager)
        {
            _services = services;
            _mapper = mapper;
            _repo = repo;
            _userManager = userManager;
        }

        [Authorize(Roles = "Donante")]
        [HttpPost("")]
        public async Task<ActionResult<WaitList>> CreateWaitListItem(WaitListCreate waitList)
        {
            var EntityWaitList = waitList.Conditions.Select(x=>_mapper.Map<WaitList>(x));

            var user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound("Error al crear el usuario");
            var userId = user.Id;

            foreach (var item in EntityWaitList.Where(x=>x.ConditionId!=ConditionEnum.SinCondicion))
            {
                item.UserId = userId;
                System.Console.WriteLine(item.ConditionId);
                System.Console.WriteLine(item.UserId);
                var months = waitList.Conditions.Single(x=>x.ConditionId==item.ConditionId).Months;
                if (months == null) months = 0;
                item.AvailableAt = await _services.CalculateAvailableAtDate(item, (int)months);
                _repo.AddWaitListItem(item);
            }
            //EntityItem.AvailableAt = await _services.CalculateAvailableAtDate(EntityItem, item.Months);

            await _repo.SaveChangesAsync();

            Response.OnCompleted(async () => await _services.ReviewDonorAvailability((int)userId, EntityWaitList.ToList()));

            return Created("/api/waitlist", new {userId, EntityWaitList});
        }

        
    }
}