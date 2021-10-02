using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using MimeKit.Utils;
using UnaPinta.Data.Contracts;
using UnaPinta.Dto.Models;
using UnaPinta.Core.Contracts;
using System.Globalization;
using UnaPinta.Core.Extensions;

namespace UnaPinta.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailBroker _broker;

        public MimeMessage message { get; set; }

        public EmailService(IEmailBroker emailBroker)
        {
            _broker = emailBroker;
        }

        public async Task<string> GetConfirmationBody(string url)
        {
            string path = "../API/Templates/ConfirmationEmail.html";
            string body = await File.ReadAllTextAsync(path);
            body = body.Replace("@Url", url);
            return body;
        }

        public async Task SendEmailVerificationAsync(User receiver, string link)
        {
            MailboxAddress to = new MailboxAddress(
                $"{receiver.FirstName} {receiver.LastName}", receiver.Email);

            string subject = "Verificación de correo";

            BodyBuilder bodyBuilder = new BodyBuilder();
            var imagePath = "../API/wwwroot/images/UnaPinta.png";
            var image = bodyBuilder.LinkedResources.Add(imagePath);
            image.ContentId = MimeUtils.GenerateMessageId();
            var preBody = await GetConfirmationBody(link);
            bodyBuilder.HtmlBody = preBody.Replace("Images/UnaPinta.png", "cid:" + image.ContentId);
            var body = bodyBuilder.ToMessageBody();

            await _broker.Send(to, subject, body);
        }

        public async Task<MimeEntity> GetRequestNotificationBody(Request request)
        {
            string path = "../API/Templates/NotificationEmail.html";
            string preBody = await File.ReadAllTextAsync(path);
            preBody = preBody.Replace("@PatientName", request.Name);
            preBody = preBody.Replace("@CenterName", request.CenterName);
            preBody = preBody.Replace("@CenterAddress", request.CenterAddress);
            preBody = preBody.Replace("@ResponseDueDate", request.ResponseDueDate.ToStringSP());
            preBody = preBody.Replace("@PatientStory", request.PatientStory);

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = preBody;
            var body = bodyBuilder.ToMessageBody();
            return body;
        }
    }
}