using Domain_js.Helpers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_js.Service
{
    public class EmailService : IEmailService

    {
        private readonly MailSettings mailSettings;
        private readonly IConfiguration config;

        public EmailService(IOptions<MailSettings> _mailsettings, IConfiguration _config)
        {
            mailSettings = _mailsettings.Value;
            config = _config;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var FromMail = config.GetSection("MailSettings")["FromMail"];
                var DisplayName = config.GetSection("MailSettings")["DisplayName"];
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(DisplayName, FromMail));
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(mailSettings.Host, mailSettings.Port, mailSettings.UseSSL);
                smtp.Authenticate(mailSettings.UserMail, mailSettings.Password);

                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
