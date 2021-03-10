namespace Veox.Attendance.Record.Application.Models
{
    public class DailyRecordResponse
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsPresent { get; set; }
        public string ImageProfile { get; set; }
        public string Date { get; set; }
    }
}