using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Contracts
{
    public interface IWaitListServices
    {
        Task<DateTime> CalculateAvailableAtDate(WaitList item, int months);
        Task ReviewDonorAvailability(int userId, List<WaitList> waitList);

    }
}