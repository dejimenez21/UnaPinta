using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Dto.Models.Request
{
    public class RequestSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PatientStory { get; set; }
        public string Province { get; set; }
        public string CenterName { get; set; }
        public string CenterAddress { get; set; }
        public string ResponseDueDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
