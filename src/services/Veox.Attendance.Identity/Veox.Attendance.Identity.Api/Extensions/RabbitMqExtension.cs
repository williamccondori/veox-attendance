using System;
using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Clients.Implementations;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Clients.Interfaces;

namespace Veox.Attendance.Identity.Api.Extensions
{
    /// <summary>
    /// Configure RabbitMQ's options. 
    /// </summary>
    public static class RabbitMqExtension
    {
        /// <summary>
        /// Configure RabbitMQ's options. 
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddRabbitMq(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
        }
    }
}