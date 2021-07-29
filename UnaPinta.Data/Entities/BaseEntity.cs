using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UnaPinta.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; }

    }
}
