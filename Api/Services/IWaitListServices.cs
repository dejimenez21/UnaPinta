using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Services
{
    public interface IWaitListServices
    {
        Task<DateTime> CalculateAvailableAtDate(WaitList item, int months);
        Task ReviewDonorAvailability(string userId, List<WaitList> waitList);

    }
}