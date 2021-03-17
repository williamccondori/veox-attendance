using Veox.Attendance.User.Application.Contexts.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace Veox.Attendance.User.Application.Contexts.Implementations
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