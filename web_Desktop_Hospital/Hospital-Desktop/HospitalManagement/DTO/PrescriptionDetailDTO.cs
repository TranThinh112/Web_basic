using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng PrescriptionDetails (Chi tiết đơn thuốc)
    /// </summary>
    public class PrescriptionDetailDTO
    {
        public int PrescriptionId { get; set; }  // FK to Prescriptions
        public int MedicineId { get; set; }  // FK to Medicines
        public int Quantity { get; set; }
        public string Usage { get; set; }  // Cách dùng

        // Navigation properties (để hiển thị)
        public string MedicineName { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }

        public PrescriptionDetailDTO()
        {
        }

        public PrescriptionDetailDTO(int prescriptionId, int medicineId, int quantity, string usage)
        {
            PrescriptionId = prescriptionId;
            MedicineId = medicineId;
            Quantity = quantity;
            Usage = usage;
        }

        /// <summary>
        /// Tính thành tiền cho dòng chi tiết này
        /// </summary>
        public decimal LineTotal
        {
            get { return Quantity * UnitPrice; }
        }

        // Alias properties for compatibility
        public decimal Price
        {
            get { return UnitPrice; }
            set { UnitPrice = value; }
        }

        public decimal TotalPrice
        {
            get { return LineTotal; }
        }
    }
}
