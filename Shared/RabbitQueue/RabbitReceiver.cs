using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shared.RabbitQueue
{
    public class RabbitReceiver : BackgroundService
    {
        //private readonly IMailService _service;
        private IServiceProvider _provider;
        protected string queue = "";
        public RabbitReceiver(IServiceProvider provider)
        {
            _provider = provider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queue,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                HandleWork(message).Wait();
            };
            channel.BasicConsume(queue: queue,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public virtual async Task HandleWork(string message)
        {
            
        }
    }


}
