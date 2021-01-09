using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class RequestCreate
    {
        public int? RequesterId { get; set; }
        public int? BloodComponentId { get; set; }
        public double? Amount { get; set; }
        // [MaxLength(500)]
        // public string Location { get; set; }
    }
}