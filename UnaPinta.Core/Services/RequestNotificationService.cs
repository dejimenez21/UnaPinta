using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Core.Contracts;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;

namespace UnaPinta.Core.Services
{
    public class RequestNotificationService : IRequestNotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailBroker _emailBroker;
        private readonly IEmailService _emailService;

        public RequestNotificationService(IUserRepository userRepository, IEmailBroker emailBroker, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailBroker = emailBroker;
            _emailService = emailService;
        }

        public async Task SendRequestNotification(Request request)
        {
            var compatibleDonors = await _userRepository.SelectDonorsForNotification(request);

            if (!compatibleDonors.Any())
                return;

            var to = compatibleDonors.Select(x => new MailboxAddress(
                $"{x.FirstName} {x.LastName}", x.Email
            ));

            var messageBody = await _emailService.GetRequestNotificationBody(request);

            var subject = "Nueva Solicitud de Donación";

            await _emailBroker.SendToMany(to, subject, messageBody);
        }
    }
}
