using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Services.Interfaces.Common;

namespace Veox.Attendance.Record.Application.Services.Interfaces
{
    public interface IRecordService : IBaseService
    {
        Task<List<DailySummaryResponse>> GetDailySummaryAsync(DailySummaryRequest dailySummaryRequest);

        Task<SummaryEmployeeResponse> GetSummaryByEmployeeAsync(SummaryEmployeeRequest summaryEmployeeRequest);

        Task<SummaryEmployeeResponse> CreateAsync(RecordCreateRequest recordCreateRequest);
    }
}