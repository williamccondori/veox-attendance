using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Veox.Attendance.User.Application.Models;
using Veox.Attendance.User.Application.Services.Interfaces;

// ReSharper disable ConvertToUsingDeclaration

namespace Veox.Attendance.User.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqOptions _options;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(
            ILogger<Worker> logger,
            IServiceScopeFactory serviceScopeFactory,
            IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _options = rabbitMqOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _options.Hostname,
                Port = AmqpTcpEndpoint.UseDefaultPort,
                UserName = _options.Username,
                Password = _options.Password,
                VirtualHost = _options.VHost
            };

            using var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            
            channel.QueueDeclare("attendance--user-save", true, false, false, null);
            channel.BasicQos(0, 1, false);
            var eventingBasicConsumer = new EventingBasicConsumer(channel);
            eventingBasicConsumer.Received += ConsumerOnReceived;
            channel.BasicConsume("attendance--user-save", false, eventingBasicConsumer);

            Console.WriteLine($"Consumming queue: attendance--user-save");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async void ConsumerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var body = e.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var saveUserRequest = JsonSerializer.Deserialize<SaveUserRequest>(json);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IUserService>();

                    await service.SaveAsync(saveUserRequest);

                    ((EventingBasicConsumer) sender).Model.BasicAck(e.DeliveryTag, false);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(@"{Message}", exception.Message);
            }
        }
    }
}