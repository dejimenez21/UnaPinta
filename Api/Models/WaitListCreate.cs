using System.Collections.Generic;

namespace Api.Models
{
    public class WaitListCreate
    {
        public int UserId { get; set; }
        public List<WaitListItemCreate> Conditions { get; set; }
    }
}