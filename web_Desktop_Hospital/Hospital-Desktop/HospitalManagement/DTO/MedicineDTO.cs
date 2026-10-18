using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng Medicines
    /// </summary>
    public class MedicineDTO
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Unit { get; set; }  // Đơn vị: viên, gói, chai, ống...
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }  // Tồn kho
        public int MinStockLevel { get; set; }  // Mức tồn tối thiểu

        public MedicineDTO()
        {
        }

        public MedicineDTO(int medicineId, string medicineName, string unit, decimal price)
        {
            MedicineId = medicineId;
            MedicineName = medicineName;
            Unit = unit;
            Price = price;
        }
    }
}
