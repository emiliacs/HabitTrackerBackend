using System.Threading.Tasks;

namespace TeamRedBackEnd.Services
{
    public interface IMailService
    {
        Task SendMailAsync(DataObjects.MailRequest mailRequest);
        public DataObjects.MailRequest MakeVerificationMail(Database.Models.User user);
    }
}
