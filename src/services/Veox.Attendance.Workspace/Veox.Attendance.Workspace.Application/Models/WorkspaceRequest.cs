// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Veox.Attendance.Workspace.Application.Models
{
    public class WorkspaceRequest
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public UserRequest User { get; set; }
        public string DocumentNumber { get; set; }
        public int TotalHours { get; set; }
    }

    public class UserRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageProfile { get; set; }
    }
}