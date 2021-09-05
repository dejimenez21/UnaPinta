using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Data.Contracts
{
    public interface IEmailBroker
    {
        Task Send(string message, MailboxAddress to);
    }
}
