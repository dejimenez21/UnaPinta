using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnaPinta.Dto.Enums;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public class Condition
    {
        public Condition()
        {
            WaitLists = new HashSet<WaitList>();
        }

        [Key]
        public ConditionEnum Id { get; set; }
        [MaxLength(500)]
        public string Decription { get; set; }
        public int MonthsToWait { get; set; }

        public virtual ICollection<WaitList> WaitLists { get; set; }
    }

    
}
