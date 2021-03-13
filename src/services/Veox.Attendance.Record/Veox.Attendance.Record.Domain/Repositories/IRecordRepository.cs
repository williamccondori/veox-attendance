using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Record.Domain.Entities;

namespace Veox.Attendance.Record.Domain.Repositories
{
    public interface IRecordRepository
    {
        Task<IEnumerable<RecordEntity>> GetSummaryByDate(string employeeId, DateTime startDate, DateTime endDate);

        Task<RecordEntity> GetByDate(string employeeId, DateTime date);

        Task<RecordEntity> Create(RecordEntity recordEntity);
        
        Task<RecordEntity> Update(string recordId, RecordEntity recordEntity);
    }
}