namespace Veox.Attendance.User.Domain.Entities.Common
{
    public interface IDocument
    {
        string Id { get; set; }
        void Update(string userId);
        void Delete(string userId);
    }
}