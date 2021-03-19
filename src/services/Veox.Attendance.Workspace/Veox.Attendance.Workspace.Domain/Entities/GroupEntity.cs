using System;
using Veox.Attendance.Workspace.Domain.Entities.Common;

namespace Veox.Attendance.Workspace.Domain.Entities
{
    public class GroupEntity : IDocument, IAuditable
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

        public string Name { get; set; }

        public static GroupEntity Create(string name, string userId)
        {
            return new GroupEntity
            {
                Name = name,
                IsActive = true,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
        }
    }
}