using System;

namespace Veox.Attendance.Record.Application.Models
{
    public class DailySummaryRequest
    {
        public string WorkspaceId { get; set; }
        public DateTime? Date { get; set; }
    }
}