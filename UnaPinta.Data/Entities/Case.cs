using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Data.Entities
{
    public class Case : BaseEntity<long>
    {
        public long DonorId { get; set; }
        public long RequestId { get; set; }
        public CaseStatusEnum StatusId { get; set; }

        [ForeignKey("DonorId")]
        public virtual User DonorNav { get; set; }

        [ForeignKey("RequestId")]
        public virtual Request RequestNav { get; set; }

        [ForeignKey("StatusId")]
        public virtual CaseStatus StatusNav { get; set; }
    }
}
