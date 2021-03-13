using System;
using System.ComponentModel.DataAnnotations;

namespace Veox.Attendance.Workspace.Application.Models
{
    public class WorkspaceModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}