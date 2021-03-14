using System;
using Veox.Attendance.Workspace.Domain.Entities.Common;

namespace Veox.Attendance.Workspace.Domain.Entities
{
    public class WorkspaceEntity : IDocument, IAuditable
    {
        public string Id { get; set; }
        public bool IsActive { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; private set; }
        public string ImageProfile { get; private set; }

        public static WorkspaceEntity Create(string name, string identifier, string description, string imageProfile,
            string userId)
        {
            return new WorkspaceEntity
            {
                Name = name,
                Identifier = identifier,
                Description = description,
                ImageProfile = imageProfile,
                IsActive = true,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
        }

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
    }
}