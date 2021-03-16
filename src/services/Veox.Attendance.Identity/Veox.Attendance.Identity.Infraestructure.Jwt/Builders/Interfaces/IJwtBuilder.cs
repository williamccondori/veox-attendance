using System.Collections.Generic;

namespace Veox.Attendance.Identity.Infraestructure.Jwt.Builders.Interfaces
{
    public interface IJwtBuilder
    {
        string GenerateToken(Dictionary<string, string> values);
        string ValidateAuthToken(string token);
    }
}