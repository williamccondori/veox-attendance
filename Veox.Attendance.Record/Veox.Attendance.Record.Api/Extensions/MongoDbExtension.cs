using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.Record.Infraestructure.MongoDb;
using Veox.Attendance.Record.Infraestructure.MongoDb.Contexts;

namespace Veox.Attendance.Record.Api.Extensions
{
    public static class MongoDbExtension
    {
        public static void AddMongoDbConfiguration(this IServiceCollection services, IMongoDbOptions mongoDbOptions)
        {
            var connectionString =
                $"mongodb://{mongoDbOptions.Username}:{mongoDbOptions.Password}@{mongoDbOptions.Hostname}:{mongoDbOptions.Port}";

            services.AddSingleton(s => new MongoDbContext(connectionString, mongoDbOptions.Database));
        }
    }
}