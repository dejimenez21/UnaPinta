using FluentAssertions;
using Force.DeepCloner;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Enums;
using UnaPinta.Dto.Models;
using Xunit;

namespace UnaPinta.Api.Tests.Unit.Services.Requests
{
    public partial class RequestServiceTests
    {
        [Fact]
        public async Task ShouldRegisterRequestAsync()
        {
            //given
            DateTime actualDate = Convert.ToDateTime("2030-8-14");
            RequestCreateDto inputRequest = GetValidRequestCreateDto();
            Request beforeInsertMappedRequest = _autoMapper.Map<Request>(inputRequest);
            beforeInsertMappedRequest.ResponseDueDate = Convert.ToDateTime("2030-8-14 12:00:00.000");
            beforeInsertMappedRequest.ProvinceId = 30;
            beforeInsertMappedRequest.RequesterId = 1;
            beforeInsertMappedRequest.CreatedAt = actualDate;
            beforeInsertMappedRequest.LastUpdatedAt = actualDate;
            beforeInsertMappedRequest.Prescription =
                new Data.Entities.File { Extension = ".png", FileContent = new byte[] { }, FileName = "test.png", Id = 0L };
            beforeInsertMappedRequest.PossibleBloodTypes =
                new List<RequestPossibleBloodTypes>
                {
                    new RequestPossibleBloodTypes{BloodTypeId=Dto.Enums.BloodTypeEnum.Aminus},
                    new RequestPossibleBloodTypes{BloodTypeId=Dto.Enums.BloodTypeEnum.Bplus},
                    new RequestPossibleBloodTypes{BloodTypeId=Dto.Enums.BloodTypeEnum.Bminus}
                };
            Request expectedRequest = beforeInsertMappedRequest.DeepClone();

            var inputUserName = "j.hernandez";
            var storedUser = new User
            {
                Id = 1
            };

            _dateTimeBrokerMock
                .Setup(broker => broker.GetCurrentDateTime())
                .Returns(actualDate);

            _userManagerMock
                .Setup(broker => broker.FindByNameAsync(inputUserName))
                .ReturnsAsync(storedUser);

            _provinceServiceMock
                .Setup(broker => broker.RetrieveProvinceByCode("SD"))
                .ReturnsAsync(new Province { Id = 30, Code = "SD", Name = "Santo Domingo" });

            _requestRepositoryMock
                .Setup(broker => broker.SelectStringDateById(2))
                .ReturnsAsync(new StringDate { Id = 2, Hours = 12, String = "2 horas" });

            //when
            var actualRequest = await _requestService.CreateRequest(inputRequest, inputUserName);

            //then
            actualRequest.Should().BeEquivalentTo(expectedRequest);

            _requestRepositoryMock
                .Verify(broker => broker.Insert(It.Is(beforeInsertMappedRequest, _comparer)), Times.Once);

            _requestRepositoryMock
                .Verify(broker => broker.SaveChangesAsync(), Times.Once);

            _requestRepositoryMock
                .Verify(broker => broker.SelectStringDateById(2), Times.Once);

            _requestRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData("Pedro", "Sanchez", "2006-05-21", BloodTypeEnum.Bplus, "2025-06-14")]
        [InlineData("Joaquin", "Hernandez", "2030-04-13", BloodTypeEnum.ABminus, "2100-07-25")]
        public async Task ShouldRegisterRequestWhenForMeIsTrueAsync(string requesterFirstName, string requesterLastName, 
            DateTime requesterBirthDate, BloodTypeEnum requesterBloodTypeId, DateTime actualDate)
        {
            //given
            RequestCreateDto inputRequest = GetValidForMeRequestCreateDto();
            Request beforeInsertMappedRequest = _autoMapper.Map<Request>(inputRequest);
            beforeInsertMappedRequest.Name = $"{requesterFirstName} {requesterLastName}";
            beforeInsertMappedRequest.BirthDate = requesterBirthDate;
            beforeInsertMappedRequest.BloodTypeId = requesterBloodTypeId;
            beforeInsertMappedRequest.ResponseDueDate = actualDate.AddHours(12);
            beforeInsertMappedRequest.ProvinceId = 30;
            beforeInsertMappedRequest.RequesterId = 1;
            beforeInsertMappedRequest.CreatedAt = actualDate;
            beforeInsertMappedRequest.LastUpdatedAt = actualDate;
            beforeInsertMappedRequest.Prescription =
                new Data.Entities.File { Extension = ".png", FileContent = new byte[] { }, FileName = "test.png", Id = 0L };
            beforeInsertMappedRequest.PossibleBloodTypes =
                new List<RequestPossibleBloodTypes>
                {
                    new RequestPossibleBloodTypes{BloodTypeId=Dto.Enums.BloodTypeEnum.Aminus},
                    new RequestPossibleBloodTypes{BloodTypeId=Dto.Enums.BloodTypeEnum.Bplus},
                    new RequestPossibleBloodTypes{BloodTypeId=Dto.Enums.BloodTypeEnum.Bminus}
                };
            Request expectedRequest = beforeInsertMappedRequest.DeepClone();

            var inputUserName = "j.hernandez";
            var storedUser = new User
            {
                Id = 1,
                FirstName = requesterFirstName,
                LastName =requesterLastName,
                BirthDate = requesterBirthDate,
                BloodTypeId = requesterBloodTypeId
            };

            _dateTimeBrokerMock
                .Setup(broker => broker.GetCurrentDateTime())
                .Returns(actualDate);

            _userManagerMock
                .Setup(broker => broker.FindByNameAsync(inputUserName))
                .ReturnsAsync(storedUser);

            _provinceServiceMock
                .Setup(broker => broker.RetrieveProvinceByCode("SD"))
                .ReturnsAsync(new Province { Id = 30, Code = "SD", Name = "Santo Domingo" });

            _requestRepositoryMock
                .Setup(broker => broker.SelectStringDateById(2))
                .ReturnsAsync(new StringDate { Id = 2, Hours = 12, String = "2 horas" });

            //when
            var actualRequest = await _requestService.CreateRequest(inputRequest, inputUserName);

            //then
            actualRequest.Should().BeEquivalentTo(expectedRequest);

            _requestRepositoryMock
                .Verify(broker => broker.Insert(It.Is(beforeInsertMappedRequest, _comparer)), Times.Once);

            _requestRepositoryMock
                .Verify(broker => broker.SaveChangesAsync(), Times.Once);

            _requestRepositoryMock
                .Verify(broker => broker.SelectStringDateById(2), Times.Once);

            _requestRepositoryMock.VerifyNoOtherCalls();
        }
    }
}