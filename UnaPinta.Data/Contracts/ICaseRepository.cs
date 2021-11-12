using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Data.Contracts
{
    public interface ICaseRepository : IRepositoryBase<Case, long>
    {
        Task<IEnumerable<Case>> SelectCasesByRequestId(long id);
    }
}
