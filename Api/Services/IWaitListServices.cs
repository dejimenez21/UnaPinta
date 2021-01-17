using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Services
{
    public interface IWaitListServices
    {
        Task<DateTime> CalculateAvailableAtDate(WaitList item, int months);
        Task ReviewDonorAvailability(int userId, List<WaitList> waitList);

    }
}