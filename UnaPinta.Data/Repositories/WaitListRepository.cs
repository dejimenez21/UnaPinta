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
    public class WaitListRepository : IWaitListRepository
    {
        private readonly UnaPintaDBContext _context;

        public WaitListRepository(UnaPintaDBContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<WaitList>> SelectWaitListItemsByDonorId(long id)
        {
            var items = await _context.WaitLists.Where(x => x.UserId == id).ToListAsync();
            return items;
        }
    }
}
