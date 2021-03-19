using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Clients;
using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Models.Identity;

// ReSharper disable ConvertToUsingDeclaration

namespace Veox.Attendance.Workspace.Infraestructure.RabbitMq.Producers.Identity
{
    public class IdentityProducer : IIdentityProducer
    {
        private const string QueueName = "attendance--identity-adduserrol";

        private readonly IConnection _connection;

        public IdentityProducer(IRabbitMqClient rabbitMqClient)
        {
            _connection = rabbitMqClient.Connect();
        }

        public void AddUserRol(AddUserRolModel addUserRolModel)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(QueueName, true, false, false, null);

                var message = JsonSerializer.Serialize(addUserRolModel);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(string.Empty, QueueName, properties, body);
            }
        }
    }
}