using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Core.Contracts;
using UnaPinta.Data.Contracts;
using Xunit;

namespace UnaPinta.Api.Tests.Unit.Services.Requests
{
    public partial class RequestServiceTests
    {
        private readonly Mock<IUnaPintaRepository> _unaPintaRepoMock;
        private readonly Mock<IRequestRepository> _requestRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRequestNotificationService> _requestNotificationServiceMock;
        private readonly Mock<IProvinceService> _provinceServiceMock;
        private readonly Mock<ICaseRepository> _caseRepositoryMock;
        private readonly Mock<IFileRepository> _fileRepositoryMock;
        public RequestServiceTests()
        {

        }

        [Fact]
        public async Task ShouldRegisterRequestAsync()
        {
            //given

        }
    }
}
