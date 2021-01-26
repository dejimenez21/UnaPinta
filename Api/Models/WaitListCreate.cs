using System.Collections.Generic;

namespace Api.Models
{
    public class WaitListCreate
    {
        public string UserId { get; set; }
        public List<WaitListItemCreate> Conditions { get; set; }
    }
}