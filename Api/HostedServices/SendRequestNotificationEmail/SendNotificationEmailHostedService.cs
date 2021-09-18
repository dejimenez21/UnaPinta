using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Core.Contracts;

namespace UnaPinta.Api.HostedServices.SendRequestNotificationEmail
{
    public sealed class SendNotificationEmailHostedService : BaseHostedService<SendNotificationEmailHostedService>
    {
        private readonly IServiceProvider _serviceProvider;

        public SendNotificationEmailHostedService(IServiceProvider serviceProvider, IConfiguration configuration, ILoggerManager logger) : base(configuration, logger)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task DoWork(object state)
        {
            throw new NotImplementedException();
        }
    }
}
