using System.ComponentModel.DataAnnotations;

namespace UnaPinta.Core.Models
{
    public class RequestCreate
    {
        [Required]
        public int? BloodComponentId { get; set; }
        [Required]
        public double? Amount { get; set; }
        // [MaxLength(500)]
        // public string Location { get; set; }
    }
}