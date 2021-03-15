using System;
using System.Security.Cryptography;

namespace Veox.Attendance.Identity.Application.Helpers
{
    public static class EncryptorHelper
    {
        private const int SALT_SIZE = 40;
        private const int ITERATIONS_COUNT = 10000;

        public static string GetPasswordKey()
        {
            var secret = new byte[SALT_SIZE];
            var random = RandomNumberGenerator.Create();
            random.GetBytes(secret);
            return Convert.ToBase64String(secret);
        }

        public static string GetHash(string value, string key)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(key), ITERATIONS_COUNT);
            return Convert.ToBase64String(pbkdf2.GetBytes(SALT_SIZE));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length + sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}