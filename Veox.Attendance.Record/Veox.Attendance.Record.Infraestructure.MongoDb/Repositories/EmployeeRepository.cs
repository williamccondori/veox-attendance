using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Veox.Attendance.Record.Domain.Entities;
using Veox.Attendance.Record.Domain.Repositories;
using Veox.Attendance.Record.Infraestructure.MongoDb.Contexts;

namespace Veox.Attendance.Record.Infraestructure.MongoDb.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MongoDbContext _context;

        public EmployeeRepository(MongoDbContext context)
        {
            _context = context;
        }

        public Task<EmployeeEntity> GetById(string employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeEntity> GetByDocumentNumber(string documentNumber)
        {
            var cursor = await _context.Employees.FindAsync(x => x.IsActive && x.DocumentNumber == documentNumber);

            return await cursor.FirstOrDefaultAsync();
        }

        public Task<EmployeeEntity> Create(EmployeeEntity employeeEntity)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeEntity> Update(string employeeId, EmployeeEntity employeeEntity)
        {
            throw new NotImplementedException();
        }
    }
}