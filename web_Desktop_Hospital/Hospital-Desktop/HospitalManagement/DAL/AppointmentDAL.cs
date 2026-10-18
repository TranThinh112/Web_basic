using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Appointments - Optimized Version
    /// Sử dụng JOIN thay vì N+1 queries
    /// </summary>
    public class AppointmentDAL
    {
        public AppointmentDTO GetById(int appointmentId)
        {
            using (var db = new ClinicDbContext())
            {
                var result = GetOptimizedQuery(db)
                    .FirstOrDefault(a => a.AppointmentId == appointmentId);
                return result != null ? MapToDTO(result) : null;
            }
        }

        public List<AppointmentDTO> GetByPatient(int patientId)
        {
            using (var db = new ClinicDbContext())
            {
                return GetOptimizedQuery(db)
                    .Where(a => a.PatientId == patientId)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        public List<AppointmentDTO> GetByDoctor(int doctorId)
        {
            using (var db = new ClinicDbContext())
            {
                return GetOptimizedQuery(db)
                    .Where(a => a.DoctorId == doctorId)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        public List<AppointmentDTO> GetByDate(DateTime date)
        {
            using (var db = new ClinicDbContext())
            {
                return GetOptimizedQuery(db)
                    .Where(a => DbFunctions.TruncateTime(a.AppointmentDate) == date.Date)
                    .OrderBy(a => a.AppointmentDate)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        public List<AppointmentDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                return GetOptimizedQuery(db)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        /// <summary>
        /// Lấy appointments phân trang có sắp xếp
        /// </summary>
        public List<AppointmentDTO> GetPaged(int page, int pageSize, string sortColumn = null, bool sortAsc = true)
        {
            using (var db = new ClinicDbContext())
            {
                var query = GetOptimizedQuery(db);
                query = ApplySortQuery(query, sortColumn, sortAsc);
                
                return query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        /// <summary>
        /// Tìm kiếm appointments có phân trang
        /// </summary>
        public List<AppointmentDTO> SearchPaged(string keyword, int page, int pageSize, string sortColumn = null, bool sortAsc = true)
        {
            using (var db = new ClinicDbContext())
            {
                var query = GetOptimizedQuery(db);
                
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query = query.Where(a => a.PatientName.Contains(keyword) || 
                                            a.PatientPhone.Contains(keyword) ||
                                            a.DoctorName.Contains(keyword) ||
                                            a.Reason.Contains(keyword));
                }

                query = ApplySortQuery(query, sortColumn, sortAsc);

                return query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        /// <summary>
        /// Đếm tổng số appointments
        /// </summary>
        public int GetCount()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Appointments.Count();
            }
        }

        /// <summary>
        /// Đếm số appointments theo keyword
        /// </summary>
        public int SearchCount(string keyword)
        {
            using (var db = new ClinicDbContext())
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return db.Appointments.Count();

                var query = GetOptimizedQuery(db);
                return query.Count(a => a.PatientName.Contains(keyword) || 
                                       a.PatientPhone.Contains(keyword) ||
                                       a.DoctorName.Contains(keyword));
            }
        }

        /// <summary>
        /// Áp dụng sắp xếp theo cột
        /// </summary>
        private IQueryable<AppointmentQueryResult> ApplySortQuery(IQueryable<AppointmentQueryResult> query, string sortColumn, bool sortAsc)
        {
            if (string.IsNullOrEmpty(sortColumn))
            {
                return query.OrderByDescending(a => a.AppointmentDate);
            }

            switch (sortColumn)
            {
                case "PatientName":
                    return sortAsc ? query.OrderBy(a => a.PatientName) : query.OrderByDescending(a => a.PatientName);
                case "DoctorName":
                    return sortAsc ? query.OrderBy(a => a.DoctorName) : query.OrderByDescending(a => a.DoctorName);
                case "AppointmentDate":
                    return sortAsc ? query.OrderBy(a => a.AppointmentDate) : query.OrderByDescending(a => a.AppointmentDate);
                case "Status":
                    return sortAsc ? query.OrderBy(a => a.Status) : query.OrderByDescending(a => a.Status);
                case "Reason":
                    return sortAsc ? query.OrderBy(a => a.Reason) : query.OrderByDescending(a => a.Reason);
                default:
                    return sortAsc ? query.OrderBy(a => a.AppointmentId) : query.OrderByDescending(a => a.AppointmentId);
            }
        }

        // Intermediate class for query
        private class AppointmentQueryResult
        {
            public int AppointmentId { get; set; }
            public int PatientId { get; set; }
            public int DoctorId { get; set; }
            public DateTime AppointmentDate { get; set; }
            public string Reason { get; set; }
            public string Status { get; set; }
            public string PatientName { get; set; }
            public string PatientPhone { get; set; }
            public string DoctorName { get; set; }
        }

        private IQueryable<AppointmentQueryResult> GetOptimizedQuery(ClinicDbContext db)
        {
            return from a in db.Appointments
                   join p in db.Patients on a.PatientId equals p.PatientId
                   join d in db.Doctors on a.DoctorId equals d.DoctorId
                   select new AppointmentQueryResult
                   {
                       AppointmentId = a.AppointmentId,
                       PatientId = a.PatientId,
                       DoctorId = a.DoctorId,
                       AppointmentDate = a.AppointmentDate,
                       Reason = a.Reason,
                       Status = a.Status,
                       PatientName = p.FullName,
                       PatientPhone = p.Phone,
                       DoctorName = d.FullName
                   };
        }

        private AppointmentDTO MapToDTO(AppointmentQueryResult r)
        {
            return new AppointmentDTO
            {
                AppointmentId = r.AppointmentId,
                PatientId = r.PatientId,
                DoctorId = r.DoctorId,
                AppointmentDate = r.AppointmentDate,
                Reason = r.Reason,
                Status = r.Status,
                PatientName = r.PatientName,
                PatientPhone = r.PatientPhone,
                DoctorName = r.DoctorName
            };
        }

        public int Insert(AppointmentDTO appointment)
        {
            using (var db = new ClinicDbContext())
            {
                var entity = new AppointmentDTO
                {
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,
                    AppointmentDate = appointment.AppointmentDate,
                    Reason = appointment.Reason,
                    Status = string.IsNullOrEmpty(appointment.Status) ? "Pending" : appointment.Status
                };
                db.Appointments.Add(entity);
                db.SaveChanges();
                return entity.AppointmentId;
            }
        }

        public bool Update(AppointmentDTO appointment)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Appointments.Find(appointment.AppointmentId);
                if (existing == null) return false;

                existing.PatientId = appointment.PatientId;
                existing.DoctorId = appointment.DoctorId;
                existing.AppointmentDate = appointment.AppointmentDate;
                existing.Reason = appointment.Reason;
                existing.Status = appointment.Status ?? "Pending";

                return db.SaveChanges() > 0;
            }
        }

        public bool UpdateStatus(int appointmentId, string status)
        {
            using (var db = new ClinicDbContext())
            {
                var apt = db.Appointments.Find(appointmentId);
                if (apt == null) return false;

                apt.Status = status;
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int appointmentId)
        {
            using (var db = new ClinicDbContext())
            {
                var apt = db.Appointments.Find(appointmentId);
                if (apt == null) return false;

                db.Appointments.Remove(apt);
                return db.SaveChanges() > 0;
            }
        }
    }
}
