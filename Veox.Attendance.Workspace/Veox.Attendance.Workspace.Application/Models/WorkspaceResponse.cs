using System;

namespace Veox.Attendance.Workspace.Application.Models
{
    public class WorkspaceResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}