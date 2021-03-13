#region ReSharper linter configuration.

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

#endregion

namespace Veox.Attendance.Record.Application.Models
{
    public class EmployeeResponse
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public string ImageProfile { get; set; }
    }
}