namespace Veox.Attendance.Workspace.Infraestructure.RabbitMq.Models.Identity
{
    public class AddUserRolModel
    {
        public string WorkspaceId { get; set; }
        public string EmployeeId { get; set; }
        public string UserId { get; set; }
    }
}