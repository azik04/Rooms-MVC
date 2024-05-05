using System.Net.Mail;
using System.Net;
using MimeKit;
using Rooms.Services.Interfaces;

namespace Rooms.Services.Implementations;

public class MailService : IMailService
{
        private readonly IWebHostEnvironment _env;

        public MailService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task Send(string from, string to, string link, string subject)
        {
            string path = Path.Combine(_env.WebRootPath, "assets", "Templates", "EmailTemplate.html");

            var builder = new BodyBuilder();

            using (StreamReader SourceReader = System.IO.File.OpenText(path))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            string emailTemplate = System.IO.File.ReadAllText(path);
            emailTemplate = emailTemplate.Replace("{{verificationLink}}", link);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = emailTemplate;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.mail.ru";
            smtp.EnableSsl = true;

            NetworkCredential networkCred = new NetworkCredential("hacibalaev.azik@mail.ru", "4RgiQRYXJ9YtJDghRSms\r\n");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = networkCred;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Port = 587;
            smtp.Send(mail);

        }
}
