using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnaPinta.Data.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly UnaPintaDBContext _dbContext;

        public RequestRepository(UnaPintaDBContext dbContext)
        {
            _dbContext = dbContext;   
        }
        public async Task<Request> SelectRequestById(int id)
        {
            return await _dbContext.Requests.SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}
