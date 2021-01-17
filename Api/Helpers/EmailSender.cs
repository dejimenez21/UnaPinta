using System.Threading.Tasks;
using Api.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using Api.Services;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Api.Helpers
{
    public class EmailSender
    {
        private readonly IUnaPintaRepository _repo;

        public MimeMessage message { get; set; }
        SmtpClient client;

        public EmailSender(IUnaPintaRepository repo)
        {
            _repo = repo;
            message = new MimeMessage();
            
            MailboxAddress from = new MailboxAddress("Una Pinta", "unapintateam@gmail.com");
            message.From.Add(from);

            client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("unapintateam@gmail.com", "Unapinta1234");

        }

        public async Task<bool> SendNotification(User user, Request request)
        {

            message.Subject = "Notificacion de Solicitud Publicada";
            BodyBuilder bodyBuilder = new BodyBuilder();
            // bodyBuilder.TextBody = $"Saludos, {user.FirstName} {user.LastName}. A traves de este correo le informamos que ha sido publicada una solicitud de donacion compatible con su perfil.";
            // bodyBuilder.TextBody += $"\n\n Solicitante: {request.RequesterNav.FirstName} {request.RequesterNav.LastName}";
            // var hemo = await _repo.GetBloodComponentById(request.BloodComponentId);
            // bodyBuilder.TextBody += $"\n Hemocomponente: {hemo.Description}";
            // bodyBuilder.TextBody += $"\n Cantidad: {request.Amount} pintas";
            // var type = await _repo.GetBloodTypeById(request.RequesterNav.BloodTypeId);
            // bodyBuilder.TextBody += $"\n Grupo Sanguineo: {type.Description}";
            bodyBuilder.HtmlBody = await GetNotificationBody(user, request);
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
            // body.TextBody = $"Su codigo de confirmacion es: {confirmation.Code}";
            body.HtmlBody = await GetConfirmationBody(confirmation);
            message.Body = body.ToMessageBody();

            await client.SendAsync(message);

            return true;
        }

        public async Task Disconnect()
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }

        public async Task<string> GetConfirmationBody(ConfirmationCode confirmation)
        {
            string path = "../wwwroot/EmailTemplate.html";
            string body = await File.ReadAllTextAsync(path);
            body = body.Replace("#Codigo OTP#", confirmation.Code);
            return body;
        }

        public async Task<string> GetNotificationBody(User user, Request request)
        {
            string path = "../wwwroot/EmailTemplate2.html";
            string body = await File.ReadAllTextAsync(path);
            body = body.Replace("#Usuario#", user.FirstName+" "+user.LastName);
            body = body.Replace("#Solicitante#", $"{request.RequesterNav.FirstName} {request.RequesterNav.LastName}");
            var hemo = await _repo.GetBloodComponentById(request.BloodComponentId);
            body = body.Replace("#Hemocomponente", $"{hemo.Description}");
            body = body.Replace("#Cantidad#", $"{request.Amount}");
            var type = await _repo.GetBloodTypeById(request.RequesterNav.BloodTypeId);
            body = body.Replace("#Gruposanguineo#", $"{type.Description}");
            return body;
        }



    }
}