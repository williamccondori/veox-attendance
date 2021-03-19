using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Clients;
using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Models.Record;

// ReSharper disable ConvertToUsingDeclaration

namespace Veox.Attendance.Workspace.Infraestructure.RabbitMq.Producers.Record
{
    public class RecordProducer : IRecordProducer
    {
        private const string QueueName = "attendance--record-addemployee";

        private readonly IConnection _connection;

        public RecordProducer(IRabbitMqClient rabbitMqClient)
        {
            _connection = rabbitMqClient.Connect();
        }
        
        public void AddEmployee(AddEmployeeModel addEmployeeModel)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(QueueName, true, false, false, null);

                var message = JsonSerializer.Serialize(addEmployeeModel);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(string.Empty, QueueName, properties, body);
            }
        }
    }
}