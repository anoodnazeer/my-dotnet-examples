using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Mail;
using System.Threading.Tasks;

namespace Domain.Service.Email.Interface
{
    public interface IProviderEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}