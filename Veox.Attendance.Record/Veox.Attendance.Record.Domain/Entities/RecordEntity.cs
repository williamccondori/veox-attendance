using System;
using System.Collections.Generic;
using Veox.Attendance.Record.Domain.Common;

namespace Veox.Attendance.Record.Domain.Entities
{
    public class RecordEntity : AuditableBaseEntity, IDocument
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public bool IsPresent { get; set; }
        public DateTime Date { get; set; }
        public ICollection<DetailRecord> Details { get; set; }

        public static RecordEntity Create(string employeeId, bool isPresent, string userId)
        {
            var details = new List<DetailRecord> {DetailRecord.Create()};

            return new RecordEntity
            {
                EmployeeId = employeeId,
                IsPresent = isPresent,
                Date = DateTime.Today,
                Details = details,
                IsActive = true,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
        }
    }

    public class DetailRecord
    {
        public DateTime DateRecord { get; set; }
        public ObservationType ObservationType { get; set; }
        public string Observation { get; set; }

        public static DetailRecord Create()
        {
            return new DetailRecord
            {
                DateRecord = DateTime.Now,
                ObservationType = ObservationType.Ok,
                Observation = ObservationType.Ok.ToString(),
            };
        }

        public static DetailRecord CreateWithObservation(ObservationType observationType, string observation = null)
        {
            return new DetailRecord
            {
                DateRecord = DateTime.Now,
                ObservationType = observationType,
                Observation = string.IsNullOrEmpty(observation) ? observationType.ToString() : observation
            };
        }
    }

    public enum ObservationType
    {
        Ok,
        CloseBySystem,
    }
}