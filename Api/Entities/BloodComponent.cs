using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Api.Entities
{
    public partial class BloodComponent
    {
        public BloodComponent()
        {
            Requests = new HashSet<Request>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
