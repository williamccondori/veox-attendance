using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Veox.Attendance.User.Domain.Entities;
using Veox.Attendance.User.Infraestructure.MongoDb.Contexts.Interfaces;
using Veox.Attendance.User.Infraestructure.MongoDb.Serializers;

namespace Veox.Attendance.User.Infraestructure.MongoDb.Contexts.Implementations
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
        }

        public IMongoCollection<UserEntity> Users => _database.GetCollection<UserEntity>("users");
    }
}