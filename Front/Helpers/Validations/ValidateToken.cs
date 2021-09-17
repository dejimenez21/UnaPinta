using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Helpers.Validations
{
    public static class ValidateToken
    {
        public static bool VerifiedToken(JwtSecurityToken token)
        {
            if (token.Claims.First(c => c.Type == "EmailConfirmed").Value.Contains("False"))
            {
                return false;
            }

            return true;
        }
    }
}
