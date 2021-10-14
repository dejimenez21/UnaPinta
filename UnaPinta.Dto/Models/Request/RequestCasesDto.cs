using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Models.Case;

namespace UnaPinta.Dto.Models.Request
{
    public class RequestCasesDto
    {
        public RequestDetailsDto Request { get; set; }
        public IEnumerable<CaseForRequestDto> Cases { get; set; }
        public string Status { get; set; }
    }
}
