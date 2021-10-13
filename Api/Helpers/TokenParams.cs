using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnaPinta.Api.Helpers
{
    public class TokenParams : ITokenParams
    {
        public string Name { get; }
        public string UserName { get; }
        public string EmailConfirmed { get; }
        public string BloodType { get; }
        public string BirthDate { get; }

        public TokenParams(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor?.HttpContext?.User;
            if (!user.Claims.Any()) return;

            Name = user.FindFirst(p => p.Type == nameof(Name)).Value;
            UserName = user.FindFirst(p => p.Type == nameof(UserName)).Value;
            EmailConfirmed = user.FindFirst(p => p.Type == nameof(EmailConfirmed)).Value;
            BloodType = user.FindFirst(p => p.Type == nameof(BloodType)).Value;
            BirthDate = user.FindFirst(p => p.Type == nameof(BirthDate)).Value;
        }
    }
}
