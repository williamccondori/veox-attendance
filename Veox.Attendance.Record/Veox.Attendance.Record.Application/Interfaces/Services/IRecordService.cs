using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Record.Application.Models;

namespace Veox.Attendance.Record.Application.Interfaces.Services
{
    public interface IRecordService
    {
        Task<List<DailySummaryResponse>> GetDailySummaryAsync(DailySummaryRequest dailySummaryRequest);

        Task<SummaryEmployeeResponse> GetSummaryByEmployeeAsync(SummaryEmployeeRequest summaryEmployeeRequest);

        Task<SummaryEmployeeResponse> CreateAsync(RecordCreateRequest recordCreateRequest);
    }
}