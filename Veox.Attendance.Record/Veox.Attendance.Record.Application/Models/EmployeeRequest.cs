#region ReSharper linter configuration.

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

#endregion

namespace Veox.Attendance.Record.Application.Models
{
    public class EmployeeRequest
    {
        public string Id { get; set; }
        public string WorkspaceId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int TotalHours { get; set; }
        public string DocumentNumber { get; set; }
        public string ImageProfile { get; set; }
        public bool IsEnabled { get; set; }
    }
}