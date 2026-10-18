using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng Patients
    /// </summary>
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string PatientCode { get; set; } // Mã bệnh nhân (BN001, BN002, ...)
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }  // Nam | Nữ | Khác
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CCCD { get; set; }
        public string MedicalHistory { get; set; }

        public PatientDTO()
        {
        }

        public PatientDTO(int patientId, string fullName, DateTime dateOfBirth, string gender, 
            string phone, string address, string cccd, string medicalHistory)
        {
            PatientId = patientId;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Phone = phone;
            Address = address;
            CCCD = cccd;
            MedicalHistory = medicalHistory;
        }

        /// <summary>
        /// Tính tuổi của bệnh nhân
        /// </summary>
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age))
                    age--;
                return age;
            }
        }
    }
}
