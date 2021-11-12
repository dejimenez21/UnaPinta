using MimeKit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Core.Contracts;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;
using UnaPinta.Core.Extensions;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Core.Services
{
    public class RequestNotificationService : IRequestNotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailBroker _emailBroker;
        private readonly IEmailService _emailService;
        private readonly IWaitListRepository _waitListRepository;
        private readonly IWaitListServices _waitListServices;

        public RequestNotificationService(IUserRepository userRepository, IEmailBroker emailBroker, 
            IEmailService emailService, IWaitListRepository waitListRepository, IWaitListServices waitListServices)
        {
            _userRepository = userRepository;
            _emailBroker = emailBroker;
            _emailService = emailService;
            _waitListRepository = waitListRepository;
            _waitListServices = waitListServices;
        }

        public async Task SendRequestNotification(Request request)
        {
            var compatibleDonors = await _userRepository.SelectDonorsForNotification(request);

            var availableDonors = await compatibleDonors.WhereAsync(async x => await _waitListServices.IsDonorAvailable(x));

            if (!availableDonors.Any())
                return;

            var to = availableDonors.Select(x => new MailboxAddress(
                $"{x.FirstName} {x.LastName}", x.Email
            ));

            var messageBody = await _emailService.GetRequestNotificationBody(request);

            var subject = "Nueva Solicitud de Donación";

            await _emailBroker.SendToMany(to, subject, messageBody);
        }

        

    }
}
