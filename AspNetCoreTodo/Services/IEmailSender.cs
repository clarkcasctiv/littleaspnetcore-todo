using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}