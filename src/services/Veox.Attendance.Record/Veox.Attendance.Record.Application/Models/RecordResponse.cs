#region ReSharper linter configuration.

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

#endregion

using System.Collections.Generic;

namespace Veox.Attendance.Record.Application.Models
{
    public class RecordResponse
    {
        public bool IsPresent { get; set; }
        public string Date { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public string TotalHours { get; set; }
        public string ElapsedHours { get; set; }
        public string MissingHours { get; set; }
        public List<DetailRecordResponse> Details { get; set; }
    }

    public class DetailRecordResponse
    {
        public string Hour { get; set; }
        public bool IsStartHour { get; set; }
        public bool HasObservation { get; set; }
        public string Observation { get; set; }
    }
}