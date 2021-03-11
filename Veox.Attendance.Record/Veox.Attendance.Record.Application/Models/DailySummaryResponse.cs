namespace Veox.Attendance.Record.Application.Models
{
    public class DailySummaryResponse : EmployeeResponse
    {
        public bool IsPresent { get; set; }
        public string Date { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
    }
}