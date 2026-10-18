using System;
using System.Collections.Generic;
using System.Linq;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Doctors - Entity Framework Version
    /// </summary>
    public class DoctorDAL
    {
        public DoctorDTO GetById(int doctorId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Doctors.Find(doctorId);
            }
        }

        public List<DoctorDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Doctors.OrderBy(d => d.FullName).ToList();
            }
        }

        public DoctorDTO GetByUserId(int userId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Doctors.FirstOrDefault(d => d.UserId == userId);
            }
        }

        public List<DoctorDTO> Search(string keyword)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Doctors
                    .Where(d => d.FullName.Contains(keyword) || d.Specialty.Contains(keyword))
                    .OrderBy(d => d.FullName)
                    .ToList();
            }
        }

        public List<DoctorDTO> GetBySpecialty(string specialty)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Doctors
                    .Where(d => d.Specialty == specialty)
                    .OrderBy(d => d.FullName)
                    .ToList();
            }
        }

        public int Insert(DoctorDTO doctor)
        {
            using (var db = new ClinicDbContext())
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return doctor.DoctorId;
            }
        }

        public bool Update(DoctorDTO doctor)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Doctors.Find(doctor.DoctorId);
                if (existing == null) return false;

                existing.FullName = doctor.FullName;
                existing.Specialty = doctor.Specialty;
                existing.UserId = doctor.UserId;

                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int doctorId)
        {
            using (var db = new ClinicDbContext())
            {
                var doctor = db.Doctors.Find(doctorId);
                if (doctor == null) return false;

                db.Doctors.Remove(doctor);
                return db.SaveChanges() > 0;
            }
        }
    }
}
