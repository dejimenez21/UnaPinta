using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnaPinta.Dto.Enums;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public partial class Request : BaseEntity
    {

        public long RequesterId { get; set; }
        public BloodComponentEnum BloodComponentId { get; set; }
        public double? Amount { get; set; }
        [MaxLength(500)]
        public string Location { get; set; }

        [ForeignKey("BloodComponentId")]
        public virtual BloodComponent BloodComponentNav { get; set; }
        [ForeignKey("RequesterId")]
        public virtual User RequesterNav { get; set; }
    }
}
