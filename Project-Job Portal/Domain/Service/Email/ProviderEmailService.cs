using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Domain.Mail;
using Domain.Service.Email.Interface;
using Microsoft.Extensions.Options;

namespace Domain.Service.Email
{
    public class ProviderEmailService : IProviderEmailService
    {
        private readonly MailSettings _mailSettings;

        public ProviderEmailService(IOptionsMonitor<MailSettings> mailSettings)
        {
            // Use named option "Provider" for provider email
            _mailSettings = mailSettings.Get("Provider");
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using var smtp = new SmtpClient(_mailSettings.Host, _mailSettings.Port)
                {
                    Credentials = new NetworkCredential(_mailSettings.UserMail, _mailSettings.Password),
                    EnableSsl = _mailSettings.UseSSL
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_mailSettings.FromMail ?? _mailSettings.UserMail, _mailSettings.DisplayName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);
                await smtp.SendMailAsync(mailMessage);
            }
            catch (SmtpException smtpEx)
            {
                throw new InvalidOperationException($"SMTP error: {smtpEx.StatusCode} - {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send email.", ex);
            }
        }
    }
}
