using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;
using System.IO;
using UnaPinta.Data.Brokers.DateTimes;

namespace UnaPinta.Data.Brokers
{
    public class MockEmailBroker : IEmailBroker
    {
        private readonly DirectoryInfo _dirInfo;
        private readonly IDateTimeBroker _dateTimeBroker;

        public MockEmailBroker(IDateTimeBroker dateTimeBroker)
        {
            _dirInfo = new DirectoryInfo(Environment.CurrentDirectory + "/EmailMocks/");
            _dateTimeBroker = dateTimeBroker;
        }

        public async Task Send(string message, MailboxAddress to)
        {
            
        }

        public async Task Send(MailboxAddress to, string subject, MimeEntity body)
        {
            var path = _dirInfo.CreateSubdirectory($"{subject}-{_dateTimeBroker.GetCurrentDateTime().Hour}");
            var file = path.FullName + $"/{to.Name} - {to.Address}.html";
            await body.WriteToAsync(file);
        }

        public async Task SendToMany(IEnumerable<MailboxAddress> to, string subject, MimeEntity body)
        {
            var path = _dirInfo.CreateSubdirectory($"{subject}-{_dateTimeBroker.GetCurrentDateTime().Hour}{_dateTimeBroker.GetCurrentDateTime().Minute}");
            foreach (var dest in to)
            {
                var file = path.FullName + $"/{dest.Name} - {dest.Address}.html";
                await body.WriteToAsync(file);
            }  
        }
    }
}
