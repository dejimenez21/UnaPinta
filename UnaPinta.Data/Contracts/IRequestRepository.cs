using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Data.Contracts
{
    public interface IRequestRepository
    {
        Task<Request> SelectRequestById(int id);
        Task<IEnumerable<Request>> SelectRequestsByDonor(User donor);
    }
}
