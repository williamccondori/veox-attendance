using System;
using System.Collections.Generic;
using System.Linq;
using Veox.Attendance.Record.Domain.Entities.Common;

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
        
        // ReSharper disable once MemberCanBePrivate.Global
        public int TotalHours { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsPresent { get; set; }
        public ICollection<DetailRecord> Details { get; private set; }

        public static RecordEntity Create(string employeeId, int totalHours, string userId)
        {
            var details = new List<DetailRecord> {DetailRecord.Create()};

            return new RecordEntity
            {
                EmployeeId = employeeId,
                TotalHours = totalHours,
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

            var lastDetail = Details.Last(x => !x.IsStartHour);

            return lastDetail == null ? string.Empty : lastDetail.DateRecord.ToShortTimeString();
        }

        public string GetStartHour()
        {
            var firstDetail = Details.Last(x => x.IsStartHour);

            return firstDetail == null ? string.Empty : firstDetail.DateRecord.ToShortTimeString();
        }

        public TimeSpan GetTotalHours()
        {
            return TimeSpan.FromHours(TotalHours);
        }

        public TimeSpan GetElapsedHours()
        {
            var details = Details.ToList();

            if (details.Count % 2 != 0)
            {
                details.Add(new DetailRecord {DateRecord = DateTime.Now});
            }

            if (details.Count % 2 != 0)
            {
                return TimeSpan.Zero;
            }

            var startHour = details.First(x => x.IsStartHour).DateRecord;
            var endHour = details.First(x => !x.IsStartHour).DateRecord;

            return endHour.Subtract(startHour);
        }

        public TimeSpan GetMissingHours()
        {
            var totalHours = GetTotalHours();
            var elapsedHours = GetElapsedHours();

            return totalHours.Subtract(elapsedHours);
        }
    }

    public class DetailRecord
    {
        public DateTime DateRecord { get; set; }
        public bool IsStartHour { get; private set; }
        public ObservationType ObservationType { get; private set; }
        public string Observation { get; private set; }

        public static DetailRecord Create(bool isStartHour = true)
        {
            return new DetailRecord
            {
                DateRecord = DateTime.Now,
                IsStartHour = isStartHour,
                ObservationType = ObservationType.Ok,
                Observation = ObservationType.Ok.ToString()
            };
        }

        public static DetailRecord CreateWithObservation(DateTime dateTime, bool isStartHour,
            ObservationType observationType, string observation = null)
        {
            return new DetailRecord
            {
                DateRecord = dateTime,
                IsStartHour = isStartHour,
                ObservationType = observationType,
                Observation = string.IsNullOrEmpty(observation) ? observationType.ToString() : observation
            };
        }
    }

    public enum ObservationType
    {
        Ok,
        CloseBySystem
    }
}