using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Invoices - Entity Framework Version
    /// </summary>
    public class InvoiceDAL
    {
        public InvoiceDTO GetById(int invoiceId)
        {
            using (var db = new ClinicDbContext())
            {
                var inv = db.Invoices.Find(invoiceId);
                if (inv != null) LoadNavigationProperties(db, inv);
                return inv;
            }
        }

        public InvoiceDTO GetByVisitId(int visitId)
        {
            using (var db = new ClinicDbContext())
            {
                var inv = db.Invoices.FirstOrDefault(i => i.VisitId == visitId);
                if (inv != null) LoadNavigationProperties(db, inv);
                return inv;
            }
        }

        public List<InvoiceDTO> GetByPatient(int patientId)
        {
            using (var db = new ClinicDbContext())
            {
                var list = db.Invoices
                    .Where(i => i.PatientId == patientId)
                    .OrderByDescending(i => i.PaymentDate)
                    .ToList();
                list.ForEach(i => LoadNavigationProperties(db, i));
                return list;
            }
        }

        public List<InvoiceDTO> GetByDate(DateTime date)
        {
            using (var db = new ClinicDbContext())
            {
                var list = db.Invoices
                    .Where(i => DbFunctions.TruncateTime(i.PaymentDate) == date.Date)
                    .OrderBy(i => i.PaymentDate)
                    .ToList();
                list.ForEach(i => LoadNavigationProperties(db, i));
                return list;
            }
        }

        public List<InvoiceDTO> GetByStatus(string status)
        {
            using (var db = new ClinicDbContext())
            {
                var list = db.Invoices
                    .Where(i => i.Status == status)
                    .OrderByDescending(i => i.PaymentDate)
                    .ToList();
                list.ForEach(i => LoadNavigationProperties(db, i));
                return list;
            }
        }

        public List<InvoiceDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                var list = db.Invoices
                    .OrderByDescending(i => i.PaymentDate)
                    .ToList();
                list.ForEach(i => LoadNavigationProperties(db, i));
                return list;
            }
        }

        public InvoiceDTO GetByVisit(int visitId)
        {
            using (var db = new ClinicDbContext())
            {
                var inv = db.Invoices.FirstOrDefault(i => i.VisitId == visitId);
                if (inv != null) LoadNavigationProperties(db, inv);
                return inv;
            }
        }

        public List<InvoiceDTO> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            using (var db = new ClinicDbContext())
            {
                // Use TruncateTime to compare dates only (ignore time component)
                var list = db.Invoices
                    .Where(i => DbFunctions.TruncateTime(i.PaymentDate) >= fromDate.Date 
                             && DbFunctions.TruncateTime(i.PaymentDate) <= toDate.Date)
                    .OrderByDescending(i => i.PaymentDate)
                    .ToList();
                list.ForEach(i => LoadNavigationProperties(db, i));
                return list;
            }
        }

        public int Insert(InvoiceDTO invoice)
        {
            using (var db = new ClinicDbContext())
            {
                if (string.IsNullOrEmpty(invoice.Status))
                    invoice.Status = "Pending";
                if (string.IsNullOrEmpty(invoice.PaymentMethod))
                    invoice.PaymentMethod = "Cash";
                if (invoice.InvoiceDate == default)
                    invoice.InvoiceDate = DateTime.Now;
                
                // Generate InvoiceCode BEFORE insert - based on daily count
                if (string.IsNullOrEmpty(invoice.InvoiceCode))
                {
                    invoice.InvoiceCode = GenerateInvoiceCode(db, invoice.InvoiceDate);
                }
                    
                db.Invoices.Add(invoice);
                db.SaveChanges();
                
                return invoice.InvoiceId;
            }
        }

        /// <summary>
        /// Generate InvoiceCode format: INV-YYYYMMDD-N (N = daily sequence number starting at 1)
        /// </summary>
        private string GenerateInvoiceCode(ClinicDbContext db, DateTime invoiceDate)
        {
            // Count invoices for today
            DateTime todayStart = invoiceDate.Date;
            DateTime todayEnd = todayStart.AddDays(1);
            
            int todayCount = db.Invoices
                .Where(i => i.InvoiceDate >= todayStart && i.InvoiceDate < todayEnd)
                .Count();
            
            int nextNumber = todayCount + 1;
            return $"INV-{invoiceDate:yyyyMMdd}-{nextNumber}";
        }

        public bool Update(InvoiceDTO invoice)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Invoices.Find(invoice.InvoiceId);
                if (existing == null) return false;

                existing.VisitId = invoice.VisitId;
                existing.PatientId = invoice.PatientId;
                existing.TotalAmount = invoice.TotalAmount;
                existing.InvoiceDate = invoice.InvoiceDate;
                existing.PaymentDate = invoice.PaymentDate;
                existing.PaymentMethod = invoice.PaymentMethod ?? "Cash";
                existing.Status = invoice.Status ?? "Pending";

                return db.SaveChanges() > 0;
            }
        }

        public bool UpdateStatus(int invoiceId, string status)
        {
            using (var db = new ClinicDbContext())
            {
                var inv = db.Invoices.Find(invoiceId);
                if (inv == null) return false;

                inv.Status = status;
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int invoiceId)
        {
            using (var db = new ClinicDbContext())
            {
                var inv = db.Invoices.Find(invoiceId);
                if (inv == null) return false;

                db.Invoices.Remove(inv);
                return db.SaveChanges() > 0;
            }
        }

        public bool MarkAsPaid(int invoiceId)
        {
            return UpdateStatus(invoiceId, "Paid");
        }

        /// <summary>
        /// Kiểm tra visit đã thanh toán chưa
        /// </summary>
        public bool IsVisitPaid(int visitId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Invoices.Any(i => i.VisitId == visitId && i.Status == "Paid");
            }
        }

        /// <summary>
        /// Lấy danh sách VisitId đã thanh toán trong ngày
        /// </summary>
        public List<int> GetPaidVisitIds(DateTime date)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Invoices
                    .Where(i => i.Status == "Paid" && 
                               DbFunctions.TruncateTime(i.PaymentDate) == date.Date)
                    .Select(i => i.VisitId)
                    .ToList();
            }
        }

        private void LoadNavigationProperties(ClinicDbContext db, InvoiceDTO inv)
        {
            var patient = db.Patients.Find(inv.PatientId);
            inv.PatientName = patient?.FullName;
        }
    }
}
