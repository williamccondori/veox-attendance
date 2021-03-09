using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Veox.Attendance.Record.Domain.Entities;

namespace Veox.Attendance.Record.Infraestructure.MongoDb.Contexts
{
    public class MongoDbContext : MongoClient
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string host, string port, string user, string password, string databaseName)
        {
            var connectionString = $"mongodb://{user}:{password}@{host}:{port}";
            
            var client = new MongoClient(connectionString);

            _database = client.GetDatabase(databaseName);
            
            BsonClassMap.RegisterClassMap<EmployeeEntity>(cm =>
            { cm.SetIsRootClass(true);
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            BsonClassMap.RegisterClassMap<RecordEntity>(cm =>
            { cm.SetIsRootClass(true);
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));

                //cm.MapMember(c => c.IsActive).SetElementName("isActive");
            });
        }

        public IMongoCollection<EmployeeEntity> Employees => _database.GetCollection<EmployeeEntity>("employees");
        public IMongoCollection<RecordEntity> Records => _database.GetCollection<RecordEntity>("records");
    }
}