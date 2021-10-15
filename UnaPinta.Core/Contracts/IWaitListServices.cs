using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Core.Contracts
{
    public interface IWaitListServices
    {
        Task<DateTime> CalculateAvailableAtDate(WaitList item, int months);
        Task ReviewDonorAvailability(int userId, List<WaitList> waitList);
        Task<bool> IsDonorAvailable(User donor);

    }
}