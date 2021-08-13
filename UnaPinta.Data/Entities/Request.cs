﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnaPinta.Dto.Enums;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public partial class Request : BaseEntity
    {
        public Request()
        {
            PossibleBloodTypes = new HashSet<RequestPossibleBloodTypes>();
        }

        public long RequesterId { get; set; }
        public BloodComponentEnum BloodComponentId { get; set; }
        public double? Amount { get; set; }
        public string Name { get; set; }
        [Required]
        public string CenterAddress { get; set; }
        public int BloodTypeId { get; set; }
        [MaxLength(11)]
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        public string PrescriptionBase64 { get; set; }
        public string PatientStory { get; set; }
        [Required]
        public string ResponseDueDate { get; set; }
        
        [ForeignKey("BloodComponentId")]
        public virtual BloodComponent BloodComponentNav { get; set; }
        [ForeignKey("RequesterId")]
        public virtual User RequesterNav { get; set; }
        
        public ICollection<RequestPossibleBloodTypes> PossibleBloodTypes { get; set; }
    }
}
