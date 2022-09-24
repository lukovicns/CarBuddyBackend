using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CarBuddy.Application.Models
{
    public class EmailSender
    {
        private readonly string _recipient;
        private readonly string _email;
        private readonly string _password;
        private readonly string _body;

        public EmailSender(
            string recipient,
            string username,
            string password,
            string body)
        {
            _recipient = recipient;
            _email = username;
            _password = password;
            _body = body;
        }

        public async Task<Result> Send()
        {
            var client = CreateSmtpClient();
            var message = CreateMessage();

            try
            {
                await client.SendMailAsync(message);
                return new Result(new { Message = "Email successfully sent." }, 200);
            }
            catch (Exception ex)
            {
                return new Result(ex.Message, 400);
            }
            finally
            {
                message.Dispose();
                client.Dispose();
            }
        }

        private SmtpClient CreateSmtpClient()
        {
            return new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(_email, _password),
                EnableSsl = true,
            };
        }

        private MailMessage CreateMessage()
        {
            var message = new MailMessage
            {
                From = new MailAddress(_email),
                Subject = "Welcome to CarBuddy - Confirm your email address",
                IsBodyHtml = true,
                Body = _body,
            };
            message.To.Add(new MailAddress(_recipient));

            return message;
        }
    }
}
