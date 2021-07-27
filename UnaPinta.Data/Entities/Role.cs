using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public partial class Role : IdentityRole<int>
    {
        

        public Role()
        {
            
        }

        //[Key]
        //public RoleEnum Id { get; set; }
        [MaxLength(20)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; }

    }

}
