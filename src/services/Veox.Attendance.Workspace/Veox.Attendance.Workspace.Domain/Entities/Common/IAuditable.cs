#region ReSharper linter configuration.

// ReSharper disable UnusedMemberInSuper.Global

#endregion

using System;

namespace Veox.Attendance.Workspace.Domain.Entities.Common
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