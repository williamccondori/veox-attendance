using MongoDB.Driver;
using Veox.Attendance.Identity.Domain.Entities;

namespace Veox.Attendance.Identity.Infraestructure.MongoDb.Contexts.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<UserEntity> Users { get; }
        IMongoCollection<ActivationCodeEntity> ActivationCodes { get; }
    }
}