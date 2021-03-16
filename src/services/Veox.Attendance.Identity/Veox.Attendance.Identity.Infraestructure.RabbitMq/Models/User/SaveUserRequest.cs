namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Models.User
{
    public class SaveUserRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}