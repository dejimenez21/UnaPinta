using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Api.Entities
{
    public partial class Request
    {
        [Key]
        public int Id { get; set; }
        public int RequesterId { get; set; }
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
