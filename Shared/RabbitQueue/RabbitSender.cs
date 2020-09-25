using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;


namespace Shared.RabbitQueue
{
    public class RabbitSender
    {
        private ConnectionFactory _factory;
        public RabbitSender()
        {
            _factory = new ConnectionFactory { HostName = "localhost" };
        }

        public void Send(string queue, string message)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queue,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                                 routingKey: queue,
                                 basicProperties: null,
                                 body: body);
        }

        public void SendObject(string queue, object data)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queue,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

            channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: body);
        }
    }
}
