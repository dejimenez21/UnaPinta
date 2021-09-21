using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using MimeKit.Utils;
using UnaPinta.Data.Contracts;
using UnaPinta.Dto.Models;
using UnaPinta.Core.Contracts;

namespace UnaPinta.Core.Services
{
    public class EmailSender : IEmailService
    {
        private readonly IEmailBroker _broker;
        private readonly IUnaPintaRepository _repo;

        public MimeMessage message { get; set; }
        SmtpClient client;

        public EmailSender(IUnaPintaRepository repo, IEmailBroker emailBroker)
        {
            _broker = emailBroker;
            _repo = repo;
            message = new MimeMessage();
            
            MailboxAddress from = new MailboxAddress("Una Pinta", "unapintateam@gmail.com");
            message.From.Add(from);

            client = new SmtpClient();
            //client.Connect("smtp.gmail.com", 465, true);
            //client.Authenticate("unapintateam@gmail.com", "Unapinta1234");

        }
        public EmailSender(IUnaPintaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> SendNotification(User user, Request request)
        {

            message.Subject = "Notificacion de Solicitud Publicada";
            BodyBuilder bodyBuilder = new BodyBuilder();
            var imagePath = "../API/wwwroot/images/UnaPinta.png";
            var image = bodyBuilder.LinkedResources.Add(imagePath);
            image.ContentId = MimeUtils.GenerateMessageId();
            // bodyBuilder.TextBody = $"Saludos, {user.FirstName} {user.LastName}. A traves de este correo le informamos que ha sido publicada una solicitud de donacion compatible con su perfil.";
            // bodyBuilder.TextBody += $"\n\n Solicitante: {request.RequesterNav.FirstName} {request.RequesterNav.LastName}";
            // var hemo = await _repo.GetBloodComponentById(request.BloodComponentId);
            // bodyBuilder.TextBody += $"\n Hemocomponente: {hemo.Description}";
            // bodyBuilder.TextBody += $"\n Cantidad: {request.Amount} pintas";
            // var type = await _repo.GetBloodTypeById(request.RequesterNav.BloodTypeId);
            // bodyBuilder.TextBody += $"\n Grupo Sanguineo: {type.Description}";
            var preBody = await GetNotificationBody(user, request);
            bodyBuilder.HtmlBody = preBody.Replace("Images/UnaPinta.png", "cid:"+image.ContentId);
            //bodyBuilder.HtmlBody = await GetNotificationBody(user, request);
            message.Body = bodyBuilder.ToMessageBody();

            MailboxAddress to = new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email);
            message.To.Add(to);

            await client.SendAsync(message);

            return true;
        }

        public async Task<bool> SendConfirmation(ConfirmationCode confirmation)
        {
            MailboxAddress to = new MailboxAddress(
                $"{confirmation.UserNav.FirstName} {confirmation.UserNav.LastName}" ,confirmation.UserNav.Email);
            message.To.Add(to);
            message.Subject = "Confirmacion de correo";
            BodyBuilder body = new BodyBuilder();
            var imagePath = "../API/wwwroot/images/UnaPinta.png";
            var image = body.LinkedResources.Add(imagePath);
            image.ContentId = MimeUtils.GenerateMessageId();
            // body.TextBody = $"Su codigo de confirmacion es: {confirmation.Code}";
            var preBody = await GetConfirmationBody("confirmation");
            body.HtmlBody = preBody.Replace("Images/UnaPinta.png", "cid:"+image.ContentId);
            message.Body = body.ToMessageBody();

            await client.SendAsync(message);

            return true;
        }

        public async Task<bool> SendEmailConfirmation(UserSignUp user, string confirmationLink)
        {
            MailboxAddress to = new MailboxAddress(
                $"{user.FirstName} {user.LastName}", user.Email);
            message.To.Add(to);
            message.Subject = "Confirmacion de correo";
            BodyBuilder body = new BodyBuilder();
            var imagePath = "../API/wwwroot/images/UnaPinta.png";
            var image = body.LinkedResources.Add(imagePath);
            image.ContentId = MimeUtils.GenerateMessageId();
            body.HtmlBody = confirmationLink;
            message.Body = body.ToMessageBody();

            await client.SendAsync(message);

            return true;
        }

        public async Task Disconnect()
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }

        public async Task<string> GetConfirmationBody(string url)
        {
            string path = "../API/Templates/ConfirmationEmail.html";
            string body = await File.ReadAllTextAsync(path);
            body = body.Replace("@Url", url);
            return body;
        }

        public async Task<string> GetNotificationBody(User user, Request request)
        {
            string path = "../API/wwwroot/EmailTemplate2.html";
            string body = await File.ReadAllTextAsync(path);
            body = body.Replace("#Usuario#", user.FirstName+" "+user.LastName);
            body = body.Replace("#Solicitante#", $"{request.RequesterNav.FirstName} {request.RequesterNav.LastName}");
            var hemo = await _repo.GetBloodComponentById(request.BloodComponentId);
            body = body.Replace("#Hemocomponente#", $"{hemo.Description}");
            body = body.Replace("#Cantidad#", $"{request.Amount}");
            var type = await _repo.GetBloodTypeById(request.RequesterNav.BloodTypeId);
            body = body.Replace("#Gruposanguineo#", $"{type.Description}");
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
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "Me estoy enviando!!!";
            var body = bodyBuilder.ToMessageBody();
            return body;
        }
    }
}