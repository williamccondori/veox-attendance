using System;
using System.ComponentModel.DataAnnotations;

namespace Veox.Attendance.Record.Application.Models
{
    public class DailySummaryRequest
    {
        [Required] public string WorkspaceId { get; set; }
        public DateTime? Date { get; set; }
    }
}