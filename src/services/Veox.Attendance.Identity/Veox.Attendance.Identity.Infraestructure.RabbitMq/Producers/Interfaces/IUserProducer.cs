using Veox.Attendance.Identity.Infraestructure.RabbitMq.Models.User;

namespace Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Interfaces
{
    public interface IUserProducer
    {
        void Save(SaveUserRequest saveUserModel);
    }
}