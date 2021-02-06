using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ChrisReminders
{
    public class MailService
    {
        private readonly SmtpClient _client;
        private readonly MailAddress _from;
        private readonly ILogger _logger;

        public MailService(ILogger logger, IOptions<MailSettings> settings)
        {
            _client = new SmtpClient()
            {
                Host = settings.Value.SmtpHost,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = settings.Value.Username,
                    Password = settings.Value.Password
                }
            };
            _from = new MailAddress(settings.Value.Username);
            _logger = logger;
        }
        public async Task<bool> SendMail(MailAddress to, string subject, string body)
        {
            var message = new MailMessage(_from, to)
            {
                Subject = subject,
                Body = body
            };
            try
            {
                await _client.SendMailAsync(message);
                return true;
            }
            catch(Exception e)
            {
                _logger.Error(e,"Unknown error in mailservice.");
                return false;
            }

        }
    }
}
