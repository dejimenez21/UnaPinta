using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Api.Entities
{
    public partial class User : IdentityUser<int>
    {
        public User()
        {
            WaitLists = new HashSet<WaitList>();
            Requests = new HashSet<Request>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public bool Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public BloodTypeEnum BloodTypeId { get; set; }
        public double? Weight { get; set; }
        public bool CanDonate { get; set; } = false;


        [ForeignKey("BloodTypeId")]
        public virtual BloodType BloodTypeNav { get; set; }

        public virtual ICollection<WaitList> WaitLists { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        //public virtual ICollection<ConfirmationCode> ConfirmationCodes { get; set; }
    }
}
