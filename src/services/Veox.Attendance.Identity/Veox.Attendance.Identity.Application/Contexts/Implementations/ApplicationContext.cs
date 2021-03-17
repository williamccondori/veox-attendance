using Veox.Attendance.Identity.Application.Contexts.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace Veox.Attendance.Identity.Application.Contexts.Implementations
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