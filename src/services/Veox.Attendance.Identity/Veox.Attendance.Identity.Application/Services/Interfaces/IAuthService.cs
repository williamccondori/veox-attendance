using System.Threading.Tasks;
using Veox.Attendance.Identity.Application.Models;

namespace Veox.Attendance.Identity.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);
        Task<bool> Validate(string token);
    }
}