using System;

namespace Veox.Attendance.Record.Domain.Common
{
    public class AuditableBaseEntity
    {
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        
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