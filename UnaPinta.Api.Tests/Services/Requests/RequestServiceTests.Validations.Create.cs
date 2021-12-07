using Moq;
using System;
using System.Threading.Tasks;
using UnaPinta.Core.Exceptions.Province;
using UnaPinta.Core.Exceptions.Request;
using UnaPinta.Core.Exceptions.User;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using Xunit;

namespace UnaPinta.Api.Tests.Unit.Services.Requests
{
    public partial class RequestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnCreateWhenUserNotFoundAndLogItAsync()
        {
            //given
            RequestCreateDto inputRequest = GetValidRequestCreateDto();
            string inputUserName = "f.diaz";

            var expectedException = new UserNotFoundException(inputUserName, true);

            _requestRepositoryMock
                .Setup(broker => broker.SelectStringDateById(2))
                .ReturnsAsync(new StringDate());

            _provinceServiceMock
                .Setup(broker => broker.RetrieveProvinceByCode("SD"))
                .ReturnsAsync(new Province());

            //when
            Task<Request> createRequestTask = _requestService.CreateRequest(inputRequest, inputUserName);

            //then
            await Assert.ThrowsAsync<UserNotFoundException>(() => createRequestTask);

            _logginBrokerMock.Verify(broker => broker.LogError(It.Is(SameDomainExceptionAs(expectedException))), Times.Once);
            _requestRepositoryMock.Verify(broker => broker.SelectStringDateById(2), Times.Once);
            _requestRepositoryMock.VerifyNoOtherCalls();
        }


        [Fact]
        public async Task ShouldThrowValidationExceptionOnCreateWhenResponseDueDateNotFoundAndLogItAsync()
        {
            //given
            RequestCreateDto inputRequest = GetValidRequestCreateDto();
            string inputUserName = "f.diaz";

            var expectedException = new StringDateNotFoundException((int)inputRequest.ResponseDueDateId);

            _provinceServiceMock
                .Setup(broker => broker.RetrieveProvinceByCode("SD"))
                .ReturnsAsync(new Province());

            //when
            Task<Request> createRequestTask = _requestService.CreateRequest(inputRequest, inputUserName);

            //then
            await Assert.ThrowsAsync<StringDateNotFoundException>(() => createRequestTask);

            _logginBrokerMock.Verify(broker => broker.LogError(It.Is(SameDomainExceptionAs(expectedException))), Times.Once);
            _requestRepositoryMock.Verify(broker => broker.SelectStringDateById(2), Times.Once);
            _requestRepositoryMock.VerifyNoOtherCalls();
        }


        [Fact]
        public async Task ShouldThrowValidationExceptionOnCreateWhenProvinceNotFoundAndLogItAsync()
        {
            //given
            RequestCreateDto inputRequest = GetValidRequestCreateDto();
            string inputUserName = "f.diaz";

            var expectedException = new ProvinceNotFoundException(inputRequest.ProvinceCode);

            _requestRepositoryMock
                .Setup(broker => broker.SelectStringDateById(2))
                .ReturnsAsync(new StringDate());

            //when
            Task<Request> createRequestTask = _requestService.CreateRequest(inputRequest, inputUserName);

            //then
            await Assert.ThrowsAsync<ProvinceNotFoundException>(() => createRequestTask);

            _logginBrokerMock.Verify(broker => broker.LogError(It.Is(SameDomainExceptionAs(expectedException))), Times.Once);
            _requestRepositoryMock.Verify(broker => broker.SelectStringDateById(2), Times.Once);
            _requestRepositoryMock.VerifyNoOtherCalls();
            _provinceServiceMock.Verify(broker => broker.RetrieveProvinceByCode(inputRequest.ProvinceCode), Times.Once);
            _provinceServiceMock.VerifyNoOtherCalls();
        }

        
        [Theory]
        [InlineData("Daniel", null, null, false, true, true)]
        [InlineData("", null, null, true, true, true)]
        [InlineData(null, null, 2, true, true, false)]
        [InlineData("Pedro", "2017-12-4", null, false, false, true)]
        public async Task ShouldThrowValidationExceptionOnCreateWhenPacientInfoIsMissingAndForMeIsFalseAndLogItAsync(
            string patientName, DateTime patientBirthDate, int? patientBloodType,
            bool nameResult, bool birthDateResult, bool bloodTypeResult)
        {
            //given
            RequestCreateDto inputRequest = GetMissingPatientInfoRequestCreateDto(false);
            inputRequest.Name = patientName;
            inputRequest.BirthDate = patientBirthDate;
            inputRequest.BloodTypeId = patientBloodType;

            string inputUserName = "f.diaz";

            var expectedException = new PatientDataMissingException(nameResult, birthDateResult, bloodTypeResult);

            _userManagerMock
                .Setup(broker => broker.FindByNameAsync(inputUserName))
                .ReturnsAsync(new User { Id = 1, UserName = "dejimenez21" });

            _requestRepositoryMock
                .Setup(broker => broker.SelectStringDateById(2))
                .ReturnsAsync(new StringDate());

            _provinceServiceMock
                .Setup(broker => broker.RetrieveProvinceByCode("SD"))
                .ReturnsAsync(new Province());

            //when
            Task<Request> createRequestTask = _requestService.CreateRequest(inputRequest, inputUserName);

            //then
            await Assert.ThrowsAsync<PatientDataMissingException>(() => createRequestTask);

            _logginBrokerMock.Verify(broker => broker.LogError(It.Is(SameDomainExceptionAs(expectedException))), Times.Once);
            _requestRepositoryMock.Verify(broker => broker.SelectStringDateById(2), Times.Once);
            _requestRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
