using System.Threading.Tasks;

namespace ModularAspNetCoreWebApp.Module.Main.Services {
    public interface ISmsSender {
        Task SendSmsAsync(string number, string message);
    }
}
