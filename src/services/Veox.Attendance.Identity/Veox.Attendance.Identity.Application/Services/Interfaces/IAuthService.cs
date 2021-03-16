using System.Threading.Tasks;
using Veox.Attendance.Identity.Application.Models;
using Veox.Attendance.Identity.Application.Services.Implementations.Common;

namespace Veox.Attendance.Identity.Application.Services.Interfaces
{
    public interface IAuthService : IBaseService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest);
        Task<bool> ValidateAsync(string token);
    }
}