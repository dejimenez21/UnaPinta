using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Api.Entities
{
    public partial class Condition
    {
        public Condition()
        {
            WaitLists = new HashSet<WaitList>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Decription { get; set; }

        public virtual ICollection<WaitList> WaitLists { get; set; }
    }
}
