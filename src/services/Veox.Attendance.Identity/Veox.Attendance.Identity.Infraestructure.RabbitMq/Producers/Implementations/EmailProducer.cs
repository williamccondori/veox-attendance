// ReSharper disable ConvertToUsingDeclaration

using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Clients.Interfaces;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Models;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Interfaces;

namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Implementations
{
    public class EmailProducer : IEmailProducer
    {
        private const string QueueName = "email--send-activation-code";

        private readonly IConnection _connection;

        public EmailProducer(IRabbitMqClient rabbitMqClient)
        {
            _connection = rabbitMqClient.Connect();
        }

        public void SendActivationCode(ActivationCodeEmail activationCodeEmail)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(QueueName, true, false, false, null);

                var message = JsonSerializer.Serialize(activationCodeEmail);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(string.Empty, QueueName, properties, body);
            }
        }
    }
}