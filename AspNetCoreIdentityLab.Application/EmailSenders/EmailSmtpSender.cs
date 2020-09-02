using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AspNetCoreIdentityLab.Application.EmailSenders
{
    public class EmailSmtpSender : IEmailSender
    {
        // Our private configuration variables
        private string _host;
        private int _port;
        private bool _enableSSL;
        private string _userName;
        private string _password;

        // Get our parameterized configuration
        public EmailSmtpSender(string host, int port, bool enableSSL, string userName, string password)
        {
            _host = host;
            _port = port;
            _enableSSL = enableSSL;
            _userName = userName;
            _password = password;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSSL
            };

            var mailMessage = new MailMessage(_userName, email, subject, htmlMessage) { IsBodyHtml = true };

            return client.SendMailAsync(mailMessage);
        }
    }
}
