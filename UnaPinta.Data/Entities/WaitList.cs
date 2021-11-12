using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnaPinta.Dto.Enums;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public partial class WaitList : BaseEntity<long>
    {
        public ConditionEnum ConditionId { get; set; }
        public long UserId { get; set; }
        public DateTime AvailableAt { get; set; }

        [ForeignKey("ConditionId")]
        public virtual Condition ConditionNav { get; set; }
        [ForeignKey("UserId")]
        public virtual User UserNav { get; set; }
    }
}
