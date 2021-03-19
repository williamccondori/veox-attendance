using RabbitMQ.Client;

namespace Veox.Attendance.Workspace.Infraestructure.RabbitMq.Clients
{
    public interface IRabbitMqClient
    {
        IConnection Connect();
    }
}