// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace Veox.Attendance.User.Worker.Models
{
    public class SaveUserRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}