using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Veox.Attendance.Record.Domain.Entities;
using Veox.Attendance.Record.Infraestructure.MongoDb.Serializers;

namespace Veox.Attendance.Record.Infraestructure.MongoDb.Contexts
{
    public class MongoDbContext : MongoClient
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string database)
        {
            var client = new MongoClient(connectionString);

            _database = client.GetDatabase(database);

            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateSerializer());

            BsonClassMap.RegisterClassMap<EmployeeEntity>(cm =>
            {
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapAuditableFields();
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<RecordEntity>(cm =>
            {
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapAuditableFields();
                cm.AutoMap();
            });
        }

        public IMongoCollection<EmployeeEntity> Employees => _database.GetCollection<EmployeeEntity>("employees");
        public IMongoCollection<RecordEntity> Records => _database.GetCollection<RecordEntity>("records");
    }
}