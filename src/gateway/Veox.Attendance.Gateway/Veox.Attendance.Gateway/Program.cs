using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Veox.Attendance.Gateway
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((host, config) =>
                {
                    config.SetBasePath(host.HostingEnvironment.ContentRootPath);
                    config.AddJsonFile("appsettings.json", true, true);
                    config.AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", true, true);
                    config.AddJsonFile("ocelot.json");
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}