using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Models.Donor;

namespace UnaPinta.Dto.Models.Case
{
    public class CaseDetailsDto
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public DonorInfoDto Donor { get; set; }
        public RequestDetailsDto Request { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
