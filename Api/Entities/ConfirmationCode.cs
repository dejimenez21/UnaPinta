using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public class ConfirmationCode
    {
        public string Id { get; set; }
        [MaxLength(6)]
        [StringLength(6)]
        public string Code { get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddHours(2);
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserNav { get; set; }
    }
}