using System.Threading.Tasks;

namespace Patcha.Security
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
