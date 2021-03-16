using System.Threading.Tasks;
using Veox.Attendance.User.Application.Models;
using Veox.Attendance.User.Application.Services.Interfaces.Common;

namespace Veox.Attendance.User.Application.Services.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task SaveAsync(SaveUserRequest saveUserRequest);
    }
}