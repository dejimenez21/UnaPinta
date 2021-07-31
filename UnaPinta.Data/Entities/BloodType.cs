using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnaPinta.Dto.Enums;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public partial class BloodType
    {
        public BloodType()
        {
            Users = new HashSet<User>();
            Requests = new HashSet<RequestPossibleBloodTypes>();
        }

        [Key]
        public BloodTypeEnum Id { get; set; }
        [MaxLength(3)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<RequestPossibleBloodTypes> Requests { get; set; }
    }

    
}
