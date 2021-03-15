// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

using System;
using Veox.Attendance.Identity.Domain.Entities.Common;

namespace Veox.Attendance.Identity.Domain.Entities
{
    public class ActivationCodeEntity : IDocument, IAuditable
    {
        public string Id { get; set; }
        public bool IsActive { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public string UserId { get; set; }
        public string Code { get; private set; }

        public static ActivationCodeEntity Create(string userId, string code)
        {
            return new ActivationCodeEntity
            {
                UserId = userId,
                Code = code,
                IsActive = true,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
        }

        public void Update(string id)
        {
            UpdatedBy = string.Empty;
            UpdatedDate = DateTime.Now;
        }

        public void Delete(string id)
        {
            IsActive = false;
            UpdatedBy = string.Empty;
            UpdatedDate = DateTime.Now;
        }
    }
}