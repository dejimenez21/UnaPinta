using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace UnaPinta.Data.Repositories
{
    public class RequestRepository : RepositoryBase<Request, long>, IRequestRepository
    {

        public RequestRepository(UnaPintaDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<Request> SelectRequestById(int id)
        {
            return await _dbContext.Requests
                .Include(x => x.RequesterNav)
                .Include(x => x.BloodComponentNav)
                .Include(x => x.BloodTypeNav)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Request>> SelectRequestsByDonor(User donor)
        {
            var requests = await _dbContext.Requests
                .Include(x => x.ProvinceNav)
                .Include(x => x.PossibleBloodTypes)
                .Where(r =>
                    r.ProvinceId == donor.ProvinceId
                    && r.PossibleBloodTypes.Select(p => p.BloodTypeId).Contains(donor.BloodTypeId)
                )
                .ToListAsync();
            return requests;
        }

        public async Task<StringDate> SelectStringDateById(int id)
        {
            return await _dbContext.StringDates.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<StringDate>> SelectAllStringDates()
        {
            return await _dbContext.StringDates.ToListAsync();
        }
    }
}
