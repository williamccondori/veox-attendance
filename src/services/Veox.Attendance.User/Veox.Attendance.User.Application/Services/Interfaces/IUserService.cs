using System.Threading.Tasks;
using Veox.Attendance.User.Application.Models;
using Veox.Attendance.User.Application.Services.Interfaces.Common;

namespace Veox.Attendance.User.Application.Services.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task<UserResponse> GetAsync();
        Task<UserResponse> GetByIdAsync(string userId);
        Task<UserUpdateResponse> UpdateAsync(UserUpdateRequest userUpdateRequest);
        Task SaveAsync(SaveUserRequest saveUserRequest);
    }
}