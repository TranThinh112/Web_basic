using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Patients - Entity Framework Version
    /// </summary>
    public class PatientDAL
    {
        /// <summary>
        /// Lấy patient theo Id
        /// </summary>
        public PatientDTO GetById(int patientId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Patients.Find(patientId);
            }
        }

        /// <summary>
        /// Lấy tất cả patients
        /// </summary>
        public List<PatientDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Patients.OrderBy(p => p.FullName).ToList();
            }
        }

        /// <summary>
        /// Tìm kiếm patients theo keyword (tên, CCCD, số điện thoại)
        /// </summary>
        public List<PatientDTO> Search(string keyword)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Patients
                    .Where(p => p.FullName.StartsWith(keyword) || 
                                p.CCCD.StartsWith(keyword) || 
                                p.Phone.StartsWith(keyword))
                    .OrderBy(p => p.FullName)
                    .ToList();
            }
        }

        /// <summary>
        /// Lấy patients phân trang có sắp xếp
        /// </summary>
        public List<PatientDTO> GetPaged(int page, int pageSize, string sortColumn = null, bool sortAsc = true)
        {
            using (var db = new ClinicDbContext())
            {
                var query = db.Patients.AsQueryable();
                query = ApplySort(query, sortColumn, sortAsc);
                
                return query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        /// <summary>
        /// Tìm kiếm patients có phân trang và sắp xếp
        /// </summary>
        public List<PatientDTO> SearchPaged(string keyword, int page, int pageSize, string sortColumn = null, bool sortAsc = true)
        {
            using (var db = new ClinicDbContext())
            {
                var query = db.Patients.AsQueryable();
                
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query = query.Where(p => p.FullName.StartsWith(keyword) || 
                                            p.CCCD.StartsWith(keyword) || 
                                            p.Phone.StartsWith(keyword) ||
                                            p.PatientCode.StartsWith(keyword));
                }

                query = ApplySort(query, sortColumn, sortAsc);

                return query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        /// <summary>
        /// Áp dụng sắp xếp theo cột
        /// </summary>
        private IQueryable<PatientDTO> ApplySort(IQueryable<PatientDTO> query, string sortColumn, bool sortAsc)
        {
            if (string.IsNullOrEmpty(sortColumn))
            {
                // Default: mới nhất trước
                return query.OrderByDescending(p => p.PatientId);
            }

            switch (sortColumn)
            {
                case "PatientCode":
                    return sortAsc ? query.OrderBy(p => p.PatientCode) : query.OrderByDescending(p => p.PatientCode);
                case "FullName":
                    return sortAsc ? query.OrderBy(p => p.FullName) : query.OrderByDescending(p => p.FullName);
                case "DateOfBirth":
                    return sortAsc ? query.OrderBy(p => p.DateOfBirth) : query.OrderByDescending(p => p.DateOfBirth);
                case "Gender":
                    return sortAsc ? query.OrderBy(p => p.Gender) : query.OrderByDescending(p => p.Gender);
                case "Phone":
                    return sortAsc ? query.OrderBy(p => p.Phone) : query.OrderByDescending(p => p.Phone);
                case "Address":
                    return sortAsc ? query.OrderBy(p => p.Address) : query.OrderByDescending(p => p.Address);
                case "CCCD":
                    return sortAsc ? query.OrderBy(p => p.CCCD) : query.OrderByDescending(p => p.CCCD);
                default:
                    return sortAsc ? query.OrderBy(p => p.PatientId) : query.OrderByDescending(p => p.PatientId);
            }
        }

        /// <summary>
        /// Đếm tổng số patients
        /// </summary>
        public int GetCount()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Patients.Count();
            }
        }

        /// <summary>
        /// Đếm số patients theo keyword
        /// </summary>
        public int SearchCount(string keyword)
        {
            using (var db = new ClinicDbContext())
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return db.Patients.Count();

                return db.Patients
                    .Count(p => p.FullName.StartsWith(keyword) || 
                               p.CCCD.StartsWith(keyword) || 
                               p.Phone.StartsWith(keyword) ||
                               p.PatientCode.StartsWith(keyword));
            }
        }

        /// <summary>
        /// Lấy patient theo CCCD
        /// </summary>
        public PatientDTO GetByCCCD(string cccd)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Patients.FirstOrDefault(p => p.CCCD == cccd);
            }
        }

        /// <summary>
        /// Thêm patient mới
        /// </summary>
        public int Insert(PatientDTO patient)
        {
            using (var db = new ClinicDbContext())
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return patient.PatientId;
            }
        }

        /// <summary>
        /// Cập nhật thông tin patient
        /// </summary>
        public bool Update(PatientDTO patient)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Patients.Find(patient.PatientId);
                if (existing == null) return false;

                existing.FullName = patient.FullName;
                existing.DateOfBirth = patient.DateOfBirth;
                existing.Gender = patient.Gender;
                existing.Phone = patient.Phone;
                existing.Address = patient.Address;
                existing.CCCD = patient.CCCD;
                existing.MedicalHistory = patient.MedicalHistory;
                existing.PatientCode = patient.PatientCode;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Xóa patient
        /// </summary>
        public bool Delete(int patientId)
        {
            using (var db = new ClinicDbContext())
            {
                var patient = db.Patients.Find(patientId);
                if (patient == null) return false;

                db.Patients.Remove(patient);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Kiểm tra CCCD đã tồn tại chưa
        /// </summary>
        public bool CCCDExists(string cccd, int? excludePatientId = null)
        {
            using (var db = new ClinicDbContext())
            {
                if (excludePatientId.HasValue)
                {
                    return db.Patients.Any(p => p.CCCD == cccd && p.PatientId != excludePatientId.Value);
                }
                return db.Patients.Any(p => p.CCCD == cccd);
            }
        }

        /// <summary>
        /// Kiểm tra SĐT đã tồn tại chưa
        /// </summary>
        public bool PhoneExists(string phone, int? excludePatientId = null)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            
            using (var db = new ClinicDbContext())
            {
                if (excludePatientId.HasValue)
                {
                    return db.Patients.Any(p => p.Phone == phone && p.PatientId != excludePatientId.Value);
                }
                return db.Patients.Any(p => p.Phone == phone);
            }
        }
    }
}
