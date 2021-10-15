using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Core.Contracts
{
    public interface IWaitListServices
    {
        Task<DateTime> CalculateAvailableAtDate(ConditionEnum conditionId, int months);
        Task ReviewDonorAvailability(int userId, List<WaitList> waitList);
        Task<bool> IsDonorAvailable(User donor);

    }
}