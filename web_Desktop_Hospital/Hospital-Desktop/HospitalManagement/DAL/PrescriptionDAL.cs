using System;
using System.Collections.Generic;
using System.Linq;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Prescriptions - Entity Framework Version
    /// </summary>
    public class PrescriptionDAL
    {
        public PrescriptionDTO GetById(int prescriptionId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Prescriptions.Find(prescriptionId);
            }
        }

        public PrescriptionDTO GetByVisitId(int visitId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Prescriptions.FirstOrDefault(p => p.VisitId == visitId);
            }
        }

        // Alias for BLL compatibility
        public PrescriptionDTO GetByVisit(int visitId)
        {
            return GetByVisitId(visitId);
        }

        public List<PrescriptionDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Prescriptions.OrderByDescending(p => p.CreatedDate).ToList();
            }
        }

        public List<PrescriptionDTO> GetByDate(DateTime date)
        {
            using (var db = new ClinicDbContext())
            {
                return (from p in db.Prescriptions
                        join v in db.Visits on p.VisitId equals v.VisitId
                        join pt in db.Patients on v.PatientId equals pt.PatientId
                        join d in db.Doctors on v.DoctorId equals d.DoctorId into doctors
                        from doctor in doctors.DefaultIfEmpty()
                        where System.Data.Entity.DbFunctions.TruncateTime(p.CreatedDate) == date.Date
                        orderby p.CreatedDate descending
                        select new
                        {
                            Prescription = p,
                            PatientName = pt.FullName,
                            DoctorName = doctor != null ? doctor.FullName : ""
                        })
                        .ToList()
                        .Select(x =>
                        {
                            x.Prescription.PatientName = x.PatientName;
                            x.Prescription.DoctorName = x.DoctorName;
                            return x.Prescription;
                        })
                        .ToList();
            }
        }

        // Alias for MedicineSaleBLL compatibility
        public PrescriptionDTO GetByPrescriptionId(int prescriptionId)
        {
            return GetById(prescriptionId);
        }

        public int Insert(PrescriptionDTO prescription)
        {
            using (var db = new ClinicDbContext())
            {
                db.Prescriptions.Add(prescription);
                db.SaveChanges();
                return prescription.PrescriptionId;
            }
        }

        public bool Update(PrescriptionDTO prescription)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Prescriptions.Find(prescription.PrescriptionId);
                if (existing == null) return false;

                existing.VisitId = prescription.VisitId;
                existing.CreatedDate = prescription.CreatedDate;

                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int prescriptionId)
        {
            using (var db = new ClinicDbContext())
            {
                var prescription = db.Prescriptions.Find(prescriptionId);
                if (prescription == null) return false;

                db.Prescriptions.Remove(prescription);
                return db.SaveChanges() > 0;
            }
        }
    }
}
