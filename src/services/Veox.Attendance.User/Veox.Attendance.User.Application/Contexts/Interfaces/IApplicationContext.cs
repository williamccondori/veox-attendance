namespace Veox.Attendance.User.Application.Contexts.Interfaces
{
    public interface IApplicationContext
    {
        string UserId { get; }

        void Update(string userId);
    }
}