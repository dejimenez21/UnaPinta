using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Data.Contracts
{
    public interface IRequestRepository : IRepositoryBase<Request, long>
    {
        Task<Request> SelectRequestById(int id);
        Task<IEnumerable<Request>> SelectRequestsByDonor(User donor);
        Task<StringDate> SelectStringDateById(int id);
        Task<IEnumerable<StringDate>> SelectAllStringDates();
        Task<IEnumerable<Request>> SelectRequestByRequesterId(long id, string filter = null);
        Task<IEnumerable<Request>> SelectRequestByRequester(string username, string filter = null);
    }
}
