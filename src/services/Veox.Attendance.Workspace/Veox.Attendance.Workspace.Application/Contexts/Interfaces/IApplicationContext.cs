namespace Veox.Attendance.Workspace.Application.Contexts.Interfaces
{
    public interface IApplicationContext
    {
        string UserId { get; }
        string EmployeeId { get; }

        void Update(string userId, string employeeId);
    }
}