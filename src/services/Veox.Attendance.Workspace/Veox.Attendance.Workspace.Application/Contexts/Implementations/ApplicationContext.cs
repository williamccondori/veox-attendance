using Veox.Attendance.Workspace.Application.Contexts.Interfaces;

namespace Veox.Attendance.Workspace.Application.Contexts.Implementations
{
    public class ApplicationContext : IApplicationContext
    {
        public string UserId { get; private set; }
        public string EmployeeId { get; private set; }

        public ApplicationContext()
        {
            UserId = string.Empty;
        }

        public void Update(string userId, string employeeId)
        {
            UserId = userId;
            EmployeeId = employeeId;
        }
    }
}