using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Veox.Attendance.Workspace.Infraestructure.RabbitMq.Clients
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly RabbitMqOptions _options;

        public RabbitMqClient(IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _options = rabbitMqOptions.Value;
        }

        public IConnection Connect()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _options.Hostname,
                Port = AmqpTcpEndpoint.UseDefaultPort,
                UserName = _options.Username,
                Password = _options.Password,
                VirtualHost = _options.VHost
            };

            var connection = connectionFactory.CreateConnection();

            return connection;
        }
    }
}