using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnaPinta.Dto.Enums;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public class BloodComponent
    {
        public BloodComponent()
        {
            Requests = new HashSet<Request>();
        }

        [Key]
        public BloodComponentEnum Id { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }


}
