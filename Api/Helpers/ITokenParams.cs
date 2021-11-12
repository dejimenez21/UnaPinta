using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnaPinta.Api.Helpers
{
    public interface ITokenParams
    {
        public string Name { get; }
        public string UserName { get; }
        public string EmailConfirmed { get; }
        public string BloodType { get; }
        public string BirthDate { get; }
    }
}
