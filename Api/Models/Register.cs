using System;

namespace Api.Models
{
    public class Register
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Handle { get; set; }
        public string Password { get; set; }
        public bool? Confirmed { get; set; } = false;
    }
}