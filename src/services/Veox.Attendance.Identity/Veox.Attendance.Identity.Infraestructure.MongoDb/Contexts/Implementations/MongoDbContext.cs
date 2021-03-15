using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Veox.Attendance.Identity.Domain.Entities;
using Veox.Attendance.Identity.Infraestructure.MongoDb.Contexts.Interfaces;
using Veox.Attendance.Identity.Infraestructure.MongoDb.Serializers;

namespace Veox.Attendance.Identity.Infraestructure.MongoDb.Contexts.Implementations
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

            BsonClassMap.RegisterClassMap<UserEntity>(cm =>
            {
                cm.MapId();
                cm.MapAuditableFields();
                cm.AutoMap();
            });
            
            BsonClassMap.RegisterClassMap<ActivationCodeEntity>(cm =>
            {
                cm.MapId();
                cm.MapAuditableFields();
                cm.AutoMap();
            });
        }
        public IMongoCollection<UserEntity> Users => _database.GetCollection<UserEntity>("users");
        public IMongoCollection<ActivationCodeEntity> ActivationCodes => _database.GetCollection<ActivationCodeEntity>("activation_codes");
    }
}