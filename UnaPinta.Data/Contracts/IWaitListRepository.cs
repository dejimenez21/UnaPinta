using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Data.Contracts
{
    public interface IWaitListRepository : IRepositoryBase<WaitList, long>
    {
        Task<IEnumerable<WaitList>> SelectWaitListItemsByDonorId(long id);
    }
}
