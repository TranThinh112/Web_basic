using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// DTO cho bảng Employees - Thông tin nhân viên
    /// </summary>
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }  // Chức vụ: Bác sĩ, Lễ tân, Y tá, etc.
        public string Department { get; set; } // Phòng ban
        public DateTime HireDate { get; set; } // Ngày vào làm
        public int? UserId { get; set; }  // Liên kết với tài khoản đăng nhập

        // Navigation properties
        public string Username { get; set; }  // Tên tài khoản liên kết
    }
}
