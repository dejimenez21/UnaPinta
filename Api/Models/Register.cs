using System;
using Api.Entities;

namespace Api.Models
{
    public class Register
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Handle { get; set; }
        public string Password { get; set; }
        public UserTypeEnum? UserTypeId { get; set; }
        //Data solo de donante
        public string Address { get; set; }
        public int BloodTypeId { get; set; }
        public double? Weight { get; set; }
    }
}