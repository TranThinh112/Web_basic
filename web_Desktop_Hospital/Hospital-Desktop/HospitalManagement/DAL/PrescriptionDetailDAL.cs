using System;
using System.Collections.Generic;
using System.Linq;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng PrescriptionDetails - Entity Framework Version
    /// </summary>
    public class PrescriptionDetailDAL
    {
        public List<PrescriptionDetailDTO> GetByPrescriptionId(int prescriptionId)
        {
            using (var db = new ClinicDbContext())
            {
                var list = db.PrescriptionDetails
                    .Where(pd => pd.PrescriptionId == prescriptionId)
                    .ToList();
                
                // Load medicine info
                foreach (var pd in list)
                {
                    var med = db.Medicines.Find(pd.MedicineId);
                    if (med != null)
                    {
                        pd.MedicineName = med.MedicineName;
                        pd.Unit = med.Unit;
                        pd.Price = med.Price;
                    }
                }
                return list;
            }
        }

        // Alias for BLL compatibility
        public List<PrescriptionDetailDTO> GetByPrescription(int prescriptionId)
        {
            return GetByPrescriptionId(prescriptionId);
        }

        public bool Insert(PrescriptionDetailDTO detail)
        {
            using (var db = new ClinicDbContext())
            {
                db.PrescriptionDetails.Add(detail);
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(PrescriptionDetailDTO detail)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.PrescriptionDetails
                    .FirstOrDefault(pd => pd.PrescriptionId == detail.PrescriptionId && 
                                          pd.MedicineId == detail.MedicineId);
                if (existing == null) return false;

                existing.Quantity = detail.Quantity;
                existing.Usage = detail.Usage;

                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int prescriptionId, int medicineId)
        {
            using (var db = new ClinicDbContext())
            {
                var detail = db.PrescriptionDetails
                    .FirstOrDefault(pd => pd.PrescriptionId == prescriptionId && 
                                          pd.MedicineId == medicineId);
                if (detail == null) return false;

                db.PrescriptionDetails.Remove(detail);
                return db.SaveChanges() > 0;
            }
        }

        public bool DeleteByPrescriptionId(int prescriptionId)
        {
            using (var db = new ClinicDbContext())
            {
                var details = db.PrescriptionDetails
                    .Where(pd => pd.PrescriptionId == prescriptionId)
                    .ToList();
                
                if (!details.Any()) return true;

                db.PrescriptionDetails.RemoveRange(details);
                return db.SaveChanges() > 0;
            }
        }

        // Alias for BLL compatibility
        public bool DeleteByPrescription(int prescriptionId)
        {
            return DeleteByPrescriptionId(prescriptionId);
        }
    }
}
