using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Core.Exceptions.User;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using Xunit;

namespace UnaPinta.Api.Tests.Unit.Services.Requests
{
    public partial class RequestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnCreateWhenUserNameNotFound()
        {
            //given
            RequestCreateDto inputRequest = GetValidRequestCreateDto();
            string inputUserName = "f.diaz";

            var expectedException = new UserNotFoundException(inputUserName, true);

            //when
            Task<Request> createRequestTask = _requestService.CreateRequest(inputRequest, inputUserName);

            //then
            await Assert.ThrowsAsync<UserNotFoundException>(() => createRequestTask);

            _logginBrokerMock.Verify(broker => broker.LogError(expectedException));
        }
    }
}
