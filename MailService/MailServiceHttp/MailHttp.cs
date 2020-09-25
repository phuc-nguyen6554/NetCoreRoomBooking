using MailService.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.HttpService;
using Newtonsoft.Json;

namespace MailService.MailServiceHttp
{
    public class MailHttp : IMailHttp
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClientService _client;
        
        public MailHttp(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClientService();
        }
        public async Task SendMailAsync(SingleMailRequest request)
        {
            var endpoint = _configuration["InternalService:Mail"] + "mails";
            var json = JsonConvert.SerializeObject(request);
            await _client.Post(endpoint, json);
        }
    }
}
