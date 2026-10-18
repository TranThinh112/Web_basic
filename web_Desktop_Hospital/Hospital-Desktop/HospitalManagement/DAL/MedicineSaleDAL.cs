using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    public class MedicineSaleDAL
    {
        public int Insert(MedicineSaleDTO sale)
        {
            using (var db = new ClinicDbContext())
            {
                // Generate SaleCode BEFORE insert - based on daily count
                if (string.IsNullOrEmpty(sale.SaleCode))
                {
                    sale.SaleCode = GenerateSaleCode(db, sale.SaleDate);
                }
                
                db.MedicineSales.Add(sale);
                db.SaveChanges();
                
                return sale.SaleId;
            }
        }

        /// <summary>
        /// Generate SaleCode format: MS-YYYYMMDD-N (N = daily sequence number starting at 1)
        /// </summary>
        private string GenerateSaleCode(ClinicDbContext db, DateTime saleDate)
        {
            // Count sales for today
            DateTime todayStart = saleDate.Date;
            DateTime todayEnd = todayStart.AddDays(1);
            
            int todayCount = db.MedicineSales
                .Where(s => s.SaleDate >= todayStart && s.SaleDate < todayEnd)
                .Count();
            
            int nextNumber = todayCount + 1;
            return $"MS-{saleDate:yyyyMMdd}-{nextNumber}";
        }

        public MedicineSaleDTO GetById(int saleId)
        {
            using (var db = new ClinicDbContext())
            {
                var sale = db.MedicineSales.Find(saleId);
                if (sale != null)
                {
                    // Load navigation
                    if (sale.PatientId.HasValue)
                    {
                        var patient = db.Patients.Find(sale.PatientId.Value);
                        sale.PatientName = patient?.FullName;
                    }
                    // Load Pharmacist Name
                    if (sale.PharmacistId > 0)
                    {
                        var emp = db.Employees.FirstOrDefault(e => e.UserId == sale.PharmacistId);
                        if (emp != null)
                        {
                            sale.PharmacistName = emp.FullName;
                        }
                        else
                        {
                            var pharmacist = db.Users.Find(sale.PharmacistId);
                            sale.PharmacistName = pharmacist?.Username;
                        }
                    }
                    // Load Cashier Name (if applicable)
                    if (sale.CashierId.HasValue)
                    {
                        var emp = db.Employees.FirstOrDefault(e => e.UserId == sale.CashierId.Value);
                        if (emp != null)
                        {
                            sale.CashierName = emp.FullName;
                        }
                        else
                        {
                            var cashier = db.Users.Find(sale.CashierId.Value);
                            sale.CashierName = cashier?.Username;
                        }
                    }
                }
                return sale;
            }
        }

        public List<MedicineSaleDTO> GetByPatient(int patientId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.MedicineSales
                    .Where(s => s.PatientId == patientId)
                    .OrderByDescending(s => s.SaleDate)
                    .ToList();
            }
        }

        public List<MedicineSaleDTO> GetByDate(DateTime date)
        {
            using (var db = new ClinicDbContext())
            {
                return (from s in db.MedicineSales
                        join p in db.Patients on s.PatientId equals p.PatientId into patients
                        from patient in patients.DefaultIfEmpty()
                        where DbFunctions.TruncateTime(s.SaleDate) == date.Date
                        orderby s.SaleDate descending
                        select new
                        {
                            Sale = s,
                            PatientName = patient != null ? patient.FullName : "Khách vãng lai"
                        })
                        .ToList()
                        .Select(x =>
                        {
                            x.Sale.PatientName = x.Sale.IsWalkIn && !string.IsNullOrEmpty(x.Sale.WalkInCustomerName) 
                                ? x.Sale.WalkInCustomerName 
                                : x.PatientName;
                            return x.Sale;
                        })
                        .ToList();
            }
        }

        public List<MedicineSaleDTO> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            using (var db = new ClinicDbContext())
            {
                return (from s in db.MedicineSales
                        join p in db.Patients on s.PatientId equals p.PatientId into patients
                        from patient in patients.DefaultIfEmpty()
                        where s.SaleDate >= fromDate && s.SaleDate < toDate
                        orderby s.SaleDate descending
                        select new
                        {
                            Sale = s,
                            PatientName = patient != null ? patient.FullName : "Khách vãng lai"
                        })
                        .ToList()
                        .Select(x =>
                        {
                            x.Sale.PatientName = x.Sale.IsWalkIn && !string.IsNullOrEmpty(x.Sale.WalkInCustomerName) 
                                ? x.Sale.WalkInCustomerName 
                                : x.PatientName;
                            return x.Sale;
                        })
                        .ToList();
            }
        }

        public List<MedicineSaleDTO> GetByStatus(string status)
        {
            using (var db = new ClinicDbContext())
            {
                return (from s in db.MedicineSales
                        join p in db.Patients on s.PatientId equals p.PatientId into patients
                        from patient in patients.DefaultIfEmpty()
                        where s.Status == status
                        orderby s.SaleDate descending
                        select new
                        {
                            Sale = s,
                            PatientName = patient != null ? patient.FullName : "Khách vãng lai"
                        })
                        .ToList()
                        .Select(x =>
                        {
                            x.Sale.PatientName = x.Sale.IsWalkIn && !string.IsNullOrEmpty(x.Sale.WalkInCustomerName) 
                                ? x.Sale.WalkInCustomerName 
                                : x.PatientName;
                            return x.Sale;
                        })
                        .ToList();
            }
        }

        public List<MedicineSaleDTO> GetPendingPayments()
        {
            return GetByStatus("Pending");
        }

        public List<MedicineSaleDTO> GetPaidNotDispensed()
        {
            return GetByStatus("Paid");
        }

        public bool Update(MedicineSaleDTO sale)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.MedicineSales.Find(sale.SaleId);
                if (existing == null) return false;

                existing.TotalAmount = sale.TotalAmount;
                existing.PaymentMethod = sale.PaymentMethod;
                existing.PaymentDate = sale.PaymentDate;
                existing.Status = sale.Status;
                existing.CashierId = sale.CashierId;
                existing.Notes = sale.Notes;
                existing.IsWalkIn = sale.IsWalkIn;
                existing.WalkInCustomerName = sale.WalkInCustomerName;

                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateStatus(int saleId, string status)
        {
            using (var db = new ClinicDbContext())
            {
                var sale = db.MedicineSales.Find(saleId);
                if (sale == null) return false;

                sale.Status = status;
                if (status == "Paid") sale.PaymentDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
        }

        public bool Delete(int saleId)
        {
            using (var db = new ClinicDbContext())
            {
                var sale = db.MedicineSales.Find(saleId);
                if (sale == null) return false;

                db.MedicineSales.Remove(sale);
                db.SaveChanges();
                return true;
            }
        }
    }
}
