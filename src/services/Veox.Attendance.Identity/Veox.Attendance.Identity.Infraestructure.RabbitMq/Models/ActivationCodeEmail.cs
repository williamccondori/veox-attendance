namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Models
{
    public class ActivationCodeEmail
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ActivationCode { get; set; }
    }
}