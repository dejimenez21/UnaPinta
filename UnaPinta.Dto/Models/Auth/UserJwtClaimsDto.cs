using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Dto.Models.Auth
{
    public class UserJwtClaimsDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string EmailConfirmed { get; set; }
        public string BloodType { get; set; }
        public string BirthDate { get; set; }
        public string CanDonate { get; set; }
    }
}
