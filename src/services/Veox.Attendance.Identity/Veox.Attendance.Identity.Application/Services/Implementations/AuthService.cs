using System;
using System.Threading.Tasks;
using Veox.Attendance.Identity.Application.Models;
using Veox.Attendance.Identity.Application.Services.Interfaces;

namespace Veox.Attendance.Identity.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Validate(string token)
        {
            throw new NotImplementedException();
        }
    }
}