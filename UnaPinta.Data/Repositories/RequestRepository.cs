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
                .Include(x => x.Prescription)
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

        public async Task<IEnumerable<Request>> SelectRequestByRequesterId(long id, string filter = null)
        {
            var requestQuery = dbSet.Where(r => r.RequesterId == id);
            if (!string.IsNullOrEmpty(filter))
            {
                requestQuery = requestQuery.Where(r => r.Name.StartsWith(filter));
            }

            return await requestQuery.ToListAsync();
        }

        public async Task<IEnumerable<Request>> SelectRequestByRequester(string username, string filter = null)
        {
            var requestQuery = dbSet.Where(r => r.RequesterNav.UserName == username && !r.DeletedAt.HasValue);
            if (!string.IsNullOrEmpty(filter))
            {
                requestQuery = requestQuery.Where(r => r.Name.StartsWith(filter));
            }

            return await requestQuery.Include(e => e.ProvinceNav).ToListAsync();
        }

        public Task<Request> SelectRequestForDonorById(long id, User donor)
        {
            Expression<Func<Request, bool>> where = r =>
                    r.Id == id
                    && r.ProvinceId == donor.ProvinceId
                    && r.PossibleBloodTypes.Select(p => p.BloodTypeId).Contains(donor.BloodTypeId);

            Func<IQueryable<Request>, IIncludableQueryable<Request, object>> includes = r => r
                    .Include(p => p.ProvinceNav)
                    .Include(p => p.PossibleBloodTypes);

            return base.SelectOneAsync(where, includes);
        }
    }
}
