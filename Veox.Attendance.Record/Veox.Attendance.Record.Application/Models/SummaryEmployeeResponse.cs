using System.Collections.Generic;

namespace Veox.Attendance.Record.Application.Models
{
    public class SummaryEmployeeResponse
    {
        public EmployeeResponse Employee { get; set; }
        public List<RecordResponse> Records { get; set; }

        public SummaryEmployeeResponse()
        {
            Records = new List<RecordResponse>();
        }
    }
}