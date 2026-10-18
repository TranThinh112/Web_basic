using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng Doctors
    /// </summary>
    public class DoctorDTO
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }  // Chuyên khoa
        public int UserId { get; set; }  // FK to Users

        // Navigation properties (để hiển thị)
        public string Username { get; set; }

        public DoctorDTO()
        {
        }

        public DoctorDTO(int doctorId, string fullName, string specialty, int userId)
        {
            DoctorId = doctorId;
            FullName = fullName;
            Specialty = specialty;
            UserId = userId;
        }
    }
}
