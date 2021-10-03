using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Data.Entities
{
    public class CaseStatus : BaseEntity<CaseStatusEnum>
    {
        public string Description { get; set; }
    }
}
