using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Api.Entities
{
    public partial class BloodType
    {
        public BloodType()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public BloodTypeEnum Id { get; set; }
        [MaxLength(3)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

    public enum BloodTypeEnum{
        [Display(Name = "A+")]
        Aplus = 1,
        [Display(Name = "A-")]
        Aminus = 2,
        [Display(Name = "B+")]
        Bplus = 3,
        [Display(Name = "B-")]
        Bminus = 4,
        [Display(Name = "AB+")]
        ABplus = 5,
        [Display(Name = "AB-")]
        ABminus = 6,
        [Display(Name = "O+")]
        Oplus = 7,
        [Display(Name = "O-")]
        Ominus = 8
    }
}
