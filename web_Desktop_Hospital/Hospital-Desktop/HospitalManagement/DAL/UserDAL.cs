using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Users - Entity Framework Version
    /// </summary>
    public class UserDAL
    {
        // ==================================================================================
        // ASYNC METHODS (New for Performance)
        // ==================================================================================

        public async System.Threading.Tasks.Task<UserDTO> GetByUsernameAsync(string username)
        {
            using (var db = new ClinicDbContext())
            {
                return await db.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
        }

        public async System.Threading.Tasks.Task IncrementFailedAttemptsAsync(int userId)
        {
            using (var db = new ClinicDbContext())
            {
                var user = await db.Users.FindAsync(userId);
                if (user != null)
                {
                    user.FailedLoginAttempts++;
                    await db.SaveChangesAsync();
                }
            }
        }

        public async System.Threading.Tasks.Task ResetFailedAttemptsAsync(int userId)
        {
            using (var db = new ClinicDbContext())
            {
                var user = await db.Users.FindAsync(userId);
                if (user != null)
                {
                    user.FailedLoginAttempts = 0;
                    user.LockoutEndTime = null;
                    await db.SaveChangesAsync();
                }
            }
        }

        public async System.Threading.Tasks.Task LockAccountAsync(int userId, double durationMinutes)
        {
            using (var db = new ClinicDbContext())
            {
                var user = await db.Users.FindAsync(userId);
                if (user != null)
                {
                    user.LockoutEndTime = DateTime.Now.AddMinutes(durationMinutes);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async System.Threading.Tasks.Task<bool> UpdatePasswordAsync(int userId, string passwordHash, string salt)
        {
            using (var db = new ClinicDbContext())
            {
                var user = await db.Users.FindAsync(userId);
                if (user == null) return false;

                user.PasswordHash = passwordHash;
                user.Salt = salt;
                return await db.SaveChangesAsync() > 0;
            }
        }

        /// <summary>
        /// Update password với BCrypt (HashVersion = 2)
        /// </summary>
        public async System.Threading.Tasks.Task<bool> UpdatePasswordBCryptAsync(int userId, string bcryptHash)
        {
            using (var db = new ClinicDbContext())
            {
                var user = await db.Users.FindAsync(userId);
                if (user == null) return false;

                user.PasswordHash = bcryptHash;
                user.Salt = null; // BCrypt includes salt in hash
                user.HashVersion = 2;
                return await db.SaveChangesAsync() > 0;
            }
        }

        // ==================================================================================
        // SYNC METHODS (Legacy & Core Functionality)
        // ==================================================================================

        public UserDTO GetByUsername(string username)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Users.FirstOrDefault(u => u.Username == username);
            }
        }

        public UserDTO GetById(int userId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Users.Find(userId);
            }
        }

        public List<UserDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Users.OrderBy(u => u.Username).ToList();
            }
        }

        public List<UserDTO> GetByRole(string role)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Users
                    .Where(u => u.Role == role)
                    .OrderBy(u => u.Username)
                    .ToList();
            }
        }

        public int Insert(UserDTO user)
        {
            using (var db = new ClinicDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user.UserId;
            }
        }

        public bool Update(UserDTO user)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Users.Find(user.UserId);
                if (existing == null) return false;

                existing.Username = user.Username;
                existing.Role = user.Role;
                existing.IsActive = user.IsActive;

                return db.SaveChanges() > 0;
            }
        }

        public bool UpdatePassword(int userId, string passwordHash, string salt)
        {
            using (var db = new ClinicDbContext())
            {
                var user = db.Users.Find(userId);
                if (user == null) return false;

                user.PasswordHash = passwordHash;
                user.Salt = salt;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Cập nhật mật khẩu và set cờ MustChangePassword
        /// </summary>
        public bool UpdatePasswordWithMustChange(int userId, string passwordHash, string salt, bool mustChangePassword)
        {
            using (var db = new ClinicDbContext())
            {
                var user = db.Users.Find(userId);
                if (user == null) return false;

                user.PasswordHash = passwordHash;
                user.Salt = salt;
                user.MustChangePassword = mustChangePassword;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Update password với BCrypt (HashVersion = 2), clear MustChangePassword
        /// </summary>
        public bool UpdatePasswordBCrypt(int userId, string bcryptHash, bool mustChangePassword = false)
        {
            using (var db = new ClinicDbContext())
            {
                var user = db.Users.Find(userId);
                if (user == null) return false;

                user.PasswordHash = bcryptHash;
                user.Salt = null; // BCrypt includes salt
                user.HashVersion = 2;
                user.MustChangePassword = mustChangePassword;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Set cờ MustChangePassword
        /// </summary>
        public bool SetMustChangePassword(int userId, bool value)
        {
            using (var db = new ClinicDbContext())
            {
                var user = db.Users.Find(userId);
                if (user == null) return false;

                user.MustChangePassword = value;
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int userId)
        {
            using (var db = new ClinicDbContext())
            {
                var user = db.Users.Find(userId);
                if (user == null) return false;

                user.IsActive = false;
                return db.SaveChanges() > 0;
            }
        }

        public bool Activate(int userId)
        {
            using (var db = new ClinicDbContext())
            {
                var user = db.Users.Find(userId);
                if (user == null) return false;

                user.IsActive = true;
                return db.SaveChanges() > 0;
            }
        }

        public bool UsernameExists(string username)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Users.Any(u => u.Username == username);
            }
        }

        // Lockout Sync Methods (for fallback)
        public void IncrementFailedAttempts(int userId)
        {
            using (var db = new ClinicDbContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    user.FailedLoginAttempts++;
                    db.SaveChanges();
                }
            }
        }

        public void ResetFailedAttempts(int userId)
        {
            using (var db = new ClinicDbContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    user.FailedLoginAttempts = 0;
                    user.LockoutEndTime = null;
                    db.SaveChanges();
                }
            }
        }

        public void LockAccount(int userId, int durationMinutes)
        {
            using (var db = new ClinicDbContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    user.LockoutEndTime = DateTime.Now.AddMinutes(durationMinutes);
                    db.SaveChanges();
                }
            }
        }
    }
}
