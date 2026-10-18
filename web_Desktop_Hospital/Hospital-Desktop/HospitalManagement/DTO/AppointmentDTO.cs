using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng Appointments (Lịch hẹn)
    /// </summary>
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }  // Lý do khám
        public string Status { get; set; }  // Pending | Confirmed | Completed | Cancelled

        // Navigation properties (để hiển thị)
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string PatientPhone { get; set; }

        public AppointmentDTO()
        {
            Status = "Pending";
        }

        public AppointmentDTO(int appointmentId, int patientId, int doctorId, 
            DateTime appointmentDate, string reason, string status)
        {
            AppointmentId = appointmentId;
            PatientId = patientId;
            DoctorId = doctorId;
            AppointmentDate = appointmentDate;
            Reason = reason;
            Status = status;
        }
    }
}
