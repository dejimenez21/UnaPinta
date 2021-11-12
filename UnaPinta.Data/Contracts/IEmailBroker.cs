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
        Task Send(MailboxAddress to, string subject, MimeEntity body);
        Task SendToMany(IEnumerable<MailboxAddress> to, string subject, MimeEntity body);
    }
}
