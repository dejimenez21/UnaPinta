using AutoMapper;
using FluentAssertions;
using Force.DeepCloner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using UnaPinta.Core.Contracts;
using UnaPinta.Core.MappingProfiles;
using UnaPinta.Core.Services;
using UnaPinta.Data.Brokers.DateTimes;
using UnaPinta.Data.Brokers.Loggings;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;
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
            actualRequest.Should().BeEquivalentTo(beforeInsertMappedRequest);

            _requestRepositoryMock
                .Verify(broker => broker.Insert(beforeInsertMappedRequest), Times.Once);

            _requestRepositoryMock
                .Verify(broker => broker.SaveChangesAsync(), Times.Once);

        }
    }
}
