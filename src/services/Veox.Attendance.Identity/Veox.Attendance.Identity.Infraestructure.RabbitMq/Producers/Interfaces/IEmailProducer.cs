using Veox.Attendance.Identity.Infraestructure.RabbitMq.Models.EmailGenerator;

namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Interfaces
{
    public interface IEmailProducer
    {
        void SendActivationCode(ActivationCodeEmail activationCodeEmail);
    }
}