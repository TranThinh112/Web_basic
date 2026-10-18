using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// DTO cho chi tiết bán thuốc
    /// </summary>
    public class MedicineSaleDetailDTO
    {
        public int SaleDetailId { get; set; }
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Computed property
        public decimal LineTotal
        {
            get { return Quantity * UnitPrice; }
        }

        // Navigation properties
        public string MedicineName { get; set; }
        public string Unit { get; set; }

        public MedicineSaleDetailDTO()
        {
            Quantity = 1;
        }

        public MedicineSaleDetailDTO(int medicineId, int quantity, decimal unitPrice)
        {
            MedicineId = medicineId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
