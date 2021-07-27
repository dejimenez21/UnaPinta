using System.Collections.Generic;

namespace UnaPinta.Dto.Models
{
    public class WaitListCreate
    {
        public int UserId { get; set; }
        public List<WaitListItemCreate> Conditions { get; set; }
    }
}