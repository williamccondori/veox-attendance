using Veox.Attendance.Workspace.Application.Contexts.Interfaces;

namespace Veox.Attendance.Workspace.Application.Contexts.Implementations
{
    public class ApplicationContext : IApplicationContext
    {
        public string UserId { get; private set; }

        public ApplicationContext()
        {
            UserId = "xxxxxxxxxxxxxxxxxxxxxxxx";
        }

        public void Update(string userId)
        {
            UserId = userId;
        }
    }
}