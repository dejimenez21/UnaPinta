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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using UnaPinta.Api.Tests.Unit.Helpers;
using UnaPinta.Core.Contracts;
using UnaPinta.Core.Exceptions;
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
        private readonly IMapper _autoMapper;
        private readonly Comparer<Request, long> _comparer;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IRequestRepository> _requestRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRequestNotificationService> _requestNotificationServiceMock;
        private readonly Mock<IProvinceService> _provinceServiceMock;
        private readonly Mock<ICaseRepository> _caseRepositoryMock;
        private readonly Mock<IFileRepository> _fileRepositoryMock;
        private readonly Mock<IDateTimeBroker> _dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> _logginBrokerMock;

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
            _logginBrokerMock = new Mock<ILoggingBroker>();

            _requestService = new RequestsService(_userManagerMock.Object, _requestRepositoryMock.Object, GenerateRequestMapper(), _requestNotificationServiceMock.Object,
                _provinceServiceMock.Object, _caseRepositoryMock.Object, _fileRepositoryMock.Object, _dateTimeBrokerMock.Object, _logginBrokerMock.Object);

            _autoMapper = GenerateMapperForTests();
            _comparer = new Comparer<Request, long>();
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

        private RequestCreateDto GetValidRequestCreateDto(bool random = false)
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

        private Expression<Func<BaseDomainException, bool>> SameDomainExceptionAs(BaseDomainException expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message &&
                actualException.StatusCode == expectedException.StatusCode &&
                actualException.Title == expectedException.Title;
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
