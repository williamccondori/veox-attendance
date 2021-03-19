using System;
using Veox.Attendance.Workspace.Domain.Entities.Common;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Veox.Attendance.Workspace.Domain.Entities
{
    public class EmployeeEntity : IDocument, IAuditable
    {
        public string Id { get; set; }

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

        public bool IsActive { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DocumentNumber { get; private set; }
        public string WorkspaceId { get; private set; }
        public int TotalHours { get; set; }
        public bool IsEnabled { get; set; }
        public string ImageProfile { get; set; }
        public string GroupId { get; set; }
        public DateTime StartDate { get; set; }

        public static EmployeeEntity Create(string name, string lastName, string email, string documentNumber,
            string workspaceId, int totalHours, string imageProfile, string groupId, string userId)
        {
            return new EmployeeEntity
            {
                UserId = userId,
                Name = name,
                LastName = lastName,
                Email = email,
                DocumentNumber = documentNumber,
                WorkspaceId = workspaceId,
                TotalHours = totalHours,
                ImageProfile = imageProfile,
                GroupId = groupId,
                IsEnabled = true,
                StartDate = DateTime.Today,
                IsActive = true,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
        }
    }
}