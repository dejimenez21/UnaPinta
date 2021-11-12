using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnaPinta.Dto.Enums;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public partial class Request : BaseEntity<long>
    {
        public Request()
        {
            PossibleBloodTypes = new HashSet<RequestPossibleBloodTypes>();
        }

        public long RequesterId { get; set; }
        public BloodComponentEnum BloodComponentId { get; set; }
        public double? Amount { get; set; }
        public string Name { get; set; }
        public string CenterName { get; set; }
        [Required]
        public string CenterAddress { get; set; }
        public BloodTypeEnum BloodTypeId { get; set; }
        [MaxLength(11)]
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        public long PrescriptionImageId { get; set; }
        public string PatientStory { get; set; }
        [Required]
        public DateTime ResponseDueDate { get; set; }
        [Required]
        public int ProvinceId { get; set; }

        [ForeignKey("BloodComponentId")]
        public virtual BloodComponent BloodComponentNav { get; set; }
        [ForeignKey("RequesterId")]
        public virtual User RequesterNav { get; set; }
        [ForeignKey("BloodTypeId")]
        public virtual BloodType BloodTypeNav { get; set; }
        [ForeignKey("ProvinceId")]
        public virtual Province ProvinceNav { get; set; }
        [ForeignKey("PrescriptionImageId")]
        public virtual File Prescription { get; set; }
        public virtual ICollection<RequestPossibleBloodTypes> PossibleBloodTypes { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
    }
}
