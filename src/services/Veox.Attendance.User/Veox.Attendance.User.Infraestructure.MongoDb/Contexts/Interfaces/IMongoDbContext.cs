using MongoDB.Driver;
using Veox.Attendance.User.Domain.Entities;

namespace Veox.Attendance.User.Infraestructure.MongoDb.Contexts.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<UserEntity> Users { get; }
    }
}