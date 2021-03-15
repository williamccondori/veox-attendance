using Veox.Attendance.Identity.Infraestructure.RabbitMq.Models.Common;

namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Models
{
    public class ActivationCodeEmail : IEmailGeneratorrModel
    {
        public string Email { get; set; }
        public EmailType EmailType { get; set; }
        public string FullName { get; set; }
        public string ActivationCode { get; set; }
    }
}