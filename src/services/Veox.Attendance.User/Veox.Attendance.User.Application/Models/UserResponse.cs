// ReSharper disable ClassNeverInstantiated.Global

namespace Veox.Attendance.User.Application.Models
{
    public class UserResponse
    {
        public UserModelResponse User { get; set; }
    }

    public class UserModelResponse
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImageProfile { get; set; }
    }
}