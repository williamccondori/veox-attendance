using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Veox.Attendance.Record.Domain.Entities;
using Veox.Attendance.Record.Infraestructure.MongoDb.Contexts.Interfaces;
using Veox.Attendance.Record.Infraestructure.MongoDb.Serializers;

namespace Veox.Attendance.Record.Infraestructure.MongoDb.Contexts.Implementations
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbOptions> mongoDbOptions)
        {
            var options = mongoDbOptions.Value;

            var connectionString =
                $"mongodb://{options.Username}:{options.Password}@{options.Hostname}:{options.Port}";

            var client = new MongoClient(connectionString);

            _database = client.GetDatabase(options.Database);

            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateSerializer());

            BsonClassMap.RegisterClassMap<EmployeeEntity>(cm =>
            {
                cm.MapId();
                cm.MapAuditableFields();
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<RecordEntity>(cm =>
            {
                cm.MapId();
                cm.MapAuditableFields();
                cm.AutoMap();
            });
        }

        public IMongoCollection<EmployeeEntity> Employees => _database.GetCollection<EmployeeEntity>("employees");
        public IMongoCollection<RecordEntity> Records => _database.GetCollection<RecordEntity>("records");
    }
}