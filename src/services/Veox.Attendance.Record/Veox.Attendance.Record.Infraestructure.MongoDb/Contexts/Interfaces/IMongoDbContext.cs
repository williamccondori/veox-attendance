using MongoDB.Driver;
using Veox.Attendance.Record.Domain.Entities;

namespace Veox.Attendance.Record.Infraestructure.MongoDb.Contexts.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<EmployeeEntity> Employees { get; }
        IMongoCollection<RecordEntity> Records { get; }
    }
}