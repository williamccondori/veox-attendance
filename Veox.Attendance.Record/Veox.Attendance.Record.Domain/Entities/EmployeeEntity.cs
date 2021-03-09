using System;
using Veox.Attendance.Record.Domain.Common;

namespace Veox.Attendance.Record.Domain.Entities
{
    public class EmployeeEntity : AuditableBaseEntity, IDocument
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public bool IsEnabled { get; set; }
        public string ImageProfile { get; set; }

        public static EmployeeEntity Create(string id, string name, string lastName, string documentNumber,
            string imageProfile, bool isEnabled, string userId)
        {
            return new EmployeeEntity
            {
                Id = id,
                Name = name,
                LastName = lastName,
                DocumentNumber = documentNumber,
                ImageProfile = imageProfile,
                IsEnabled = isEnabled,
                IsActive = true,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
        }
    }
}