using System.Threading.Tasks;
using Veox.Attendance.Identity.Application.Models;
using Veox.Attendance.Identity.Application.Services.Implementations.Common;

namespace Veox.Attendance.Identity.Application.Services.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest authRegister);
    }
}