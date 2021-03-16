using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Veox.Attendance.User.Infraestructure.MongoDb;
using Veox.Attendance.User.IoC;

namespace Veox.Attendance.User.Worker
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<MongoDbOptions>(hostContext.Configuration.GetSection("MongoDb"));
                    services.Configure<RabbitMqOptions>(hostContext.Configuration.GetSection("RabbitMq"));
                    services.AddServiceDependency();
                    services.AddHostedService<Worker>();
                });
    }
}