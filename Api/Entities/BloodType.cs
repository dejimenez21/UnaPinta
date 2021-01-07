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
        public int Id { get; set; }
        [MaxLength(3)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
