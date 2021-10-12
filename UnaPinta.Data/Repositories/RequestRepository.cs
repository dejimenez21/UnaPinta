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
    public class RequestRepository : RepositoryBase<Request, long>, IRequestRepository
    {

        public RequestRepository(UnaPintaDBContext dbContext) : base(dbContext)
        {

        }

        public void CreateRequest(Request request)
        {
            _dbContext.Requests.Add(request);
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

            return await requestQuery.ToListAsync();
        }
    }
}
