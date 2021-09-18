using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;

namespace UnaPinta.Data.Brokers
{
    public class MockEmailBroker : IEmailBroker
    {
        public async Task Send(string message, MailboxAddress to)
        {
            
        }

        public async Task Send(MailboxAddress to, string subject, MimeEntity body)
        {
            
        }
    }
}
