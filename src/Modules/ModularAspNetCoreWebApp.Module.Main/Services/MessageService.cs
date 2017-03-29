using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModularAspNetCoreWebApp.Module.Main.Services {

    public class AuthMessageSender : IEmailSender, ISmsSender {
        public Task SendEmailAsync(string email, string subject, string message) {
            // email service here to send an email.
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message) {
            //SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

}
