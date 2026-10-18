using System;
using System.Collections.Generic;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.BLL
{
    public class UserBLL
    {
        private readonly UserDAL _userDAL;

        public UserBLL()
        {
            _userDAL = new UserDAL();
        }

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <summary>
        /// Đăng nhập hệ thống (Async)
        /// </summary>
        public async System.Threading.Tasks.Task<UserDTO> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống");

            // Async Call
            UserDTO user = await _userDAL.GetByUsernameAsync(username);
            
            // Generic Error Message
            string genericError = "Tên đăng nhập hoặc mật khẩu không chính xác";

            // Check existence and active status
            if (user == null || !user.IsActive)
                throw new Exception(genericError);

            // 1. Check Lockout Status
            if (user.LockoutEndTime.HasValue && user.LockoutEndTime.Value > DateTime.Now)
            {
                var secondsLeft = (int)(user.LockoutEndTime.Value - DateTime.Now).TotalSeconds;
                Logger.LogWarning($"Login blocked (Locked) for user: {username}");
                throw new Exception($"Tài khoản đang bị tạm khóa. Vui lòng thử lại sau {secondsLeft} giây.");
            }
            
            // 1b. Lockout đã hết hạn → Reset bộ đếm
            if (user.LockoutEndTime.HasValue && user.LockoutEndTime.Value <= DateTime.Now)
            {
                await _userDAL.ResetFailedAttemptsAsync(user.UserId);
                user.FailedLoginAttempts = 0;
                user.LockoutEndTime = null;
            }

            // Verify password based on HashVersion
            bool isValid = false;
            
            if (user.HashVersion == 2)
            {
                // BCrypt verification
                isValid = PasswordHelper.VerifyPasswordBCrypt(password, user.PasswordHash);
            }
            else if (string.IsNullOrEmpty(user.Salt))
            {
                // Legacy: SHA256 without salt (very old)
                isValid = PasswordHelper.VerifyPassword(password, user.PasswordHash);
            }
            else
            {
                // Legacy: SHA256 with salt
                string inputHash = PasswordHelper.HashPasswordWithSalt(password, user.Salt);
                isValid = inputHash.Equals(user.PasswordHash, StringComparison.OrdinalIgnoreCase);
            }

            // 2. Handle Failure
            if (!isValid)
            {
                await _userDAL.IncrementFailedAttemptsAsync(user.UserId);
                
                // Check if should lock (5 failed attempts → 30 second cooldown)
                if (user.FailedLoginAttempts + 1 >= 5)
                {
                    await _userDAL.LockAccountAsync(user.UserId, 0.5); // 0.5 minutes = 30 seconds
                    Logger.LogWarning($"Account LOCKED due to brute force: {username}");
                    throw new Exception("⚠️ Tài khoản đã bị khóa do nhập sai quá 5 lần.\nVui lòng thử lại sau 30 giây.");
                }

                Logger.LogWarning($"Failed login attempt for user: {username} (Attempt: {user.FailedLoginAttempts + 1}/5)");
                throw new Exception(genericError);
            }

            // 3. Auto-migrate to BCrypt on successful login
            if (user.HashVersion < 2)
            {
                string newHash = PasswordHelper.HashPasswordBCrypt(password);
                await _userDAL.UpdatePasswordBCryptAsync(user.UserId, newHash);
                Logger.LogAudit("BCRYPT_MIGRATION", username, $"Migrated password to BCrypt (from version {user.HashVersion})");
                
                user.PasswordHash = newHash;
                user.HashVersion = 2;
                user.Salt = null; // BCrypt includes salt in hash
            }

            // Reset Failures on success
            if (user.FailedLoginAttempts > 0 || user.LockoutEndTime != null)
            {
                await _userDAL.ResetFailedAttemptsAsync(user.UserId);
            }

            Logger.LogAudit("LOGIN", username, "Login successful");
            return user;
        }

        // Keep Sync Login for backward compatibility if needed, but LoginAsync is preferred.
        public UserDTO Login(string username, string password)
        {
             // ... Code cũ ...
             // Tạm thời có thể xóa hoặc giữ lại, nhưng để tránh duplicate logic, nên gọi .Result (không khuyến khích) hoặc giữ nguyên code cũ.
             // Ở đây tôi sẽ giữ nguyên Login cũ nhưng sửa lại để dùng DAL Sync Methods (đã có ở bước trước).
             // Tuy nhiên, để gọn, ta chỉ tập trung vào LoginAsync cho UI mới.
             return LoginAsync(username, password).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Đăng ký người dùng mới với mật khẩu tạm thời ngẫu nhiên
        /// </summary>
        /// <returns>Tuple (UserId, TempPassword) - TempPassword chỉ hiển thị 1 lần</returns>
        public (int UserId, string TempPassword) RegisterWithTempPassword(string username, string role)
        {
            if (!ValidationHelper.ValidateUsername(username))
                throw new ArgumentException("Tên đăng nhập không hợp lệ (3-50 ký tự, chỉ chữ cái, số, dấu gạch dưới)");

            if (_userDAL.UsernameExists(username))
                throw new Exception("Tên đăng nhập đã tồn tại");

            string[] validRoles = { "Admin", "Doctor", "Receptionist", "Cashier", "Pharmacist" };
            bool isValidRole = false;
            foreach (var r in validRoles)
            {
                if (r.Equals(role, StringComparison.OrdinalIgnoreCase))
                {
                    isValidRole = true;
                    role = r;
                    break;
                }
            }
            if (!isValidRole)
                throw new ArgumentException("Role không hợp lệ (Admin, Doctor, Receptionist, Cashier, Pharmacist)");

            // Security Check
            if (!SessionManager.IsAdmin())
                throw new UnauthorizedAccessException("Chỉ Admin mới có quyền tạo tài khoản");

            // Generate random temporary password
            string tempPassword = PasswordHelper.GenerateRandomPassword(12);
            
            // Use BCrypt for new users
            string bcryptHash = PasswordHelper.HashPasswordBCrypt(tempPassword);

            UserDTO user = new UserDTO
            {
                Username = username,
                PasswordHash = bcryptHash,
                Salt = null, // BCrypt includes salt
                Role = role,
                IsActive = true,
                MustChangePassword = true,
                HashVersion = 2 // BCrypt
            };

            int newId = _userDAL.Insert(user);
            Logger.LogAudit("REGISTER", SessionManager.CurrentUser?.Username ?? "System", $"Created user: {username} ({role}) with BCrypt");
            
            return (newId, tempPassword);
        }

        /// <summary>
        /// Đăng ký người dùng mới (Legacy - giữ lại cho tương thích)
        /// </summary>
        [Obsolete("Use RegisterWithTempPassword instead for better security")]
        public int Register(string username, string password, string role)
        {
            if (!ValidationHelper.ValidateUsername(username))
                throw new ArgumentException("Tên đăng nhập không hợp lệ (3-50 ký tự, chỉ chữ cái, số, dấu gạch dưới)");

            if (!ValidationHelper.ValidatePassword(password))
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự");

            if (_userDAL.UsernameExists(username))
                throw new Exception("Tên đăng nhập đã tồn tại");

            string[] validRoles = { "Admin", "Doctor", "Receptionist", "Cashier", "Pharmacist" };
            bool isValidRole = false;
            foreach (var r in validRoles)
            {
                if (r.Equals(role, StringComparison.OrdinalIgnoreCase))
                {
                    isValidRole = true;
                    role = r;
                    break;
                }
            }
            if (!isValidRole)
                throw new ArgumentException("Role không hợp lệ (Admin, Doctor, Receptionist, Cashier, Pharmacist)");

            if (!SessionManager.IsAdmin())
                throw new UnauthorizedAccessException("Chỉ Admin mới có quyền tạo tài khoản");

            if (!ValidationHelper.ValidateStrongPassword(password))
                throw new ArgumentException("Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt");

            string newSalt = PasswordHelper.GenerateSalt();
            string newHash = PasswordHelper.HashPasswordWithSalt(password, newSalt);

            UserDTO user = new UserDTO
            {
                Username = username,
                PasswordHash = newHash,
                Salt = newSalt,
                Role = role,
                IsActive = true
            };

            int newId = _userDAL.Insert(user);
            Logger.LogAudit("REGISTER", SessionManager.CurrentUser?.Username ?? "System", $"Created user: {username} ({role})");
            return newId;
        }

        /// <summary>
        /// Lấy tất cả users (chỉ Admin)
        /// </summary>
        public List<UserDTO> GetAllUsers()
        {
            return _userDAL.GetAll();
        }

        /// <summary>
        /// Lấy users theo role
        /// </summary>
        public List<UserDTO> GetUsersByRole(string role)
        {
            return _userDAL.GetByRole(role);
        }

        /// <summary>
        /// Lấy user theo Id
        /// </summary>
        public UserDTO GetUserById(int userId)
        {
            return _userDAL.GetById(userId);
        }

        /// <summary>
        /// Cập nhật thông tin user
        /// </summary>
        public bool UpdateUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (!ValidationHelper.ValidateUsername(user.Username))
                throw new ArgumentException("Tên đăng nhập không hợp lệ");

            return _userDAL.Update(user);
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            UserDTO user = _userDAL.GetById(userId);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            // Verify old password based on HashVersion
            bool isValid = false;
            if (user.HashVersion == 2)
            {
                isValid = PasswordHelper.VerifyPasswordBCrypt(oldPassword, user.PasswordHash);
            }
            else if (string.IsNullOrEmpty(user.Salt))
            {
                isValid = PasswordHelper.VerifyPassword(oldPassword, user.PasswordHash);
            }
            else
            {
                string oldHashCheck = PasswordHelper.HashPasswordWithSalt(oldPassword, user.Salt);
                isValid = oldHashCheck.Equals(user.PasswordHash, StringComparison.OrdinalIgnoreCase);
            }
            
            if (!isValid)
                throw new Exception("Mật khẩu cũ không chính xác");

            // Enforce Strong Password Policy
            if (!ValidationHelper.ValidateStrongPassword(newPassword))
                throw new ArgumentException("Mật khẩu mới phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt");

            // Use BCrypt for new password
            string bcryptHash = PasswordHelper.HashPasswordBCrypt(newPassword);
            
            bool result = _userDAL.UpdatePasswordBCrypt(userId, bcryptHash, false);
            if (result)
            {
                Logger.LogAudit("CHANGE_PASSWORD", SessionManager.CurrentUser?.Username ?? "Unknown", $"Changed password to BCrypt for UserId: {userId}");
            }
            return result;
        }

        /// <summary>
        /// Đổi mật khẩu bắt buộc (không cần mật khẩu cũ - dùng sau khi reset)
        /// </summary>
        public bool ForceChangePassword(int userId, string newPassword)
        {
            UserDTO user = _userDAL.GetById(userId);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            // Enforce Strong Password Policy
            if (!ValidationHelper.ValidateStrongPassword(newPassword))
                throw new ArgumentException("Mật khẩu mới phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt");

            // Use BCrypt
            string bcryptHash = PasswordHelper.HashPasswordBCrypt(newPassword);
            
            bool result = _userDAL.UpdatePasswordBCrypt(userId, bcryptHash, false);
            if (result)
            {
                Logger.LogAudit("FORCE_CHANGE_PASSWORD", user.Username, $"Force changed to BCrypt for UserId: {userId}");
            }
            return result;
        }

        /// <summary>
        /// Reset mật khẩu với mật khẩu ngẫu nhiên (Admin)
        /// </summary>
        /// <returns>Mật khẩu tạm thời (chỉ hiển thị 1 lần cho Admin)</returns>
        public string ResetPasswordWithRandomPassword(int userId)
        {
            // Security Check
            if (!SessionManager.IsAdmin())
                throw new UnauthorizedAccessException("Chỉ Admin mới có quyền reset mật khẩu");

            UserDTO user = _userDAL.GetById(userId);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            // Generate random temporary password
            string tempPassword = PasswordHelper.GenerateRandomPassword(12);

            // Use BCrypt
            string bcryptHash = PasswordHelper.HashPasswordBCrypt(tempPassword);
            
            // Update with MustChangePassword = true
            bool result = _userDAL.UpdatePasswordBCrypt(userId, bcryptHash, true);
            
            if (result)
            {
                Logger.LogAudit("RESET_PASSWORD", SessionManager.CurrentUser.Username, $"Reset to BCrypt for user: {user.Username} (ID: {userId})");
                return tempPassword;
            }
            
            throw new Exception("Không thể reset mật khẩu");
        }

        /// <summary>
        /// Reset mật khẩu (Legacy - giữ lại cho tương thích)
        /// </summary>
        [Obsolete("Use ResetPasswordWithRandomPassword instead for better security")]
        public bool ResetPassword(int userId, string newPassword)
        {
            if (!SessionManager.IsAdmin())
                throw new UnauthorizedAccessException("Chỉ Admin mới có quyền reset mật khẩu");

            if (!ValidationHelper.ValidateStrongPassword(newPassword))
                throw new ArgumentException("Mật khẩu mới phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt");

            string newSalt = PasswordHelper.GenerateSalt();
            string newHash = PasswordHelper.HashPasswordWithSalt(newPassword, newSalt);
            
            bool result = _userDAL.UpdatePasswordWithMustChange(userId, newHash, newSalt, true);
            if (result) Logger.LogAudit("RESET_PASSWORD", SessionManager.CurrentUser.Username, $"Reset password for UserId: {userId}");
            return result;
        }

        /// <summary>
        /// Xóa user (soft delete)
        /// </summary>
        public bool DeleteUser(int userId)
        {
            return _userDAL.Delete(userId);
        }

        /// <summary>
        /// Kích hoạt lại tài khoản bị vô hiệu hóa
        /// </summary>
        public bool ActivateUser(int userId)
        {
            return _userDAL.Activate(userId);
        }

        /// <summary>
        /// Kiểm tra quyền
        /// </summary>
        public bool ValidateRole(int userId, string requiredRole)
        {
            UserDTO user = _userDAL.GetById(userId);
            if (user == null || !user.IsActive)
                return false;

            return user.Role.Equals(requiredRole, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Kiểm tra user có phải Admin không
        /// </summary>
        public bool IsAdmin(int userId)
        {
            return ValidateRole(userId, "Admin");
        }
    }
}
