// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

using System;
using Veox.Attendance.Identity.Domain.Entities.Common;

namespace Veox.Attendance.Identity.Domain.Entities
{
    public class UserEntity : IDocument, IAuditable
    {
        public string Id { get; set; }
        public bool IsActive { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordKey { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsVerified { get; set; }

        public static UserEntity Create(string email, string password, string passwordKey, bool isEnabled = true)
        {
            return new UserEntity
            {
                Email = email,
                Password = password,
                PasswordKey = passwordKey,
                IsEnabled = isEnabled,
                IsVerified = true,
                IsActive = true,
                CreatedBy = string.Empty,
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