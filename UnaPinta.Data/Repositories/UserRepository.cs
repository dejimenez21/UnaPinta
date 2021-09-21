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
    public class UserRepository : IUserRepository
    {
        private readonly UnaPintaDBContext _dbContext;

        public UserRepository(UnaPintaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> SelectDonorsForNotification(Request request)
        {
            var compatibleDonorsQuery = _dbContext.Users.Where(u => u.CanDonate && u.EmailConfirmed && request.PossibleBloodTypes.Any(p => p.BloodTypeId == u.BloodTypeId));
            return await compatibleDonorsQuery.ToListAsync();
        }
    }
}
