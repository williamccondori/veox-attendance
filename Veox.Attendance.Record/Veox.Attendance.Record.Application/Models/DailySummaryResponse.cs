using System.Collections.Generic;

namespace Veox.Attendance.Record.Application.Models
{
    public class DailySummaryResponse : EmployeeResponse
    {
        public bool IsPresent { get; set; }
        public string Date { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public string TotalHours { get; set; }
        public string ElapsedHours { get; set; }
        public string MissingHours { get; set; }
        public List<DailySummaryDetailResponse> Details { get; set; }
    }

    public class DailySummaryDetailResponse
    {
        public string Hour { get; set; }
        public bool IsStartHour { get; set; }
    }
}