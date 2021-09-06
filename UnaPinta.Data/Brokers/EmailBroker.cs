using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;

namespace UnaPinta.Data.Brokers
{
    public class EmailBroker : IEmailBroker
    {
        private readonly SmtpClient _client;
        private readonly MailboxAddress _from;
        public EmailBroker()
        {
            _from = new MailboxAddress("Una Pinta", "unapintateam@gmail.com");

            _client = new SmtpClient();
            _client.Connect("smtp.gmail.com", 465, true);
            _client.Authenticate("unapintateam@gmail.com", "Unapinta1234");
        }

        public async Task Send(string message, MailboxAddress to)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(_from);
            mimeMessage.To.Add(to);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            await _client.SendAsync(mimeMessage);
        }

        public async Task Send(MailboxAddress to, string subject, MimeEntity body)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(_from);
            mimeMessage.To.Add(to);
            mimeMessage.Body = body;

            await _client.SendAsync(mimeMessage);
        }
    }
}
