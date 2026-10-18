using System;
using System.Collections.Generic;
using System.Linq;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    public class MedicineSaleDetailDAL
    {
        public int Insert(MedicineSaleDetailDTO detail)
        {
            using (var db = new ClinicDbContext())
            {
                db.MedicineSaleDetails.Add(detail);
                db.SaveChanges();
                return detail.SaleDetailId;
            }
        }

        public void InsertMany(List<MedicineSaleDetailDTO> details)
        {
            using (var db = new ClinicDbContext())
            {
                db.MedicineSaleDetails.AddRange(details);
                db.SaveChanges();
            }
        }

        public List<MedicineSaleDetailDTO> GetBySale(int saleId)
        {
            using (var db = new ClinicDbContext())
            {
                return (from d in db.MedicineSaleDetails
                        join m in db.Medicines on d.MedicineId equals m.MedicineId
                        where d.SaleId == saleId
                        select new
                        {
                            Detail = d,
                            m.MedicineName,
                            m.Unit
                        })
                        .ToList()
                        .Select(x =>
                        {
                            x.Detail.MedicineName = x.MedicineName;
                            x.Detail.Unit = x.Unit;
                            return x.Detail;
                        })
                        .ToList();
            }
        }

        public bool Update(MedicineSaleDetailDTO detail)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.MedicineSaleDetails.Find(detail.SaleDetailId);
                if (existing == null) return false;

                existing.Quantity = detail.Quantity;
                existing.UnitPrice = detail.UnitPrice;

                db.SaveChanges();
                return true;
            }
        }

        public bool Delete(int detailId)
        {
            using (var db = new ClinicDbContext())
            {
                var detail = db.MedicineSaleDetails.Find(detailId);
                if (detail == null) return false;

                db.MedicineSaleDetails.Remove(detail);
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteBySale(int saleId)
        {
            using (var db = new ClinicDbContext())
            {
                var details = db.MedicineSaleDetails.Where(d => d.SaleId == saleId).ToList();
                db.MedicineSaleDetails.RemoveRange(details);
                db.SaveChanges();
                return true;
            }
        }
    }
}
