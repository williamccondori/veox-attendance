using System.Threading.Tasks;
using Veox.Attendance.Identity.Application.Models;

namespace Veox.Attendance.Identity.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest authRegister);
    }
}