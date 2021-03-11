using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Wrappers;

namespace Veox.Attendance.Record.Application.Interfaces.Services
{
    public interface IRecordService
    {
        Task<Response<IEnumerable<DailyRecordResponse>>> GetDailySummaryByWorkspaceAsync(
            DailySummaryRequest dailySummaryRequest);

        Task<Response<SummaryEmployeeResponse>> GetSummaryByEmployeeAsync(
            RecordSummaryRequest recordSummaryRequest);
        
        Task<Response<SummaryEmployeeResponse>> CreateAsync(RecordCreateModel registerRequestModel);
    }
}