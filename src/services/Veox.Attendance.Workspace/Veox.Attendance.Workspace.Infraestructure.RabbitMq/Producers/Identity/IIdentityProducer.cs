using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Models.Identity;

namespace Veox.Attendance.Workspace.Infraestructure.RabbitMq.Producers.Identity
{
    public interface IIdentityProducer
    {
        void AddUserRol(AddUserRolModel addUserRolModel);
    }
}