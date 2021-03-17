using System;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Veox.Attendance.Record.Api.Extensions
{
    /// <summary>
    /// Consul configuration extension.
    /// </summary>
    public static class ConsulExtension
    {
        /// <summary>
        /// Add consul configuration.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceName">Service name.</param>
        /// <param name="serviceUri">Service uri.</param>
        public static void UseConsul(this IApplicationBuilder app, string serviceName, string serviceUri)
        {
            var service = new Uri(serviceUri);
            var client = app.ApplicationServices.GetRequiredService<IConsulClient>();

            var serviceId = Guid.NewGuid();
            var consulServiceId = $"{serviceName}:{serviceId}";

            var serviceRegistration = new AgentServiceRegistration
            {
                ID = consulServiceId,
                Address = service.Host,
                Port = service.Port,
                Name = serviceName,
                Check = new AgentCheckRegistration
                {
                    HTTP = $"{serviceUri}/health",
                    Notes = $"Checks {serviceUri}/health on {service.Host}:[{service.Port}]",
                    Timeout = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(30),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60)
                }
            };

            client.Agent.ServiceRegister(serviceRegistration).GetAwaiter().GetResult();
        }
    }
}