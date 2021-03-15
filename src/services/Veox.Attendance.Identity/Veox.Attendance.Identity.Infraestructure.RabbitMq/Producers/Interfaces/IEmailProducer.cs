using Veox.Attendance.Identity.Infraestructure.RabbitMq.Models;

namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Interfaces
{
    public interface IEmailProducer
    {
        void SendActivationCode(ActivationCodeEmail activationCodeEmail);
    }
}