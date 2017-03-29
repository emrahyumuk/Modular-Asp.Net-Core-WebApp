using System.Threading.Tasks;

namespace ModularAspNetCoreWebApp.Module.Main.Services {
    public interface IEmailSender {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
