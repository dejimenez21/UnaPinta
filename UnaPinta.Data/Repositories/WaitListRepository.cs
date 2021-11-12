using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;

namespace UnaPinta.Data.Repositories
{
    public class WaitListRepository : RepositoryBase<WaitList, long>, IWaitListRepository
    {
        public WaitListRepository(UnaPintaDBContext dbContext) : base(dbContext)
        {

        }
        public async Task<IEnumerable<WaitList>> SelectWaitListItemsByDonorId(long id)
        {
            var items = await dbSet.Where(x => x.UserId == id).ToListAsync();
            return items;
        }
    }
}
