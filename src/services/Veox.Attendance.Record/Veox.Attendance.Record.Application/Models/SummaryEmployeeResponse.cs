#region ReSharper linter configuration.

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

#endregion

using System.Collections.Generic;

namespace Veox.Attendance.Record.Application.Models
{
    public class SummaryEmployeeResponse
    {
        public string TotalHours { get; set; }
        public string ElapsedHours { get; set; }
        public string MissingHours { get; set; }
        public EmployeeResponse Employee { get; set; }
        public List<RecordResponse> Records { get; set; }
    }
}