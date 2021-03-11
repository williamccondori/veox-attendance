using System;
using System.Collections.Generic;
using System.Linq;
using Veox.Attendance.Record.Domain.Common;

namespace Veox.Attendance.Record.Domain.Entities
{
    public class RecordEntity : IDocument, IAuditable
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

        public string EmployeeId { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsPresent { get; set; }
        public ICollection<DetailRecord> Details { get; set; }

        public static RecordEntity Create(string employeeId, string userId)
        {
            var details = new List<DetailRecord> {DetailRecord.Create()};

            return new RecordEntity
            {
                EmployeeId = employeeId,
                IsPresent = true,
                Date = DateTime.Today,
                Details = details,
                IsActive = true,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
        }

        public string GetEndHour()
        {
            if (Details.Count % 2 != 0) return string.Empty;

            var lastDetail = Details.Last();

            return lastDetail == null ? string.Empty :  lastDetail.DateRecord.ToShortTimeString();
        }

        public string GetStartHour()
        {
            var firstDetail = Details.First();

            return firstDetail == null ? string.Empty : firstDetail.DateRecord.ToShortTimeString();
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