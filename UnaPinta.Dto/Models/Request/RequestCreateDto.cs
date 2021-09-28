using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnaPinta.Dto.Models
{
    public class RequestCreateDto
    {
        [Required]
        public int? BloodComponentId { get; set; }

        [Required]
        public double? Amount { get; set; }

        public string Name { get; set; }
        [Required]
        public string CenterName { get; set; }

        [Required]
        public string CenterAddress { get; set; }

        public int BloodTypeId { get; set; }

        [MaxLength(11)]
        public string Document { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public string PrescriptionBase64 { get; set; }

        [Required]
        public IEnumerable<int> PossibleBloodTypes { get; set; }

        public string PatientStory { get; set; }

        [Required]
        public int ResponseDueDateId { get; set; }

        [Required]
        public bool ForMe { get; set; }
        public int ProvinceId { get; set; }
    }
}