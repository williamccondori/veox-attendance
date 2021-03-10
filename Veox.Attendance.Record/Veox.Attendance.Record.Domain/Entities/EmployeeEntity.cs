using System;
using Veox.Attendance.Record.Domain.Common;

namespace Veox.Attendance.Record.Domain.Entities
{
    public class EmployeeEntity : IDocument, IAuditable
    {
        public string Id { get; set; }
        public bool IsActive { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime UpdatedDate { get; private set; }

        public void Update(string userId)
        {
            UpdatedBy = userId;
            UpdatedDate = DateTime.Now;
        }

        public void Delete(string userId)
        {
            IsActive = false;
            UpdatedBy = userId;
            UpdatedDate = DateTime.Now;
        }

        public string WorkpaceId { get; private set; }
        public string DocumentNumber { get; private set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsEnabled { get; set; }
        public string ImageProfile { get; set; }

        public static EmployeeEntity Create(string id, string workspaceId, string name, string lastName,
            string documentNumber,
            string imageProfile, bool isEnabled, string userId)
        {
            return new EmployeeEntity
            {
                Id = id,
                WorkpaceId = workspaceId,
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