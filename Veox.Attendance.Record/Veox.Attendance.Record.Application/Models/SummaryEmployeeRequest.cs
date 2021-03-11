using System;
using System.ComponentModel.DataAnnotations;

namespace Veox.Attendance.Record.Application.Models
{
    public class SummaryEmployeeRequest
    {
        [Required] public string EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}