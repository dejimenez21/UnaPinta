using System;
using Api.Entities;

namespace Api.Models
{
    public class WaitListItemCreate
    {
        public ConditionEnum ConditionId { get; set; }
        public int? Months { get; set; }
    }
}