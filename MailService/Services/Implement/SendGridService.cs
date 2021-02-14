using MailService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using Shared.Data;

namespace MailService.Services.Implement
{
    public class SendGridService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly SendGridClient _client;

        public SendGridService(IConfiguration configuration)
        {
            _configuration = configuration;
            var secretKey = _configuration["SendGrid:SecretKey"];
            _client = new SendGridClient(secretKey);
        }
        public async Task SendMailAsync(SingleMailRequest request)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress("phuc.nguyen@siliconstack.com.au"),
                Subject = request.Subject,
                HtmlContent = request.Content
            };

            message.AddTo(new EmailAddress(request.Email));

            List<EmailAddress> Ccs = new List<EmailAddress>();
            if(request.Cc.Length > 0)
            {
                foreach (var cc in request.Cc)
                {
                    Ccs.Add(new EmailAddress(cc));
                }
                message.AddCcs(Ccs);
            }

            await _client.SendEmailAsync(message).ConfigureAwait(false);
        }

        public async Task SendMailAsync(MailRequest request)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress("phuc.nguyen@siliconstack.com.au"),
                Subject = request.Subject,
                HtmlContent = request.Content
            };

            message.AddTo(new EmailAddress(request.Email));

            List<EmailAddress> Ccs = new List<EmailAddress>();
            if (request.Cc.Count > 0)
            {
                foreach (var cc in request.Cc)
                {
                    Ccs.Add(new EmailAddress(cc));
                }
                message.AddCcs(Ccs);
            }

            await _client.SendEmailAsync(message).ConfigureAwait(false);
        }

        public async Task SendMailAsync(MulitpleMailRequest request)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress("phuc.nguyen@siliconstack.com.au"),
                Subject = request.Subject,
                HtmlContent = request.Content
            };

            List<EmailAddress> emailAddresses = new List<EmailAddress>();
            foreach (var mail in request.Email)
            {
                emailAddresses.Add(new EmailAddress(mail));
            }

            message.AddTos(emailAddresses);

            List<EmailAddress> Ccs = new List<EmailAddress>();
            if(request.Cc.Length > 0)
            {
                foreach (var cc in request.Cc)
                {
                    Ccs.Add(new EmailAddress(cc));
                }
                message.AddCcs(Ccs);
            }

            await _client.SendEmailAsync(message).ConfigureAwait(false);
        }
    }
}
