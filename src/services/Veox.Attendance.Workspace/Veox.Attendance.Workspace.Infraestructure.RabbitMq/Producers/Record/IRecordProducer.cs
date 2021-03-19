using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Models.Record;

namespace Veox.Attendance.Workspace.Infraestructure.RabbitMq.Producers.Record
{
    public interface IRecordProducer
    {
        void AddEmployee(AddEmployeeModel addEmployeeModel);
    }
}