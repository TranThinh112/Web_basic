using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng Visits (Lượt khám)
    /// Status: Waiting (Chờ khám), InProgress (Đang khám), Completed (Đã xong)
    /// </summary>
    public class VisitDTO
    {
        public int VisitId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Symptoms { get; set; }  // Triệu chứng
        public string Diagnosis { get; set; }  // Chẩn đoán
        public int? DiseaseId { get; set; }  // FK to Diseases (nullable)
        public string Status { get; set; }  // Waiting | InProgress | Completed

        // Navigation properties (để hiển thị)
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string DiseaseName { get; set; }
        public string MedicalHistory { get; set; }  // Tiền sử bệnh từ bệnh nhân

        /// <summary>
        /// Hiển thị trạng thái bằng tiếng Việt
        /// </summary>
        public string StatusDisplay
        {
            get
            {
                switch (Status)
                {
                    case "Waiting": return "Chờ khám";
                    case "InProgress": return "Đang khám";
                    case "Completed": return "Đã xong";
                    default: return Status ?? "Chờ khám";
                }
            }
        }

        public VisitDTO()
        {
            VisitDate = DateTime.Now;
            Status = "Waiting";
        }

        public VisitDTO(int visitId, int patientId, int doctorId, DateTime visitDate, 
            string symptoms, string diagnosis, int? diseaseId)
        {
            VisitId = visitId;
            PatientId = patientId;
            DoctorId = doctorId;
            VisitDate = visitDate;
            Symptoms = symptoms;
            Diagnosis = diagnosis;
            DiseaseId = diseaseId;
            Status = "Waiting";
        }
    }
}
