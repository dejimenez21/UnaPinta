using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;

namespace UnaPinta.Data.Repositories
{
    public class CaseRepository : RepositoryBase<Case, long>, ICaseRepository
    {
        public CaseRepository(UnaPintaDBContext dBContext) : base(dBContext) { }

        public Task<IEnumerable<Case>> SelectCasesByRequestId(long id)
        {
            return SelectAsync(c => c.RequestId == id);
        }
    }
}
