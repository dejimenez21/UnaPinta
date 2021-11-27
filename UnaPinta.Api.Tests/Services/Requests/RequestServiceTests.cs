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
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using Xunit;

namespace UnaPinta.Api.Tests.Unit.Services.Requests
{
    public partial class RequestServiceTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IRequestRepository> _requestRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRequestNotificationService> _requestNotificationServiceMock;
        private readonly Mock<IProvinceService> _provinceServiceMock;
        private readonly Mock<ICaseRepository> _caseRepositoryMock;
        private readonly Mock<IFileRepository> _fileRepositoryMock;
        private readonly Mock<IDateTimeBroker> _dateTimeBrokerMock;

        private readonly IRequestsService _requestService;
        public RequestServiceTests()
        {
            _dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            var store = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _requestRepositoryMock = new Mock<IRequestRepository>();
            _requestNotificationServiceMock = new Mock<IRequestNotificationService>();
            _fileRepositoryMock = new Mock<IFileRepository>();
            _provinceServiceMock = new Mock<IProvinceService>();
            _caseRepositoryMock =  new Mock<ICaseRepository>();

            _requestService = new RequestsService(_userManagerMock.Object, _requestRepositoryMock.Object, GenerateRequestMapper(), _requestNotificationServiceMock.Object,
                _provinceServiceMock.Object, _caseRepositoryMock.Object, _fileRepositoryMock.Object, _dateTimeBrokerMock.Object);
        }

        [Fact]
        public async Task ShouldRegisterRequestAsync()
        {
            IMapper mapper = GenerateMapperForTests();
            //given
            DateTime actualDate = Convert.ToDateTime("2030-8-14");
            RequestCreateDto inputRequest = GetValidRequest();
            Request beforeInsertMappedRequest = mapper.Map<Request>(inputRequest);
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

        private User GenerateRequestOwner()
        {
            return new User();
        }

        private IMapper GenerateRequestMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<RequestMappingProfile>() );
            var mapper = new Mapper(config);
            return mapper;
        }

        private IMapper GenerateMapperForTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RequestCreateDto, Request>()
                    .ForMember(r => r.PossibleBloodTypes, opt => opt.Ignore());
                // other configurations
            });
            var mapper = config.CreateMapper();
            return mapper;
        }

        private RequestCreateDto GetValidRequest(bool random = false)
        {
            var validRequest = new RequestCreateDto();

            validRequest.Amount = 2;
            validRequest.BirthDate = Convert.ToDateTime("1999-05-21");
            validRequest.BloodComponentId = 2;
            validRequest.BloodTypeId = 3;
            validRequest.CenterAddress = "Av Romulo Betancourt, No. 452, El Millon";
            validRequest.CenterName = "Centro Medico Real";
            validRequest.Document = "";
            validRequest.ForMe = false;
            validRequest.Name = "Juan Hernandez";
            validRequest.PatientStory = "Tuve un accidente de transito y necesito cirugia.";
            validRequest.PossibleBloodTypes = new List<int> { 2, 3, 4 };
            validRequest.PrescriptionImage = GenerateIFormFile();
            validRequest.ProvinceCode = "SD";
            validRequest.ResponseDueDateId = 2;

            return validRequest;
        }

        private IFormFile GenerateIFormFile()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return fileMock.Object;
        }

        //private static Filler<Request> CreateRequestFilller(DateTime dates)
        //{
        //    var filler = new Filler<Request>();

        //    filler.Setup()
        //        .OnProperty(request => request.Amount).Use(Enumerable.Range(1, 10).Select(x => (double?)x))
        //        .OnProperty(request => request.BirthDate).Use(GetValidBirthDate(dates))
        //        .OnProperty(request => request.)
        //}

        private static DateTime GetAdultBirthDates() =>
            new DateTimeRange(new DateTime(), DateTime.Now.AddYears(-18)).GetValue();

        private static DateTime GetValidBirthDate(DateTime date) =>
            new DateTimeRange(new DateTime(), date).GetValue();
    }
}
