using System.Collections.Generic;

namespace Veox.Attendance.Record.Application.Models
{
    public class SummaryEmployeeResponse
    {
        public EmployeeResponse Employee { get; set; }
        public IEnumerable<RecordResponse> Records { get; set; }
    }
}