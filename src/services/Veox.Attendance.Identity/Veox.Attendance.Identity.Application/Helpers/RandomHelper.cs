using System;

namespace Veox.Attendance.Identity.Application.Helpers
{
    public static class RandomHelper
    {
        public static string GenerateActivationCode()
        {
            var generator = new Random();

            var activationCode = generator.Next(0, 1000000).ToString("D6");

            return activationCode;
        }
    }
}