using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Api.Entities
{
    public partial class User
    {
        public User()
        {
            WaitLists = new HashSet<WaitList>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public bool Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public BloodTypeEnum? BloodTypeId { get; set; }
        public string Email { get; set; }
        public double? Weight { get; set; }
        public string Phone { get; set; }
        public string Handle { get; set; }
        public string Password { get; set; }
        public bool? CanDonate { get; set; }
        public UserTypeEnum? UserTypeId { get; set; }
        public bool? Confirmed { get; set; } = false;

        [ForeignKey("BloodTypeId")]
        public virtual BloodType BloodTypeNav { get; set; }
        [ForeignKey("UserTypeId")]
        public virtual UserType UserTypeNav { get; set; }
        public virtual ICollection<WaitList> WaitLists { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
