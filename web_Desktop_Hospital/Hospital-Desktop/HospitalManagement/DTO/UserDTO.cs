using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng Users
    /// </summary>
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }  // Receptionist | Doctor | Admin
        public bool IsActive { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime? LockoutEndTime { get; set; }
        public bool MustChangePassword { get; set; }
        public int HashVersion { get; set; } // 1=SHA256, 2=BCrypt

        public UserDTO()
        {
            IsActive = true;
            FailedLoginAttempts = 0;
            MustChangePassword = false;
        }

        public UserDTO(int userId, string username, string passwordHash, string salt, string role, bool isActive)
        {
            UserId = userId;
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
            Role = role;
            IsActive = isActive;
            FailedLoginAttempts = 0;
            MustChangePassword = false;
        }
    }
}
