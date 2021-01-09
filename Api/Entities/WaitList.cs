using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Api.Entities
{
    public partial class WaitList
    {
        [Key]
        public int Id { get; set; }
        public ConditionEnum? ConditionId { get; set; }
        public int? UserId { get; set; }
        public DateTime AvailableAt { get; set; }

        [ForeignKey("ConditionId")]
        public virtual Condition ConditionNav { get; set; }
        [ForeignKey("UserId")]
        public virtual User UserNav { get; set; }
    }
}
