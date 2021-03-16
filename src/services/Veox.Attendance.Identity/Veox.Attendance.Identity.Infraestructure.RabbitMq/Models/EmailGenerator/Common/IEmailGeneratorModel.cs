namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Models.Common
{
    public interface IEmailGeneratorrModel
    {
        string Email { get; set; }
        EmailType EmailType { get; set; }
    }

    public enum EmailType
    {
        ActivationCode
    }
}