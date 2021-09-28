using System;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Dto.Models
{
    public class WaitListItemCreate
    {
        public ConditionEnum ConditionId { get; set; }
        public int? Months { get; set; }
    }
}