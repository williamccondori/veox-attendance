using System;

namespace Veox.Attendance.Record.Domain.Common
{
    public interface IAuditable
    {
        bool IsActive { get; }

        string CreatedBy { get; }

        DateTime CreatedDate { get; }

        string UpdatedBy { get; }

        DateTime UpdatedDate { get; }
    }
}