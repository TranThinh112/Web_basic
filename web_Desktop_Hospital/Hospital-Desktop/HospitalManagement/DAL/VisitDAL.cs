using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Visits - Optimized with Safe Async
    /// </summary>
    public class VisitDAL
    {
        #region Sync Methods

        public VisitDTO GetById(int visitId)
        {
            using (var db = new ClinicDbContext())
            {
                var result = GetOptimizedQuery(db)
                    .FirstOrDefault(v => v.VisitId == visitId);
                return result != null ? MapToDTO(result) : null;
            }
        }

        public List<VisitDTO> GetByPatient(int patientId)
        {
            using (var db = new ClinicDbContext())
            {
                return GetOptimizedQuery(db)
                    .Where(v => v.PatientId == patientId)
                    .OrderByDescending(v => v.VisitDate)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        public List<VisitDTO> GetByDoctor(int doctorId)
        {
            using (var db = new ClinicDbContext())
            {
                return GetOptimizedQuery(db)
                    .Where(v => v.DoctorId == doctorId)
                    .OrderByDescending(v => v.VisitDate)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        public List<VisitDTO> GetByDate(DateTime date)
        {
            using (var db = new ClinicDbContext())
            {
                return GetOptimizedQuery(db)
                    .Where(v => DbFunctions.TruncateTime(v.VisitDate) == date.Date)
                    .OrderBy(v => v.VisitDate)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        public List<VisitDTO> GetByDoctorAndDate(int doctorId, DateTime date)
        {
            using (var db = new ClinicDbContext())
            {
                return GetOptimizedQuery(db)
                    .Where(v => v.DoctorId == doctorId &&
                                DbFunctions.TruncateTime(v.VisitDate) == date.Date)
                    .OrderBy(v => v.VisitDate)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        public List<VisitDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                return GetOptimizedQuery(db)
                    .OrderByDescending(v => v.VisitDate)
                    .ToList()
                    .Select(MapToDTO).ToList();
            }
        }

        /// <summary>
        /// Lấy visits phân trang có sắp xếp
        /// </summary>
        public List<VisitDTO> GetPaged(int page, int pageSize, string sortColumn = null, bool sortAsc = true)
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
        /// Tìm kiếm visits có phân trang
        /// </summary>
        public List<VisitDTO> SearchPaged(string keyword, int page, int pageSize, string sortColumn = null, bool sortAsc = true)
        {
            using (var db = new ClinicDbContext())
            {
                var query = GetOptimizedQuery(db);
                
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query = query.Where(v => v.PatientName.Contains(keyword) || 
                                            v.DoctorName.Contains(keyword) ||
                                            v.Symptoms.Contains(keyword) ||
                                            v.Diagnosis.Contains(keyword));
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
        /// Đếm tổng số visits
        /// </summary>
        public int GetCount()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Visits.Count();
            }
        }

        /// <summary>
        /// Đếm số visits theo keyword
        /// </summary>
        public int SearchCount(string keyword)
        {
            using (var db = new ClinicDbContext())
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return db.Visits.Count();

                var query = GetOptimizedQuery(db);
                return query.Count(v => v.PatientName.Contains(keyword) || 
                                       v.DoctorName.Contains(keyword) ||
                                       v.Symptoms.Contains(keyword));
            }
        }

        /// <summary>
        /// Áp dụng sắp xếp theo cột
        /// </summary>
        private IQueryable<VisitQueryResult> ApplySortQuery(IQueryable<VisitQueryResult> query, string sortColumn, bool sortAsc)
        {
            if (string.IsNullOrEmpty(sortColumn))
            {
                return query.OrderByDescending(v => v.VisitDate);
            }

            switch (sortColumn)
            {
                case "PatientName":
                    return sortAsc ? query.OrderBy(v => v.PatientName) : query.OrderByDescending(v => v.PatientName);
                case "DoctorName":
                    return sortAsc ? query.OrderBy(v => v.DoctorName) : query.OrderByDescending(v => v.DoctorName);
                case "VisitDate":
                    return sortAsc ? query.OrderBy(v => v.VisitDate) : query.OrderByDescending(v => v.VisitDate);
                case "Status":
                    return sortAsc ? query.OrderBy(v => v.Status) : query.OrderByDescending(v => v.Status);
                case "Symptoms":
                    return sortAsc ? query.OrderBy(v => v.Symptoms) : query.OrderByDescending(v => v.Symptoms);
                case "DiseaseName":
                    return sortAsc ? query.OrderBy(v => v.DiseaseName) : query.OrderByDescending(v => v.DiseaseName);
                default:
                    return sortAsc ? query.OrderBy(v => v.VisitId) : query.OrderByDescending(v => v.VisitId);
            }
        }

        #endregion

        #region Safe Async Methods (dùng Task.Run để tránh deadlock)

        /// <summary>
        /// Async version - chạy trên background thread để tránh deadlock
        /// </summary>
        public Task<List<VisitDTO>> GetByDateSafeAsync(DateTime date)
        {
            return Task.Run(() => GetByDate(date));
        }

        /// <summary>
        /// Async version - chạy trên background thread để tránh deadlock
        /// </summary>
        public Task<List<VisitDTO>> GetByDoctorAndDateSafeAsync(int doctorId, DateTime date)
        {
            return Task.Run(() => GetByDoctorAndDate(doctorId, date));
        }

        #endregion

        #region Helper Classes

        private class VisitQueryResult
        {
            public int VisitId { get; set; }
            public int PatientId { get; set; }
            public int DoctorId { get; set; }
            public DateTime VisitDate { get; set; }
            public string Symptoms { get; set; }
            public string Diagnosis { get; set; }
            public int? DiseaseId { get; set; }
            public string Status { get; set; }
            public string PatientName { get; set; }
            public string DoctorName { get; set; }
            public string DiseaseName { get; set; }
            public string MedicalHistory { get; set; }
        }

        private IQueryable<VisitQueryResult> GetOptimizedQuery(ClinicDbContext db)
        {
            return from v in db.Visits
                   join p in db.Patients on v.PatientId equals p.PatientId
                   join d in db.Doctors on v.DoctorId equals d.DoctorId
                   join dis in db.Diseases on v.DiseaseId equals dis.DiseaseId into diseases
                   from disease in diseases.DefaultIfEmpty()
                   select new VisitQueryResult
                   {
                       VisitId = v.VisitId,
                       PatientId = v.PatientId,
                       DoctorId = v.DoctorId,
                       VisitDate = v.VisitDate,
                       Symptoms = v.Symptoms,
                       Diagnosis = v.Diagnosis,
                       DiseaseId = v.DiseaseId,
                       Status = v.Status,
                       PatientName = p.FullName,
                       DoctorName = d.FullName,
                       DiseaseName = disease != null ? disease.DiseaseName : null,
                       MedicalHistory = p.MedicalHistory
                   };
        }

        private VisitDTO MapToDTO(VisitQueryResult r)
        {
            return new VisitDTO
            {
                VisitId = r.VisitId,
                PatientId = r.PatientId,
                DoctorId = r.DoctorId,
                VisitDate = r.VisitDate,
                Symptoms = r.Symptoms,
                Diagnosis = r.Diagnosis,
                DiseaseId = r.DiseaseId,
                Status = r.Status,
                PatientName = r.PatientName,
                DoctorName = r.DoctorName,
                DiseaseName = r.DiseaseName,
                MedicalHistory = r.MedicalHistory
            };
        }

        #endregion

        #region CRUD Operations

        public int Insert(VisitDTO visit)
        {
            using (var db = new ClinicDbContext())
            {
                var entity = new VisitDTO
                {
                    PatientId = visit.PatientId,
                    DoctorId = visit.DoctorId,
                    VisitDate = visit.VisitDate,
                    Symptoms = visit.Symptoms,
                    Diagnosis = visit.Diagnosis,
                    DiseaseId = visit.DiseaseId,
                    Status = visit.Status ?? "Waiting"
                };
                db.Visits.Add(entity);
                db.SaveChanges();
                return entity.VisitId;
            }
        }

        public bool Update(VisitDTO visit)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Visits.Find(visit.VisitId);
                if (existing == null) return false;

                existing.PatientId = visit.PatientId;
                existing.DoctorId = visit.DoctorId;
                existing.VisitDate = visit.VisitDate;
                existing.Symptoms = visit.Symptoms;
                existing.Diagnosis = visit.Diagnosis;
                existing.DiseaseId = visit.DiseaseId;
                existing.Status = visit.Status;

                return db.SaveChanges() > 0;
            }
        }

        public bool UpdateDiagnosis(int visitId, string symptoms, string diagnosis, int? diseaseId)
        {
            using (var db = new ClinicDbContext())
            {
                var visit = db.Visits.Find(visitId);
                if (visit == null) return false;

                visit.Symptoms = symptoms;
                visit.Diagnosis = diagnosis;
                visit.DiseaseId = diseaseId;
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int visitId)
        {
            using (var db = new ClinicDbContext())
            {
                var visit = db.Visits.Find(visitId);
                if (visit == null) return false;

                db.Visits.Remove(visit);
                return db.SaveChanges() > 0;
            }
        }

        #endregion
    }
}
