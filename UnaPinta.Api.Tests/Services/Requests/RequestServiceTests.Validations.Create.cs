using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Core.Exceptions.BloodType;
using UnaPinta.Core.Exceptions.Province;
using UnaPinta.Core.Exceptions.Request;
using UnaPinta.Core.Exceptions.User;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Enums;
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
        public async Task ShouldThrowValidationExceptionOnCreateWhenPatientInfoIsMissingAndForMeIsFalseAndLogItAsync(
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

        [Theory]
        [InlineData(BloodTypeEnum.ABminus, new BloodTypeEnum[] { BloodTypeEnum.ABplus, BloodTypeEnum.Aminus, BloodTypeEnum.Aplus, BloodTypeEnum.Ominus}, new string[] { "AB+", "A+" })]
        [InlineData(BloodTypeEnum.Oplus, new BloodTypeEnum[] { BloodTypeEnum.Bplus, BloodTypeEnum.Ominus }, new string[] { "B+" })]
        [InlineData(BloodTypeEnum.Aplus, new BloodTypeEnum[] { BloodTypeEnum.Aplus, BloodTypeEnum.Aminus, BloodTypeEnum.Bplus, BloodTypeEnum.Bminus}, new string[] { "B+", "B-" })]
        [InlineData(BloodTypeEnum.Bminus, new BloodTypeEnum[] { BloodTypeEnum.ABplus, BloodTypeEnum.Aminus, BloodTypeEnum.Aplus, BloodTypeEnum.Ominus}, new string[] { "AB+", "A-", "A+" })]
        public async Task ShouldThrowValidationExceptionOnCreateWhenPossibleBloodTypesAreNotCompatibleWithPatientsBloodTypeAndLogItAsync(
            BloodTypeEnum patienBloodTypeId, IEnumerable<BloodTypeEnum> requestedBloodTypesIds, IEnumerable<string> incompatiblesBloodTypesIds)
        {
            //given
            RequestCreateDto inputRequest = GetValidRequestCreateDto();
            inputRequest.BloodTypeId = (int)patienBloodTypeId;
            inputRequest.PossibleBloodTypes = requestedBloodTypesIds.Select(x => (int)x);

            string inputUserName = "f.diaz";

            var incompatiblesBloodTypes = incompatiblesBloodTypesIds.Select(x => (BloodTypeEnumeration)x);
            var patientBloodType = (BloodTypeEnumeration)(int)patienBloodTypeId;
            var expectedException = new IncompatibleBloodTypesException(incompatiblesBloodTypes, patientBloodType);

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
            await Assert.ThrowsAsync<IncompatibleBloodTypesException>(() => createRequestTask);

            _logginBrokerMock.Verify(broker => broker.LogError(It.Is(SameDomainExceptionAs(expectedException))), Times.Once);
            _requestRepositoryMock.Verify(broker => broker.SelectStringDateById(2), Times.Once);
            _requestRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData("1998-02-12", "1998-02-11")]
        [InlineData("2025-02-12", "2023-02-13")]
        public async Task ShouldThrowValidationExceptionOnCreateWhenPatientBirthDateIsLaterThanActualDateAndLogItAsync(DateTime patientBirthDate, DateTime actualDate)
        {
            //given
            RequestCreateDto inputRequest = GetValidRequestCreateDto();
            inputRequest.BirthDate = patientBirthDate;

            string inputUserName = "f.diaz";

            var expectedException = new InvalidBirthDateException();

            _userManagerMock
                .Setup(broker => broker.FindByNameAsync(inputUserName))
                .ReturnsAsync(new User { Id = 1, UserName = "dejimenez21" });

            _requestRepositoryMock
                .Setup(broker => broker.SelectStringDateById(2))
                .ReturnsAsync(new StringDate());

            _provinceServiceMock
                .Setup(broker => broker.RetrieveProvinceByCode("SD"))
                .ReturnsAsync(new Province());

            _dateTimeBrokerMock
                .Setup(broker => broker.GetCurrentDateTime())
                .Returns(actualDate);

            //when
            Task<Request> createRequestTask = _requestService.CreateRequest(inputRequest, inputUserName);

            //then
            await Assert.ThrowsAsync<InvalidBirthDateException>(() => createRequestTask);

            _logginBrokerMock.Verify(broker => broker.LogError(It.Is(SameDomainExceptionAs(expectedException))), Times.Once);
            _requestRepositoryMock.Verify(broker => broker.SelectStringDateById(2), Times.Once);
            _requestRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
