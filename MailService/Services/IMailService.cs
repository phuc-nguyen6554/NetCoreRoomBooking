using MailService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Data;

namespace MailService.Services
{
    public interface IMailService
    {
        public Task SendMailAsync(SingleMailRequest request);
        public Task SendMailAsync(MulitpleMailRequest request);

        public Task SendMailAsync(MailRequest request);
    }
}
