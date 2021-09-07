using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Dto.Models.Auth
{
    public class UserSignUpResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string BloodType { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public double? Weight { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
