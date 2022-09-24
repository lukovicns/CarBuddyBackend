using Microsoft.Extensions.Configuration;

namespace CarBuddy.WebApi.Config
{
    public class SmtpConfig
    {
        public string ConfirmEmailUrl { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }

        public SmtpConfig(IConfigurationSection section)
        {
            ConfirmEmailUrl = section["ConfirmEmailUrl"];
            SenderEmail = section["SenderEmail"];
            SenderPassword = section["SenderPassword"];
        }
    }
}
