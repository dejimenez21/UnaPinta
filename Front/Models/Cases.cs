using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Models
{
    public class Cases
    {
        [Required]
        public long RequestId { get; set; }
        public long DonorId { get; set; }
    }
}
