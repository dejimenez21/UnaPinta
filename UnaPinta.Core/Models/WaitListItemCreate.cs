using System;
using UnaPinta.Data.Entities;

namespace UnaPinta.Core.Models
{
    public class WaitListItemCreate
    {
        public ConditionEnum ConditionId { get; set; }
        public int? Months { get; set; }
    }
}