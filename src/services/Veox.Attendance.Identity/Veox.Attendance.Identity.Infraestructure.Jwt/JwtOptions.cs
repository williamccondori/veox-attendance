namespace Veox.Attendance.Identity.Infraestructure.Jwt
{
    public class JwtOptions
    {
        public string SecretId { get; set; }
        public double ExpiryMinutes { get; set; }
    }
}