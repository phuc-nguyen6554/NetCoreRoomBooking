using MailService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.MailServiceHttp
{
    public interface IMailHttp
    {
        public Task SendMailAsync(SingleMailRequest request);
    }
}
