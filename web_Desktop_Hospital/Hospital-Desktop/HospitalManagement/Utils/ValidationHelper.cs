using System;
using System.Text.RegularExpressions;

namespace HospitalManagement.Utils
{
    /// <summary>
    /// Helper class để validate dữ liệu đầu vào
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Validate số điện thoại Việt Nam (10-11 số, bắt đầu bằng 0)
        /// </summary>
        /// <param name="phone">Số điện thoại cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Số điện thoại Việt Nam: 10-11 số, bắt đầu bằng 0
            // Ví dụ: 0901234567, 0123456789
            string pattern = @"^0\d{9,10}$";
            return Regex.IsMatch(phone.Trim(), pattern);
        }

        /// <summary>
        /// Validate CCCD (Căn cước công dân - 12 số)
        /// </summary>
        /// <param name="cccd">Số CCCD cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateCCCD(string cccd)
        {
            if (string.IsNullOrWhiteSpace(cccd))
                return false;

            // CCCD gồm 12 số
            string pattern = @"^\d{12}$";
            return Regex.IsMatch(cccd.Trim(), pattern);
        }

        /// <summary>
        /// Validate CMND cũ (9 số)
        /// </summary>
        /// <param name="cmnd">Số CMND cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateCMND(string cmnd)
        {
            if (string.IsNullOrWhiteSpace(cmnd))
                return false;

            // CMND cũ gồm 9 số
            string pattern = @"^\d{9}$";
            return Regex.IsMatch(cmnd.Trim(), pattern);
        }

        /// <summary>
        /// Validate CCCD hoặc CMND
        /// </summary>
        /// <param name="id">Số giấy tờ</param>
        /// <returns>True nếu là CCCD hoặc CMND hợp lệ</returns>
        public static bool ValidateIdentityCard(string id)
        {
            return ValidateCCCD(id) || ValidateCMND(id);
        }

        /// <summary>
        /// Validate địa chỉ email
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email.Trim(), pattern);
        }

        /// <summary>
        /// Validate tên người dùng (không chứa ký tự đặc biệt, 3-50 ký tự)
        /// </summary>
        /// <param name="username">Username cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            // Username: 3-50 ký tự, chỉ chữ cái, số và dấu gạch dưới
            string pattern = @"^[a-zA-Z0-9_]{3,50}$";
            return Regex.IsMatch(username.Trim(), pattern);
        }

        /// <summary>
        /// Validate họ tên (chứa chữ cái và khoảng trắng, 2-100 ký tự)
        /// </summary>
        /// <param name="fullName">Họ tên cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            // Họ tên: 2-100 ký tự, chữ cái (có dấu) và khoảng trắng
            string trimmed = fullName.Trim();
            if (trimmed.Length < 2 || trimmed.Length > 100)
                return false;

            // Chấp nhận chữ cái Unicode (tiếng Việt) và khoảng trắng
            string pattern = @"^[\p{L}\s]+$";
            return Regex.IsMatch(trimmed, pattern);
        }

        /// <summary>
        /// Validate mật khẩu (ít nhất 6 ký tự)
        /// </summary>
        /// <param name="password">Mật khẩu cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            // Mật khẩu tối thiểu 6 ký tự
            return password.Length >= 6;
        }

        /// <summary>
        /// Validate mật khẩu mạnh (chữ hoa, chữ thường, số, ký tự đặc biệt, ít nhất 8 ký tự)
        /// </summary>
        /// <param name="password">Mật khẩu cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateStrongPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;

            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]");

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        /// <summary>
        /// Validate ngày sinh (không trong tương lai, không quá 150 năm trước)
        /// </summary>
        /// <param name="dateOfBirth">Ngày sinh cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateDateOfBirth(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            DateTime minDate = today.AddYears(-150);

            return dateOfBirth <= today && dateOfBirth >= minDate;
        }

        /// <summary>
        /// Validate giới tính
        /// </summary>
        /// <param name="gender">Giới tính (Nam/Nữ/Khác)</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateGender(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
                return false;

            string[] validGenders = { "Nam", "Nữ", "Khác", "Male", "Female", "Other" };
            foreach (var g in validGenders)
            {
                if (gender.Trim().Equals(g, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Validate số tiền (không âm)
        /// </summary>
        /// <param name="amount">Số tiền cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateAmount(decimal amount)
        {
            return amount >= 0;
        }

        /// <summary>
        /// Validate số lượng (số nguyên dương)
        /// </summary>
        /// <param name="quantity">Số lượng cần kiểm tra</param>
        /// <returns>True nếu hợp lệ</returns>
        public static bool ValidateQuantity(int quantity)
        {
            return quantity > 0;
        }
    }
}
