using System;
using System.Collections.Generic;
using System.Linq;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Medicines - Entity Framework Version
    /// </summary>
    public class MedicineDAL
    {
        public MedicineDTO GetById(int medicineId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Medicines.Find(medicineId);
            }
        }

        public List<MedicineDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Medicines.OrderBy(m => m.MedicineName).ToList();
            }
        }

        public List<MedicineDTO> Search(string keyword)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Medicines
                    .Where(m => m.MedicineName.Contains(keyword))
                    .OrderBy(m => m.MedicineName)
                    .ToList();
            }
        }

        public int Insert(MedicineDTO medicine)
        {
            using (var db = new ClinicDbContext())
            {
                db.Medicines.Add(medicine);
                db.SaveChanges();
                return medicine.MedicineId;
            }
        }

        public bool Update(MedicineDTO medicine)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Medicines.Find(medicine.MedicineId);
                if (existing == null) return false;

                existing.MedicineName = medicine.MedicineName;
                existing.Unit = medicine.Unit;
                existing.Price = medicine.Price;
                existing.StockQuantity = medicine.StockQuantity;
                existing.MinStockLevel = medicine.MinStockLevel;

                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int medicineId)
        {
            using (var db = new ClinicDbContext())
            {
                var medicine = db.Medicines.Find(medicineId);
                if (medicine == null) return false;

                db.Medicines.Remove(medicine);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Cập nhật tồn kho
        /// </summary>
        public bool UpdateStock(int medicineId, int quantity)
        {
            using (var db = new ClinicDbContext())
            {
                var medicine = db.Medicines.Find(medicineId);
                if (medicine == null) return false;

                medicine.StockQuantity += quantity; // Có thể âm để giảm
                if (medicine.StockQuantity < 0) medicine.StockQuantity = 0;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Kiểm tra tồn kho đủ không
        /// </summary>
        public bool CheckStock(int medicineId, int requiredQuantity)
        {
            using (var db = new ClinicDbContext())
            {
                var medicine = db.Medicines.Find(medicineId);
                if (medicine == null) return false;
                return medicine.StockQuantity >= requiredQuantity;
            }
        }

        /// <summary>
        /// Lấy danh sách thuốc sắp hết
        /// </summary>
        public List<MedicineDTO> GetLowStock()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Medicines
                    .Where(m => m.StockQuantity <= m.MinStockLevel)
                    .OrderBy(m => m.StockQuantity)
                    .ToList();
            }
        }
    }
}
