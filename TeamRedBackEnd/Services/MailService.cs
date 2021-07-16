using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamRedBackEnd.ViewModels;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace TeamRedBackEnd.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendMailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            BodyBuilder builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public MailRequest MakeVerificationMail(Database.Models.User user)
        {
            return  new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Verification code",
                Body = "Hello, " + user.Name + "<br> Activate your account by clicking the link below  <br> <a href="+_mailSettings.VerificationLinkBase + user.VerificationCode + " > Click me!</a>"
                
            };
        }

    }
}
