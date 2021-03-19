using MongoDB.Driver;
using Veox.Attendance.Workspace.Domain.Entities;

namespace Veox.Attendance.Workspace.Infraestructure.MongoDb.Contexts.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<WorkspaceEntity> Workspaces { get; }
        IMongoCollection<EmployeeEntity> Employees { get; }
        IMongoCollection<GroupEntity> Groups { get; }
    }
}