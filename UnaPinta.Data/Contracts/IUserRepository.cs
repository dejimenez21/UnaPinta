using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Data.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> SelectDonorsForNotification(Request request);
        Task<int> SaveChangesAsync();
        void Update(User user);
    }
}
