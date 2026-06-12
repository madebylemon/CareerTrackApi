using System;
using System.Security.Cryptography;

namespace CareerTrackApi.Application.Security
{
    // Lớp hỗ trợ mã hóa mật khẩu dùng PBKDF2
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

        // Băm mật khẩu ra chuỗi Hex
        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);
            return $"{Convert.ToHexString(salt)}.{Convert.ToHexString(hash)}";
        }

        // Kiểm tra mật khẩu băm
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                string[] parts = hashedPassword.Split('.', 2);
                if (parts.Length != 2) return false;

                byte[] salt = Convert.FromHexString(parts[0]);
                byte[] hash = Convert.FromHexString(parts[1]);

                byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);
                return CryptographicOperations.FixedTimeEquals(hash, inputHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
