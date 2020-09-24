using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Shared.RabbitQueue;

namespace MailService.Services.Implement
{
    public class MailQueueService : RabbitReceiver
    {
        //private readonly IMailService _service;
        private IServiceProvider _provider;
        public MailQueueService(IServiceProvider provider) : base(provider)
        {
            _provider = provider;
            queue = "hello";
        }

        public override async Task HandleWork(string message)
        {
            using (var scoped = _provider.CreateScope())
            {
                var mailService = scoped.ServiceProvider.GetRequiredService<IMailService>();
                await mailService.SendMailAsync(new DTO.SingleMailRequest
                {
                    Email = "phuc.nguyen@siliconstack.com.au",
                    Subject = "Test Rabbit",
                    Content = "Hello Rabbit edited version " + message
                });
            }
        }
    }
}
