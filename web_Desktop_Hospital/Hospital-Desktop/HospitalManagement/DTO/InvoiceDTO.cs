using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng Invoices (Hóa đơn)
    /// </summary>
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public int VisitId { get; set; }  // FK to Visits
        public int PatientId { get; set; }  // FK to Patients
        public string InvoiceCode { get; set; }  // Auto-generated: INV-YYYYMMDD-XXX
        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentMethod { get; set; }  // Cash | Card | Transfer
        public string Status { get; set; }  // Pending | Paid | Cancelled

        // Navigation properties (để hiển thị)
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public InvoiceDTO()
        {
            Status = "Pending";
            PaymentMethod = "Cash";
        }

        public InvoiceDTO(int invoiceId, int visitId, int patientId, decimal totalAmount,
            DateTime paymentDate, string paymentMethod, string status)
        {
            InvoiceId = invoiceId;
            VisitId = visitId;
            PatientId = patientId;
            TotalAmount = totalAmount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            Status = status;
        }
    }
}
