using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng Prescriptions (Đơn thuốc)
    /// </summary>
    public class PrescriptionDTO
    {
        public int PrescriptionId { get; set; }
        public int VisitId { get; set; }  // FK to Visits
        public DateTime CreatedDate { get; set; }

        // Navigation properties (để hiển thị)
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public PrescriptionDTO()
        {
            CreatedDate = DateTime.Now;
        }

        public PrescriptionDTO(int prescriptionId, int visitId, DateTime createdDate)
        {
            PrescriptionId = prescriptionId;
            VisitId = visitId;
            CreatedDate = createdDate;
        }
    }
}
