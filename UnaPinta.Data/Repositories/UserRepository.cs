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
            var sql = @"SELECT u.* FROM Users u
                            INNER JOIN UserRoles ur ON ur.UserId = u.Id AND ur.RoleId = 1
                            INNER JOIN Requests r ON r.Id = @requestId AND r.ProvinceId = u.ProvinceId
                            INNER JOIN RequestPossibleBloodTypes rp ON rp.RequestId = r.Id AND rp.BloodTypeId = u.BloodTypeId
                        WHERE u.EmailConfirmed = 1 AND u.CanDonate = 1";

            var donors = _dbContext.Users.FromSqlRaw(sql, new { requestId = request.Id });

            return await donors.ToListAsync();
        }
    }
}
