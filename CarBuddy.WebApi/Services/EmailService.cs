using CarBuddy.Application.Models;
using CarBuddy.Application.Models.DTOs;
using CarBuddy.WebApi.Config;
using System.Threading.Tasks;

namespace CarBuddy.WebApi.Services
{
    public class EmailService
    {
        private readonly SmtpConfig _config;

        public EmailService(SmtpConfig config)
        {
            _config = config;
        }

        public Task<Result> Send(RegisterDto data, string path)
        {
            var url = _config.ConfirmEmailUrl
                .Replace("{UserId}", data.Id)
                .Replace("{Token}", data.ActivationToken);
            var body = new HtmlMessageParser(path, data.Email, url)
                .CreateHtmlBody();
            var sender = new EmailSender(
                data.Email,
                _config.SenderEmail,
                _config.SenderPassword,
                body);

            return sender.Send();
        }
    }
}
