using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Models.User;

namespace UnaPinta.Core.Contracts.Users
{
    public interface IUserService
    {
        Task<UserProfileDto> RetrieveUserProfile(string username);
    }
}
