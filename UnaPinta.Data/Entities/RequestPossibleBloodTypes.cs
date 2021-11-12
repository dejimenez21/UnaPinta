using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Data.Entities
{
    public class RequestPossibleBloodTypes
    {
        public long RequestId { get; set; }
        public BloodTypeEnum BloodTypeId { get; set; }

        [ForeignKey("RequestId")]
        public virtual Request RequestNav { get; set; }
        [ForeignKey("BloodTypeId")]
        public virtual BloodType BloodTypeNav { get; set; }
    }
}
