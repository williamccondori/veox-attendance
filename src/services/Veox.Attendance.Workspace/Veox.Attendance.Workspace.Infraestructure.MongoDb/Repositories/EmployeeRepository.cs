using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Veox.Attendance.Workspace.Domain.Entities;
using Veox.Attendance.Workspace.Domain.Repositories;
using Veox.Attendance.Workspace.Infraestructure.MongoDb.Contexts.Interfaces;

namespace Veox.Attendance.Workspace.Infraestructure.MongoDb.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoDbContext _context;

        public EmployeeRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllByUserId(string userId)
        {
            var cursor = await _context.Employees.FindAsync(x => x.IsActive && x.UserId.Equals(userId));

            return await cursor.ToListAsync();
        }

        public async Task<EmployeeEntity> Create(EmployeeEntity employeeEntity)
        {
            await _context.Employees.InsertOneAsync(employeeEntity);

            return employeeEntity;
        }
    }
}