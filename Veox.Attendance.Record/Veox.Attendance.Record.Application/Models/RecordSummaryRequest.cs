using System;

namespace Veox.Attendance.Record.Application.Models
{
    public class RecordSummaryRequest
    {
        public string EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}