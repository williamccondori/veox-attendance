using System;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Clients.Interfaces;

namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Clients.Implementations
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
            IConnection connection;
            
            try
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = _options.Hostname,
                    Port = AmqpTcpEndpoint.UseDefaultPort,
                    UserName = _options.Username,
                    Password = _options.Password,
                    VirtualHost = _options.VHost
                };

                connection = connectionFactory.CreateConnection();
            }
            catch (Exception)
            {
                connection = null;
            }

            return connection;
        }
    }
}