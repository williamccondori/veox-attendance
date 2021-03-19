// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Veox.Attendance.Workspace.Infraestructure.RabbitMq.Models.Record
{
    public class AddEmployeeModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public string WorkspaceId { get; set; }
        public string DocumentNumber { get; set; }
        public int TotalHours { get; set; }
        public bool IsEnabled { get; set; }
        public string ImageProfile { get; set; }
    }
}