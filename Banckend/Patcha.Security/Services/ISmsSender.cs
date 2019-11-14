using System.Threading.Tasks;

namespace Patcha.Security
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
