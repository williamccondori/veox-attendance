using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Veox.Attendance.Workspace.Domain.Entities;
using Veox.Attendance.Workspace.Domain.Repositories;
using Veox.Attendance.Workspace.Infraestructure.MongoDb.Contexts.Interfaces;

namespace Veox.Attendance.Workspace.Infraestructure.MongoDb.Repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly IMongoDbContext _context;

        public WorkspaceRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<WorkspaceEntity> GetByIdentifier(string workspaceIdentifier)
        {
            var cursor = await _context.Workspaces.FindAsync(x =>
                x.IsActive && x.Identifier.Equals(workspaceIdentifier));

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<WorkspaceEntity>> GetByEmployee(string employeeId)
        {
            var cursor = await _context.Workspaces.FindAsync(x =>
                x.IsActive && x.Id.Equals(employeeId));

            return await cursor.ToListAsync();
        }

        public async Task<WorkspaceEntity> Create(WorkspaceEntity workspaceEntity)
        {
            await _context.Workspaces.InsertOneAsync(workspaceEntity);

            return workspaceEntity;
        }

        public async Task<WorkspaceEntity> Update(string employeeId, WorkspaceEntity workspaceEntity)
        {
            await _context.Workspaces.ReplaceOneAsync(x => x.Id == employeeId, workspaceEntity);

            return workspaceEntity;
        }
    }
}