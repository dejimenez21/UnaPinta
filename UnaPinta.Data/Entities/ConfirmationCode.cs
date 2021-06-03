using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnaPinta.Data.Entities
{
    public class ConfirmationCode
    {
        public string Id { get; set; }
        [MaxLength(6)]
        [StringLength(6)]
        public string Code { get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddHours(2);
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserNav { get; set; }
    }
}