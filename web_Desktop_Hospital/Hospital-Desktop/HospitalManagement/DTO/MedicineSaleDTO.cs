using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// DTO cho bán thuốc tại quầy dược
    /// </summary>
    public class MedicineSaleDTO
    {
        public int SaleId { get; set; }
        public string SaleCode { get; set; }          // Auto-generated: MS-YYYYMMDD-XXX
        public int? PatientId { get; set; }           // Null nếu khách vãng lai
        public int? VisitId { get; set; }             // Liên kết lượt khám
        public int? PrescriptionId { get; set; }      // Liên kết đơn thuốc BS
        public int PharmacistId { get; set; }         // Dược sĩ tạo đơn
        public int? CashierId { get; set; }           // Thu ngân thu tiền
        public DateTime SaleDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }     // Cash | Card | Transfer
        public string Status { get; set; }            // Pending | Paid | Dispensed | Cancelled
        public string Notes { get; set; }
        public bool IsWalkIn { get; set; }            // True nếu khách vãng lai
        public string WalkInCustomerName { get; set; } // Tên khách vãng lai (nếu có)

        // Navigation properties
        public string PatientName { get; set; }
        public string PharmacistName { get; set; }
        public string CashierName { get; set; }

        // Status display
        public string StatusDisplay
        {
            get
            {
                switch (Status)
                {
                    case "Pending": return "⏳ Chờ thanh toán";
                    case "Paid": return "💰 Đã thanh toán";
                    case "Dispensed": return "✅ Đã phát thuốc";
                    case "Cancelled": return "❌ Đã hủy";
                    default: return Status;
                }
            }
        }

        public MedicineSaleDTO()
        {
            SaleDate = DateTime.Now;
            Status = "Pending";
            PaymentMethod = "Cash";
            TotalAmount = 0;
        }
    }
}
