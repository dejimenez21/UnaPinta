using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Models
{
    public class RequestSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PatientStory { get; set; }
        public string Province { get; set; }
        public string CenterName { get; set; }
        public string CenterAddress { get; set; }
        public string ResponseDueDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
