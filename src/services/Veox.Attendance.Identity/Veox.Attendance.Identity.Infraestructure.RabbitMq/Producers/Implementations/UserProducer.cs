using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Clients.Interfaces;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Models.User;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Interfaces;

// ReSharper disable ConvertToUsingDeclaration
namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Implementations
{
    public class UserProducer : IUserProducer
    {
        private const string QueueName = "attendance--user-save";

        private readonly IConnection _connection;

        public UserProducer(IRabbitMqClient rabbitMqClient)
        {
            _connection = rabbitMqClient.Connect();
        }
        
        public void Save(SaveUserRequest saveUserModel)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(QueueName, true, false, false, null);

                var message = JsonSerializer.Serialize(saveUserModel);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(string.Empty, QueueName, properties, body);
            }
        }
    }
}