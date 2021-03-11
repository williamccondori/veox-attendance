using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Veox.Attendance.Record.Domain.Entities;
using Veox.Attendance.Record.Domain.Repositories;
using Veox.Attendance.Record.Infraestructure.MongoDb.Contexts;

namespace Veox.Attendance.Record.Infraestructure.MongoDb.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly MongoDbContext _context;

        public RecordRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecordEntity>> GetSummaryByDate(string employeeId, DateTime startDate, DateTime endDate)
        {
            var cursor = await _context.Records.FindAsync(x =>
                x.IsActive && x.EmployeeId.Equals(employeeId) && x.Date >= startDate && x.Date <= endDate);
            
            return await cursor.ToListAsync();
        }

        public async Task<RecordEntity> GetByDate(string employeeId, DateTime date)
        {
            var cursor = await _context.Records.FindAsync(x =>
                x.IsActive && x.EmployeeId.Equals(employeeId) && x.Date.Equals(date));

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<RecordEntity> Create(RecordEntity recordEntity)
        {
            await _context.Records.InsertOneAsync(recordEntity);

            return recordEntity;
        }

        public async Task<RecordEntity> Update(string id, RecordEntity recordEntity)
        {
            await _context.Records.ReplaceOneAsync(x => x.Id == id, recordEntity);

            return recordEntity;
        }
    }
}