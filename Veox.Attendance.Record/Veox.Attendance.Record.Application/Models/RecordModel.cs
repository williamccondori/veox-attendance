using System;

namespace Veox.Attendance.Record.Application.Models
{
    public class RecordModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ImageProfile { get; set; }
        public bool IsPresent { get; set; }
    }
}