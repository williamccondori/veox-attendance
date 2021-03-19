using System.Threading.Tasks;
using Veox.Attendance.Workspace.Domain.Entities;
using Veox.Attendance.Workspace.Domain.Repositories;
using Veox.Attendance.Workspace.Infraestructure.MongoDb.Contexts.Interfaces;

namespace Veox.Attendance.Workspace.Infraestructure.MongoDb.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMongoDbContext _context;

        public GroupRepository(IMongoDbContext context)
        {
            _context = context;
        }
        
        public async Task<GroupEntity> Create(GroupEntity groupEntity)
        {
            await _context.Groups.InsertOneAsync(groupEntity);

            return groupEntity;
        }
    }
}