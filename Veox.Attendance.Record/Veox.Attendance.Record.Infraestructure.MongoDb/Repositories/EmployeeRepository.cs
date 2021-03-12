using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Veox.Attendance.Record.Domain.Entities;
using Veox.Attendance.Record.Domain.Repositories;
using Veox.Attendance.Record.Infraestructure.MongoDb.Contexts.Interfaces;

namespace Veox.Attendance.Record.Infraestructure.MongoDb.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoDbContext _context;

        public EmployeeRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllByWorksapce(string workspaceId)
        {
            var cursor =
                await _context.Employees.FindAsync(x => x.IsActive && x.IsEnabled && x.WorkpaceId.Equals(workspaceId));

            return await cursor.ToListAsync();
        }

        public async Task<EmployeeEntity> GetById(string employeeId)
        {
            var cursor = await _context.Employees.FindAsync(x => x.IsActive && x.Id.Equals(employeeId));

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<EmployeeEntity> GetByDocumentNumberAndWorkspace(string documentNumber, string workspaceId)
        {
            var cursor = await _context.Employees.FindAsync(x =>
                x.IsActive && x.DocumentNumber.Equals(documentNumber) && x.WorkpaceId.Equals(workspaceId));

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<EmployeeEntity> Create(EmployeeEntity employeeEntity)
        {
            await _context.Employees.InsertOneAsync(employeeEntity);

            return employeeEntity;
        }

        public async Task<EmployeeEntity> Update(string employeeId, EmployeeEntity employeeEntity)
        {
            await _context.Employees.ReplaceOneAsync(x => x.Id == employeeId, employeeEntity);

            return employeeEntity;
        }
    }
}