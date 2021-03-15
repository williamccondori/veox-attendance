// ReSharper disable UnusedMemberInSuper.Global

using System;

namespace Veox.Attendance.Identity.Domain.Entities.Common
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