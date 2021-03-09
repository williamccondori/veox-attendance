using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        public Task<IEnumerable<RecordEntity>> GetSummaryByDate(Guid employeeId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<RecordEntity> GetByDate(string employeeId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<RecordEntity> Create(RecordEntity recordEntity)
        {
            throw new NotImplementedException();
        }

        public Task<RecordEntity> Update(string id, RecordEntity recordEntity)
        {
            throw new NotImplementedException();
        }
    }
}