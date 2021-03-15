// ReSharper disable UnusedMemberInSuper.Global

namespace Veox.Attendance.Identity.Domain.Entities.Common
{
    public interface IDocument
    {
        string Id { get; set; }
        void Update(string id);
        void Delete(string id);
    }
}