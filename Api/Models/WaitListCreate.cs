using System.Collections.Generic;

namespace Api.Models
{
    public class WaitListCreate
    {
        public int DonorId { get; set; }
        public List<WaitListItemCreate> Conditions { get; set; }
    }
}