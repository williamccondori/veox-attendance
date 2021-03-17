namespace Veox.Attendance.Identity.Application.Contexts.Interfaces
{
    public interface IApplicationContext
    {
        string UserId { get; }

        void Update(string userId);
    }
}