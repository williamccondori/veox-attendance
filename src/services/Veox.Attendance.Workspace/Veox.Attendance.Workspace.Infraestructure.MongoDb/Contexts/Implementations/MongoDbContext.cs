using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Veox.Attendance.Workspace.Domain.Entities;
using Veox.Attendance.Workspace.Infraestructure.MongoDb.Contexts.Interfaces;
using Veox.Attendance.Workspace.Infraestructure.MongoDb.Serializers;

namespace Veox.Attendance.Workspace.Infraestructure.MongoDb.Contexts.Implementations
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

            BsonClassMap.RegisterClassMap<WorkspaceEntity>(cm =>
            {
                cm.MapId();
                cm.MapAuditableFields();
                cm.AutoMap();
            });
            
            BsonClassMap.RegisterClassMap<EmployeeEntity>(cm =>
            {
                cm.MapId();
                cm.MapAuditableFields();
                cm.AutoMap();
            });
            
            BsonClassMap.RegisterClassMap<GroupEntity>(cm =>
            {
                cm.MapId();
                cm.MapAuditableFields();
                cm.AutoMap();
            });
        }

        public IMongoCollection<WorkspaceEntity> Workspaces => _database.GetCollection<WorkspaceEntity>("workspaces");
        public IMongoCollection<EmployeeEntity> Employees => _database.GetCollection<EmployeeEntity>("employees");
        public IMongoCollection<GroupEntity> Groups => _database.GetCollection<GroupEntity>("groups");
    }
}