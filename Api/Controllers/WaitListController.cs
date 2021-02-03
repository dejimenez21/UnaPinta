using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Api.Models;
using Api.Entities;
using Api.Services;
using Api.Models;
using AutoMapper;
using Api.Contracts;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitListController : ControllerBase
    {
        private readonly IWaitListServices _services;
        private readonly IMapper _mapper;
        private readonly IUnaPintaRepository _repo;

        public WaitListController(IWaitListServices services, IMapper mapper, IUnaPintaRepository repo)
        {
            _services = services;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost("")]
        public async Task<ActionResult<WaitList>> CreateWaitListItem(WaitListCreate waitList)
        {
            var EntityWaitList = waitList.Conditions.Select(x=>_mapper.Map<WaitList>(x));
            var userId = waitList.UserId;
            foreach (var item in EntityWaitList.Where(x=>x.ConditionId!=ConditionEnum.SinCondicion))
            {
                item.UserId = userId;
                System.Console.WriteLine(item.ConditionId);
                System.Console.WriteLine(item.UserId);
                var months = waitList.Conditions.Single(x=>x.ConditionId==item.ConditionId).Months;
                item.AvailableAt = await _services.CalculateAvailableAtDate(item, (int)months);
                _repo.AddWaitListItem(item);
            }
            //EntityItem.AvailableAt = await _services.CalculateAvailableAtDate(EntityItem, item.Months);

            await _repo.SaveChangesAsync();

            Response.OnCompleted(async () => await _services.ReviewDonorAvailability(userId, EntityWaitList.ToList()));

            return Created("/api/waitlist", new {userId, EntityWaitList});
        }

        
    }
}