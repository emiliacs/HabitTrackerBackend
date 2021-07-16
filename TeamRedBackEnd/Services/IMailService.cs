using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd.Services
{
    public interface IMailService
    {
        Task SendMailAsync(ViewModels.MailRequest mailRequest);
        public ViewModels.MailRequest MakeVerificationMail(Database.Models.User user);
    }
}
