using System;
using Veox.Attendance.User.Domain.Entities.Common;

namespace Veox.Attendance.User.Domain.Entities
{
    public class UserEntity : IDocument, IAuditable
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

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageProfile { get; set; }
        public string FullName => $"{Name} {LastName}";

        public static UserEntity Create(string id, string name, string lastName, string email, string imageProfile,
            string userId)
        {
            return new UserEntity
            {
                Id = id,
                Name = name,
                LastName = lastName,
                Email = email,
                ImageProfile = imageProfile,
                IsActive = true,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
        }
    }
}