using System;

namespace Veox.Attendance.Record.Application.Models
{
    public class EmployeeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public string ImageProfile { get; set; }
        public bool IsEnabled { get; set; }
    }
}