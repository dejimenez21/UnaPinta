using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Models.Case;

namespace UnaPinta.Core.Contracts.Case
{
    public interface ICaseService
    {
        Task<CaseDetailsDto> CreateCase(CreateCaseDto inputCase, string userName);

        Task<CaseDetailsDto> RetrieveCaseDetails(long id);

        Task<CaseForRequestDto> MarkCaseAsCompleted(long id, string requesterUsername);
    }
}
