#region ReSharper linter configuration.

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

#endregion

namespace Veox.Attendance.Record.Application.Models
{
    public class RecordCreateRequest
    {
        public string WorkspaceId { get; set; }
        public string DocumentNumber { get; set; }
    }
}