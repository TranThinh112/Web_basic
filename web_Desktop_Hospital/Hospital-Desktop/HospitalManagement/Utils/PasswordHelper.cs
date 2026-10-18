using System;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagement.Utils
{
    /// <summary>
    /// Helper class để hash và xác thực mật khẩu sử dụng SHA256
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Hash mật khẩu sử dụng SHA256
        /// </summary>
        /// <param name="password">Mật khẩu dạng plaintext</param>
        /// <returns>Chuỗi hash dạng hex</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Mật khẩu không được để trống");

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Xác thực mật khẩu với hash đã lưu
        /// </summary>
        /// <param name="password">Mật khẩu plaintext cần kiểm tra</param>
        /// <param name="hashedPassword">Hash đã lưu trong database</param>
        /// <returns>True nếu khớp, False nếu không khớp</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            string hashOfInput = HashPassword(password);
            return hashOfInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Tạo salt ngẫu nhiên (dự phòng cho tương lai nếu cần nâng cấp)
        /// </summary>
        /// <param name="size">Kích thước salt</param>
        /// <returns>Salt dạng base64 string</returns>
        public static string GenerateSalt(int size = 32)
        {
            byte[] salt = new byte[size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Hash mật khẩu với salt (SHA256)
        /// </summary>
        /// <param name="password">Mật khẩu</param>
        /// <param name="salt">Salt (Base64 string)</param>
        /// <returns>Hash kết hợp với salt</returns>
        public static string HashPasswordWithSalt(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Mật khẩu không được để trống", nameof(password));
            if (string.IsNullOrEmpty(salt))
                throw new ArgumentException("Salt không được để trống", nameof(salt));

            // Hash(password + salt)
            return HashPassword(password + salt);
        }

        /// <summary>
        /// Sinh mật khẩu ngẫu nhiên an toàn (cryptographically secure)
        /// Tuân thủ chính sách mật khẩu mạnh: chữ hoa, chữ thường, số, ký tự đặc biệt
        /// </summary>
        /// <param name="length">Độ dài mật khẩu (mặc định 12)</param>
        /// <returns>Mật khẩu plaintext (chỉ hiển thị 1 lần)</returns>
        public static string GenerateRandomPassword(int length = 12)
        {
            if (length < 8) length = 8; // Minimum for strong password

            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*";
            const string allChars = upperChars + lowerChars + digitChars + specialChars;

            using (var rng = new RNGCryptoServiceProvider())
            {
                var password = new char[length];
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                // Ensure at least one of each required type
                password[0] = upperChars[randomBytes[0] % upperChars.Length];
                password[1] = lowerChars[randomBytes[1] % lowerChars.Length];
                password[2] = digitChars[randomBytes[2] % digitChars.Length];
                password[3] = specialChars[randomBytes[3] % specialChars.Length];

                // Fill remaining with random from all chars
                for (int i = 4; i < length; i++)
                {
                    password[i] = allChars[randomBytes[i] % allChars.Length];
                }

                // Shuffle to avoid predictable pattern
                for (int i = length - 1; i > 0; i--)
                {
                    int j = randomBytes[i] % (i + 1);
                    char temp = password[i];
                    password[i] = password[j];
                    password[j] = temp;
                }

                return new string(password);
            }
        }

        // ==================================================================================
        // BCRYPT METHODS (Recommended - Industry Standard)
        // ==================================================================================

        /// <summary>
        /// Hash mật khẩu với BCrypt (salt tự động embed trong hash)
        /// WorkFactor = 12 (~250ms trên máy hiện đại, đủ chậm để chống brute-force)
        /// </summary>
        /// <param name="password">Mật khẩu plaintext</param>
        /// <returns>BCrypt hash (đã bao gồm salt)</returns>
        public static string HashPasswordBCrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Mật khẩu không được để trống", nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        /// <summary>
        /// Xác thực mật khẩu với BCrypt hash
        /// </summary>
        /// <param name="password">Mật khẩu plaintext cần kiểm tra</param>
        /// <param name="hash">Hash đã lưu trong database</param>
        /// <returns>True nếu khớp</returns>
        public static bool VerifyPasswordBCrypt(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch
            {
                // Invalid hash format
                return false;
            }
        }
    }
}
