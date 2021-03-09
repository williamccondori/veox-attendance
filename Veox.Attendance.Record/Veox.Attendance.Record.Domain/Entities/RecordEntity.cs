using System;
using System.Collections.Generic;
using Veox.Attendance.Record.Domain.Common;

namespace Veox.Attendance.Record.Domain.Entities
{
    public class RecordEntity : AuditableBaseEntity, IDocument
    {
        public string EmployeeId { get; set; }
        public bool IsPresent { get; set; }
        public DateTime Date { get; set; }
        public ICollection<DetailRecord> Details { get; set; }
        public ICollection<ObservationRecord> Observations { get; set; }
        public string Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class DetailRecord
    {
        public DateTime DateRecord { get; set; }
    }

    public class ObservationRecord
    {
        public ObservationType ObservationType { get; set; }
        public string Message { get; set; }
    }

    public enum ObservationType
    {
        CloseBySystem,
        
    }
}