using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MailService.DTO;
using MailService.Services;
using Shared.Data;

namespace MailService.Controllers
{
    [Route("mails")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _service;

        public MailController(IMailService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task SendMail(MailRequest request)
        {
            await _service.SendMailAsync(request);
        }
    }
}
