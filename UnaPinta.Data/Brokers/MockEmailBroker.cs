using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;
using System.IO;

namespace UnaPinta.Data.Brokers
{
    public class MockEmailBroker : IEmailBroker
    {
        private readonly DirectoryInfo _dirInfo;

        public MockEmailBroker()
        {
            _dirInfo = new DirectoryInfo(Environment.CurrentDirectory + "/EmailMocks/");
        }

        public async Task Send(string message, MailboxAddress to)
        {
            
        }

        public async Task Send(MailboxAddress to, string subject, MimeEntity body)
        {
            var path = _dirInfo.CreateSubdirectory($"{subject}-{DateTime.Now.Hour}");
            var file = path.FullName + $"/{to.Name} - {to.Address}.html";
            await body.WriteToAsync(file);
        }

        public async Task SendToMany(IEnumerable<MailboxAddress> to, string subject, MimeEntity body)
        {
            var path = _dirInfo.CreateSubdirectory($"{subject}-{DateTime.Now.Hour}{DateTime.Now.Minute}");
            foreach (var dest in to)
            {
                var file = path.FullName + $"/{dest.Name} - {dest.Address}.html";
                await body.WriteToAsync(file);
            }  
        }
    }
}
