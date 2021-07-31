using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnaPinta.Dto.Enums;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public partial class User : IdentityUser<long>
    {
        public User()
        {
            WaitLists = new HashSet<WaitList>();
            Requests = new HashSet<Request>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public BloodTypeEnum BloodTypeId { get; set; }
        public double? Weight { get; set; }
        public bool CanDonate { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; }


        [ForeignKey("BloodTypeId")]
        public virtual BloodType BloodTypeNav { get; set; }

        public virtual ICollection<WaitList> WaitLists { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        //public virtual ICollection<ConfirmationCode> ConfirmationCodes { get; set; }
    }
}
