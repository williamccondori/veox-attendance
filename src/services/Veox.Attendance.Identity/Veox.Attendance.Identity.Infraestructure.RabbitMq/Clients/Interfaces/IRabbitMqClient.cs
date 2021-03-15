using RabbitMQ.Client;

namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Clients.Interfaces
{
    public interface IRabbitMqClient
    {
        IConnection Connect();
    }
}